using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.PlayerView {
    public class PlayerView : MonoBehaviour {
        
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private GameObject _apperenceWindow;
        [SerializeField] private GameObject _specializationWindow;
        [SerializeField] private GameObject _characteristicsWindow;
        [SerializeField] private Button _apperenceButton;
        [SerializeField] private Button _specializationButton;
        [SerializeField] private Button _characteristicsButton;
        [SerializeField] private Button _playButton;

        public TMP_Text HeaderText => _headerText;
        public IWindow ApperenceWindow => _apperenceWindow.GetComponent<IWindow>();
        public IWindow SpecializationWindow => _specializationWindow.GetComponent<IWindow>();
        public IWindow CharacteristicsWindow => _characteristicsWindow.GetComponent<IWindow>();
        public Button ApperenceButton => _apperenceButton;
        public Button SpecializationButton => _specializationButton;
        public Button CharacteristicsButton => _characteristicsButton;
        public Button PlayButton => _playButton;

        #if UNITY_EDITOR
        private void OnValidate() {
            if (_apperenceWindow.GetComponent<IWindow>() == null) _apperenceWindow = null;
            if (_specializationWindow.GetComponent<IWindow>() == null) _specializationWindow = null;
            if (_characteristicsWindow.GetComponent<IWindow>() == null) _characteristicsWindow = null;
        }
        #endif
        
    }
}