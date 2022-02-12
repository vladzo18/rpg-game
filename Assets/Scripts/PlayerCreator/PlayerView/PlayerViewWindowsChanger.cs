using System;
using UnityEngine;

namespace PlayerCreator.PlayerView {
    
    public class PlayerViewWindowsChanger : MonoBehaviour {

        [SerializeField] private PlayerView _playerView;
        [Header("Headers text")]
        [SerializeField] private String _apperenceWindowHeader;
        [SerializeField] private String _specializationHeader;
        [SerializeField] private String _characteristicsHeader;

        private IWindow _activeWindow;

        private void Start() {
            _activeWindow = _playerView.ApperenceWindow;
        }

        private void OnEnable() {
            _playerView.ApperenceButton.onClick.AddListener(SwitchToPlayerApperenceWindow);
            _playerView.SpecializationButton.onClick.AddListener(SwitchToPlayerSpecializationWindow);
            _playerView.CharacteristicsButton.onClick.AddListener(SwitchToPlayerCharacteristicsWindow);
        }
        
        private void OnDisable() {
            _playerView.ApperenceButton.onClick.RemoveListener(SwitchToPlayerApperenceWindow);
            _playerView.SpecializationButton.onClick.RemoveListener(SwitchToPlayerSpecializationWindow);
            _playerView.CharacteristicsButton.onClick.RemoveListener(SwitchToPlayerCharacteristicsWindow);
        }

        private void SwitchToPlayerApperenceWindow() {
            SwitchWindow(_playerView.ApperenceWindow, _apperenceWindowHeader);
        }
        
        private void SwitchToPlayerSpecializationWindow() {
            SwitchWindow(_playerView.SpecializationWindow, _specializationHeader);
        }
        
        private void SwitchToPlayerCharacteristicsWindow() {
            SwitchWindow(_playerView.CharacteristicsWindow, _characteristicsHeader);
        }

        private void SwitchWindow(IWindow targetWindow, string headerText) {
            _playerView.HeaderText.text = headerText;
            _activeWindow.Hide();
            targetWindow.Show();
           _activeWindow = targetWindow;
        }
        
    }
    
}