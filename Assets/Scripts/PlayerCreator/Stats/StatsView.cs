using PlayerCreator.PlayerView;
using TMPro;
using UnityEngine;

namespace PlayerCreator.Stats {
    
    public class StatsView : MonoBehaviour, IWindow {
        
        [SerializeField] private TMP_Text _freeStatsText;
        [SerializeField] private Transform _statViewsContainerTransform;
        [SerializeField] private StatView _statsViewPrefab;

        public TMP_Text FreeStatsText => _freeStatsText;
        public Transform StatViewsContainerTransform => _statViewsContainerTransform;
        public StatView StatsViewPrefab => _statsViewPrefab;
        
        public void Show() => this.gameObject.SetActive(true);
        public void Hide() =>this.gameObject.SetActive(false);
        
    }

}