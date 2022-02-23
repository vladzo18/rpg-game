using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoreUI;
using ObjectPooling;
using PlayerCreator.Specialization;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsChanger : IViewController {

        private readonly StatsView _statsView;
        private readonly SpecializationChanger _specializationChanger;
        private readonly SpecializationConfigsStorage _specializationConfigsStorage;
        private int _freeStats;
        
        private string SavePath => Path.Combine(Application.dataPath, "Serialization/Player", "PlayerStats.json");
        
        private List<StatView> _statViews;
        private List<StatViewData> _statViewDatas;
        private List<StatController> _statChangers;
        private ObjectPool _objectPool;

        public StatsChanger(StatsView statsView) {
            _statsView = statsView;
            _statViews = new List<StatView>();
            _statViewDatas = new List<StatViewData>();
            _statChangers = new List<StatController>();
            _objectPool = ObjectPool.Instance;
        }
        
        public void Initialize(params object[] args) {
            if (args == null || args.Length < 1 || !args.Any(arg => arg is StatsModel)) {
                throw new NullReferenceException($"There is no args for {nameof(StatsChanger)}");
            }

            object model = args.First(arg => arg is StatsModel);
            StatsModel statsModel = model as StatsModel;
            
            _statViewDatas.Clear();
            foreach (var startStat in statsModel.Stats) {
                StatView statView = _objectPool.GetObject(_statsView.StatsViewPrefab);
                statView.transform.SetParent(_statsView.StatViewsContainerTransform);
                statView.transform.localScale = Vector3.one;
                _statViews.Add(statView);
                
                StatController statController = new StatController(statView);
                statController.Initialize(startStat.StatType.ToString());
                statController.OnStateViewIncreaseClicked += IncreaseStatValue;
                statController.OnStateViewDecreaseClicked += DecreaseStatValue;
                statController.OnStateViewValueClicked += ChangeStatValue;
                
                _statViewDatas.Add(new StatViewData(statController, startStat, startStat.Amount));
            }
            
            _freeStats = statsModel.FreeStats;
            _statsView.FreeStatsText.text = $"Stats left: {_freeStats}";
            
            UpdateStatViews();
            _statsView.Show();
        }

        public void Complete() {
            _statsView.Hide();
            foreach (var statsViewData in _statViewDatas) {
                statsViewData.StatController.Dispose();
                DisposeStatController(statsViewData.StatController);
            }
        }
        
        private void IncreaseStatValue(StatController statController) {
            StatViewData stat = _statViewDatas.Find(data => data.StatController == statController);
            ChangeStat(stat, stat.Stat.Amount + 1);
        }
        
        private void DecreaseStatValue(StatController statController) {
            StatViewData stat = _statViewDatas.Find(data => data.StatController == statController);
            ChangeStat(stat, stat.Stat.Amount - 1);
        }

        private void ChangeStatValue(StatController statController, int value) {
            StatViewData stat = _statViewDatas.Find(data => data.StatController == statController);
            ChangeStat(stat, value);
        }
        
        private void ChangeStat(StatViewData statViewData, int value) {
            int oldValue = statViewData.Stat.Amount;
            if (_freeStats < 0) return;
            
            value = Mathf.Clamp(value, statViewData.MinValue, oldValue + _freeStats);
            _freeStats += oldValue - value;
            _statsView.FreeStatsText.text = $"Stats left: {_freeStats}";
            statViewData.Stat.SetValue(value);
            UpdateStatViews();
        }
        
        private void UpdateStatViews() {
            foreach (var statViewData in _statViewDatas) {
                int value = statViewData.Stat.Amount;
                statViewData.StatController.UpdateView(_freeStats > 0 && value < statViewData.StatController.MaxValue, value > statViewData.MinValue, value);
            }
        }

        private void DisposeStatController(StatController statController) {
            statController.OnStateViewIncreaseClicked -= IncreaseStatValue;
            statController.OnStateViewDecreaseClicked -= DecreaseStatValue;
            statController.OnStateViewValueClicked -= ChangeStatValue;
        }
        
        private void OnDestroy() {
            foreach (var statsChanger in _statChangers) {
                DisposeStatController(statsChanger);
            }
        }
        
    }
    
}