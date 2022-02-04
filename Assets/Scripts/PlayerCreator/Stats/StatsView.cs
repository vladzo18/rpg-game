using TMPro;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsView : MonoBehaviour {

        //[SerializeField] private List<StatView> _statsViews;
        
        [SerializeField] private TMP_Text _freeStatsText;
        [SerializeField] private Transform _statViewsContainerTransform;
        [SerializeField] private GameObject _statsViewPrefab;

        public TMP_Text FreeStatsText => _freeStatsText;
        public Transform StatViewsContainerTransform => _statViewsContainerTransform;
        public GameObject StatsViewPrefab => _statsViewPrefab;
/*
        private int _freeStats;
        private List<StatViewData> _statViewDatas;
        
        private void Start() {
            List<Stat> stats = new List<Stat>() {
                new Stat(StatType.Agility, 2),
                new Stat(StatType.Inteligence, 1),
                new Stat(StatType.Strength, 1)
            };
            _statViewDatas = new List<StatViewData>();
            
            _freeStats = 10;
            _freeStatsText.text = $"Stats left: {_freeStats}";

            for (int i = 0; i < _statsViews.Count; i++) {
                _statsViews[i].Initialize(stats[i].StatType.ToString());
                _statsViews[i].OnStateViewIncreaseClicked += IncreaseStatValue;
                _statsViews[i].OnStateViewDecreaseClicked += DecreaseStatValue;
                _statsViews[i].OnStateViewValueClicked += ChangeStatValue;
                _statViewDatas.Add(new StatViewData(_statsViews[i], stats[i], stats[i].Amount));
                if (i == stats.Count - 1) {
                    break;
                }
            }
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
            _freeStatsText.text = $"Stats left: {_freeStats}";
            statViewData.Stat.SetValue(value);
            UpdateStatViews();
        }

        private void UpdateStatViews() {
            foreach (var statViewData in _statViewDatas) {
                int value = statViewData.Stat.Amount;
                statViewData.StatView.UpdateView(_freeStats > 0 && value < statViewData.StatView.MaxValue, value > statViewData.MinValue, value);
            }
        }
        */
    }

}