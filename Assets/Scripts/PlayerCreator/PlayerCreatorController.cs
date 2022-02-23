using System.IO;
using CoreUI;
using Player.Config;
using PlayerCreator.Apperance;
using PlayerCreator.Specialization;
using PlayerCreator.Stats;
using Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerCreator {
    
    public class PlayerCreatorController : MonoBehaviour {

        [SerializeField] private PlayerCreatorView _playerCreatorView;

        private PlayerConfig _playerConfig;
        private StatsChanger _statsChanger;
        private StatsModel _statsModel;
        private SpecializationChanger _specializationChanger;
        private SpecializationModel _specializationModel;
        private AppearanceChanger _appearanceChanger;
        private AppearanceModel _appearanceModel;

        private IViewController _currentController;
        private CreationTab _currentCreationTab;

        private void Start() {
            _playerConfig = new PlayerConfig();
            
            _statsModel = new StatsModel(_playerConfig.Stats, 10);
            _specializationModel = new SpecializationModel(_playerConfig.Stats);
            _appearanceModel = new AppearanceModel();
            
            _playerConfig.SetAppearanceFeatureSprites(_appearanceModel.AppearanceFeatureSprites);
            
            _statsChanger = new StatsChanger(_playerCreatorView.StatsView);
            _specializationChanger = new SpecializationChanger(_playerCreatorView.PlayerSpecializationView, _playerCreatorView.SpecializationConfigsStorage);
            _appearanceChanger = new AppearanceChanger(_playerCreatorView.PlayerApperence, _playerCreatorView.AppearanceView, _playerCreatorView.ApperenceFeaturesSpritesStorage);
            
            foreach (var button in _playerCreatorView.CreationTabButtons) {
                button.Initialize();
                button.OnButtonClicked += OnTabChanged;
            }
            _playerCreatorView.SaveButton.onClick.AddListener(OnStartGameClicked);
            _playerCreatorView.NameInputField.onValueChanged.AddListener(OnNameChanged);

            _currentCreationTab = CreationTab.Specialization;
            _currentController = GetAndItitializeController(_currentCreationTab);
        }

        private void OnNameChanged(string arg) {
            _playerConfig.Name = arg;
        }

        private void OnTabChanged(CreationTab creationTab) {
            if (creationTab != _currentCreationTab) {
                _playerCreatorView.HeaderText.text = creationTab.ToString();
                _currentController?.Complete();
                _currentController = GetAndItitializeController(creationTab);
                _currentCreationTab = creationTab;
            }
        }

        private IViewController GetAndItitializeController(CreationTab creationTab) {
            switch (creationTab) {
                case CreationTab.Specialization:
                    _specializationChanger.Initialize(_specializationModel);
                    return _specializationChanger;
                case CreationTab.Stats:
                    _statsChanger.Initialize(_statsModel);
                    return _statsChanger;
                case CreationTab.Appearance:
                    _appearanceChanger.Initialize(_appearanceModel);
                    return _appearanceChanger;
                default:
                    return null;
            }
        }
        
        private void OnStartGameClicked() {
            _playerConfig.SetSpecialization(_specializationModel.SpecializationType);
            Serializator.SerializeData(_playerConfig, Path.Combine(Application.dataPath, "Serialization/Player", "PlayerConfig.json"));
            SceneManager.LoadScene(1);
        }

        private void OnDestroy() {
            foreach (var button in _playerCreatorView.CreationTabButtons) {
                button.OnButtonClicked -= OnTabChanged;
            }
            _playerCreatorView.SaveButton.onClick.RemoveListener(OnStartGameClicked);
            _playerCreatorView.NameInputField.onValueChanged.RemoveListener(OnNameChanged);
        }
        
    }

}