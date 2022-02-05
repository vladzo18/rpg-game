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
        public GameObject ApperenceWindow => _apperenceWindow;
        public GameObject SpecializationWindow => _specializationWindow;
        public GameObject CharacteristicsWindow => _characteristicsWindow;
        public Button ApperenceButton => _apperenceButton;
        public Button SpecializationButton => _specializationButton;
        public Button CharacteristicsButton => _characteristicsButton;
        public Button PlayButton => _playButton;
        
    }
    
}