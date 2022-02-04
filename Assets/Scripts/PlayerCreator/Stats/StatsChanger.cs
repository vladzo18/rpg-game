using System.Collections.Generic;
using GamePlay;
using PlayerCreator.Specialization;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsChanger : MonoBehaviour {

        [SerializeField] private StatsView _statsView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        [SerializeField] private int _freeStats;

        [SerializeField] private SpecializationChanger _specializationChanger;

        private List<StatView> _statsViews;
        private List<StatViewData> _statViewDatas;
        
        private void Start() {
            _statsViews = new List<StatView>();
            _statViewDatas = new List<StatViewData>();
            List<Stat> stats = new List<Stat>();
            foreach (var startStat in _specializationConfigsStorage.SpecializationConfigs[0].StartStats) {
                stats.Add(new Stat(startStat.StatType, startStat.Amount));
            }
            foreach (var startStat in stats) {
                StatView statView = Instantiate(_statsView.StatsViewPrefab, _statsView.StatViewsContainerTransform).GetComponent<StatView>();
                _statsViews.Add(statView);
            }
            
            _statsView.FreeStatsText.text = $"Stats left: {_freeStats}";

            _specializationChanger.OnSpecializationChange += refreasheStats;

            for (int i = 0; i < _statsViews.Count; i++) {
                if (i > stats.Count) break;
                _statsViews[i].Initialize(stats[i].StatType.ToString());
                _statsViews[i].OnStateViewIncreaseClicked += IncreaseStatValue;
                _statsViews[i].OnStateViewDecreaseClicked += DecreaseStatValue;
                _statsViews[i].OnStateViewValueClicked += ChangeStatValue;
                _statViewDatas.Add(new StatViewData(_statsViews[i], stats[i], stats[i].Amount));
            }
            
            UpdateStatViews();
        }

        private void refreasheStats(int index) {
            foreach (var statsView in _statsViews) {
                Destroy(statsView.gameObject);
            }
            _statsViews.Clear();
            
            List<Stat> stats = new List<Stat>();
            foreach (var startStat in _specializationConfigsStorage.SpecializationConfigs[index].StartStats) {
                stats.Add(new Stat(startStat.StatType, startStat.Amount));
            }
            foreach (var startStat in stats) {
                StatView statView = Instantiate(_statsView.StatsViewPrefab, _statsView.StatViewsContainerTransform).GetComponent<StatView>();
                _statsViews.Add(statView);
            }
            _statViewDatas.Clear();
            
            for (int i = 0; i < _statsViews.Count; i++) {
                if (i > stats.Count) break;
                _statsViews[i].Initialize(stats[i].StatType.ToString());
                _statsViews[i].OnStateViewIncreaseClicked += IncreaseStatValue;
                _statsViews[i].OnStateViewDecreaseClicked += DecreaseStatValue;
                _statsViews[i].OnStateViewValueClicked += ChangeStatValue;
                _statViewDatas.Add(new StatViewData(_statsViews[i], stats[i], stats[i].Amount));
            }

            _freeStats = 10;
            _statsView.FreeStatsText.text = $"Stats left: {_freeStats}";
            UpdateStatViews();
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
            if (_freeStats < 0 && value > statViewData.Stat.Amount) return;
            if (value < statViewData.MinValue) return;

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

    }
    
}