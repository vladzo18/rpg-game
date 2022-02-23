using CoreUI;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsView : BaseView {
        
        [SerializeField] private TMP_Text _freeStatsText;
        [SerializeField] private Transform _statViewsContainerTransform;
        [SerializeField] private StatView _statsViewPrefab;

        public TMP_Text FreeStatsText => _freeStatsText;
        public Transform StatViewsContainerTransform => _statViewsContainerTransform;
        public StatView StatsViewPrefab => _statsViewPrefab;
        
    }

}