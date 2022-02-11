using PlayerCreator.Stats;
using UnityEngine;

namespace PlayerCreator.PlayerView {
    
    public class PlayerViewConfigurationSaver : MonoBehaviour {

        [SerializeField] private PlayerView _playerView;
        [SerializeField] private PlayerApperenceChanger _playerApperenceChanger;
        [SerializeField] private StatsChanger _statsChanger;

        private void Start() {
            _playerView.PlayButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() {
            _playerApperenceChanger.SavePlayerApperenceChanges();
            _statsChanger.SaveStats();
        }

        private void OnDestroy() {
            _playerView.PlayButton.onClick.RemoveListener(OnPlayButtonClick);
        }
        
    }
    
}