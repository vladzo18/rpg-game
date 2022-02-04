using System;
using UnityEngine;

namespace PlayerCreator.PlayerView {
    
    public class PlayerViewChanger : MonoBehaviour {

        [SerializeField] private PlayerView _playerView;
        [SerializeField] private String _playerApperenceWindowsName;
        [SerializeField] private String _playerPlayerSpecializationName;
        [SerializeField] private String _playerPlayerCharacteristicsName;
        
        private void OnEnable() {
            _playerView.ToPlayerApperenceButton.onClick.AddListener(SwitchToPlayerApperenceWindow);
            _playerView.ToPlayerSpecializationButton.onClick.AddListener(SwitchToPlayerSpecializationWindow);
            _playerView.ToPlayerCharacteristicsButton.onClick.AddListener(SwitchToPlayerCharacteristicsWindow);
        }
        
        private void OnDisable() {
            _playerView.ToPlayerApperenceButton.onClick.RemoveListener(SwitchToPlayerApperenceWindow);
            _playerView.ToPlayerSpecializationButton.onClick.RemoveListener(SwitchToPlayerSpecializationWindow);
            _playerView.ToPlayerCharacteristicsButton.onClick.RemoveListener(SwitchToPlayerCharacteristicsWindow);
        }

        private void SwitchToPlayerApperenceWindow() {
            HideAllWindows();
            _playerView.PlayerApperenceWindow.SetActive(true);
            _playerView.HeaderText.text = _playerApperenceWindowsName;
        }
        
        private void SwitchToPlayerSpecializationWindow() {
            HideAllWindows();
            _playerView.PlayerSpecializationWindow.SetActive(true);
            _playerView.HeaderText.text = _playerPlayerSpecializationName;;
        }
        
        private void SwitchToPlayerCharacteristicsWindow() {
            HideAllWindows();
            _playerView.PlayerCharacteristicsWindow.SetActive(true);
            _playerView.HeaderText.text = _playerPlayerCharacteristicsName;
        }

        private void HideAllWindows() {
            _playerView.PlayerApperenceWindow.SetActive(false);
            _playerView.PlayerSpecializationWindow.SetActive(false);
            _playerView.PlayerCharacteristicsWindow.SetActive(false);
        }

    }
    
}