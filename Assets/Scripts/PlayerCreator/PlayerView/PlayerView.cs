using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.PlayerView {
    
    public class PlayerView : MonoBehaviour {

        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private GameObject _playerApperenceWindow;
        [SerializeField] private GameObject _playerSpecializationWindow;
        [SerializeField] private GameObject _PlayerCharacteristicsWindow;
        [SerializeField] private Button _toPlayerApperenceButton;
        [SerializeField] private Button _toPlayerSpecializationButton;
        [SerializeField] private Button _toPlayerCharacteristicsButton;
        
        public TMP_Text HeaderText => _headerText;
        public GameObject PlayerApperenceWindow => _playerApperenceWindow;
        public GameObject PlayerSpecializationWindow => _playerSpecializationWindow;
        public GameObject PlayerCharacteristicsWindow => _PlayerCharacteristicsWindow;
        public Button ToPlayerApperenceButton => _toPlayerApperenceButton;
        public Button ToPlayerSpecializationButton => _toPlayerSpecializationButton;
        public Button ToPlayerCharacteristicsButton => _toPlayerCharacteristicsButton;
        
    }
    
}