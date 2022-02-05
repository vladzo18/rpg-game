using System;
using UnityEngine;

namespace PlayerCreator.PlayerView {
    
    public class PlayerWindowsChanger : MonoBehaviour {

        [SerializeField] private PlayerView playerView;
        [Header("Headers text")]
        [SerializeField] private String _apperenceWindowHeader;
        [SerializeField] private String _specializationHeader;
        [SerializeField] private String _characteristicsHeader;
        
        private void OnEnable() {
            playerView.ApperenceButton.onClick.AddListener(SwitchToPlayerApperenceWindow);
            playerView.SpecializationButton.onClick.AddListener(SwitchToPlayerSpecializationWindow);
            playerView.CharacteristicsButton.onClick.AddListener(SwitchToPlayerCharacteristicsWindow);
        }
        
        private void OnDisable() {
            playerView.ApperenceButton.onClick.RemoveListener(SwitchToPlayerApperenceWindow);
            playerView.SpecializationButton.onClick.RemoveListener(SwitchToPlayerSpecializationWindow);
            playerView.CharacteristicsButton.onClick.RemoveListener(SwitchToPlayerCharacteristicsWindow);
        }

        private void SwitchToPlayerApperenceWindow() {
            HideAllWindows();
            playerView.ApperenceWindow.SetActive(true);
            playerView.HeaderText.text = _apperenceWindowHeader;
        }
        
        private void SwitchToPlayerSpecializationWindow() {
            HideAllWindows();
            playerView.SpecializationWindow.SetActive(true);
            playerView.HeaderText.text = _specializationHeader;;
        }
        
        private void SwitchToPlayerCharacteristicsWindow() {
            HideAllWindows();
            playerView.CharacteristicsWindow.SetActive(true);
            playerView.HeaderText.text = _characteristicsHeader;
        }

        private void HideAllWindows() {
            playerView.ApperenceWindow.SetActive(false);
            playerView.SpecializationWindow.SetActive(false);
            playerView.CharacteristicsWindow.SetActive(false);
        }

    }
    
}