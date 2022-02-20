using System.Collections.Generic;
using System.IO;
using GamePlay;
using ObjectPooling;
using PlayerCreator.Specialization;
using Serialization;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsChanger : MonoBehaviour {

        [SerializeField] private StatsView _statsView;
        [SerializeField] private SpecializationChanger _specializationChanger;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        [SerializeField] private int _freeStats;
        
        private string SavePath => Path.Combine(Application.dataPath, "Serialization/Player", "PlayerStats.json");
        
        private List<StatView> _statsViews;
        private List<StatViewData> _statViewDatas;
        private ObjectPool _objectPool;
        private int _defaultFreeStats;
        private int _currentStartStatsIndex;
        
        private void Start() {
            _defaultFreeStats = _freeStats;
            _statsViews = new List<StatView>();
            _statViewDatas = new List<StatViewData>();
            _objectPool = ObjectPool.Instance;
            
            _specializationChanger.OnSpecializationChange += RefreshStats;
            RefreshStats(_currentStartStatsIndex);
        }
        
        private void RefreshStats(int index) {
            if (_statsViews.Count > 0) ClearStatsViews();
            
            List<Stat> startStats = new List<Stat>();
            foreach (var stat in _specializationConfigsStorage.GetStartStats(index)) {
                startStats.Add(new Stat(stat.StatType, stat.Amount));
            }
            
            _statViewDatas.Clear();
            foreach (var startStat in startStats) {
                StatView statView = _objectPool.GetObject(_statsView.StatsViewPrefab);
                statView.transform.SetParent(_statsView.StatViewsContainerTransform);
                statView.transform.localScale = Vector3.one;
                statView.Initialize(startStat.StatType.ToString());
                statView.OnStateViewIncreaseClicked += IncreaseStatValue;
                statView.OnStateViewDecreaseClicked += DecreaseStatValue;
                statView.OnStateViewValueClicked += ChangeStatValue;
                
                _statsViews.Add(statView);
                _statViewDatas.Add(new StatViewData(statView, startStat, startStat.Amount));
            }
            
            _currentStartStatsIndex = index;
            _freeStats = _defaultFreeStats;
            _statsView.FreeStatsText.text = $"Stats left: {_freeStats}";
            
            UpdateStatViews();
        }
        
        public void SaveStats() {
            List<Stat> stats = new List<Stat>(); 
            foreach (var statViewData in _statViewDatas) {
                stats.Add(new Stat(statViewData.Stat.StatType, statViewData.Stat.Amount));
            }
            StatsSavingData statsSavingData = new StatsSavingData(_currentStartStatsIndex, stats);
            Serializator.SerializeData(statsSavingData, SavePath);
        }

        private void ClearStatsViews() {
            foreach (var statsView in _statsViews) {
                DisposeStatView(statsView);
                statsView.ReturnToPool();
            }
            _statsViews.Clear();
        }
        
        private void IncreaseStatValue(StatView statView) {
            StatViewData stat = _statViewDatas.Find(data => data.StatView == statView);
            ChangeStat(stat, stat.Stat.Amount + 1);
        }
        
        private void DecreaseStatValue(StatView statView) {
            StatViewData stat = _statViewDatas.Find(data => data.StatView == statView);
            ChangeStat(stat, stat.Stat.Amount - 1);
        }

        private void ChangeStatValue(StatView statView, int value) {
            StatViewData stat = _statViewDatas.Find(data => data.StatView == statView);
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
                statViewData.StatView.UpdateView(_freeStats > 0 && value < statViewData.StatView.MaxValue, value > statViewData.MinValue, value);
            }
        }

        private void DisposeStatView(StatView statsView) {
            statsView.OnStateViewIncreaseClicked -= IncreaseStatValue;
            statsView.OnStateViewDecreaseClicked -= DecreaseStatValue;
            statsView.OnStateViewValueClicked -= ChangeStatValue;
        }
        
        private void OnDestroy() {
            foreach (var statsView in _statsViews) {
                DisposeStatView(statsView);
            }
            _specializationChanger.OnSpecializationChange -= RefreshStats;
        }
        
    }
    
}