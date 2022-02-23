using System.Collections.Generic;
using PlayerCreator.Apperance;
using PlayerCreator.Specialization;
using PlayerCreator.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator {
    
    public class PlayerCreatorView : MonoBehaviour {

        [Header("General")]
        [SerializeField] private List<CreationTabButton> _creationTabButtons;
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private Button _saveButton;
        [SerializeField] private TMP_InputField _nameInputField;
        [Header("Specialization")]
        [SerializeField] private PlayerSpecializationView _playerSpecializationView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        [Header("Appearance")]
        [SerializeField] private AppearanceView _appearanceView;
        [SerializeField] private ApperenceFeaturesSpritesStorage _apperenceFeaturesSpritesStorage;
        [SerializeField] private PlayerApperence _playerApperence;
        [Header("Characteristics")]
        [SerializeField] private StatsView _statsView;
        
        public List<CreationTabButton> CreationTabButtons => _creationTabButtons;
        public TMP_Text HeaderText => _headerText;
        public Button SaveButton => _saveButton;
        public TMP_InputField NameInputField => _nameInputField;
        public PlayerSpecializationView PlayerSpecializationView => _playerSpecializationView;
        public SpecializationConfigsStorage SpecializationConfigsStorage => _specializationConfigsStorage;
        public AppearanceView AppearanceView => _appearanceView;
        public PlayerApperence PlayerApperence => _playerApperence;
        public ApperenceFeaturesSpritesStorage ApperenceFeaturesSpritesStorage => _apperenceFeaturesSpritesStorage;
        public StatsView StatsView => _statsView;
        
    }
    
}