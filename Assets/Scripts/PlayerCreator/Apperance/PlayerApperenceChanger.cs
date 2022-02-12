using System.Collections.Generic;
using System.IO;
using Serialization;
using UnityEngine;

namespace PlayerCreator {
    
    public class PlayerApperenceChanger : MonoBehaviour {

        [SerializeField] private PlayerApperence _playerApperence;
        [SerializeField] private PlayerApperenceView _playerApperenceView;
        [SerializeField] private ApperenceFeaturesSpritesStorage _apperenceFeatureSpritesStorage;

        private List<PlayerApperenceElementController> _elementControllers;
        
        private string SavePath => Path.Combine(Application.dataPath, "Serialization/Player", "PlayerApperence.json");

        private void Start() {
            Dictionary<ApperenceFeature, int> dictionary = Serializator.DeserializeData<Dictionary<ApperenceFeature, int>>(SavePath);
            dictionary ??=  dictionary = new Dictionary<ApperenceFeature, int>();
            _elementControllers = new List<PlayerApperenceElementController>();
            
            foreach (var featureSprite in _apperenceFeatureSpritesStorage.ApperenceFeatureSpriteses) {
                PlayerApperanceElementView elementView = Instantiate(_playerApperenceView.PlayerApperanceElementView, _playerApperenceView.ElementsGrid);
                PlayerApperenceElementController elementController = new PlayerApperenceElementController(elementView, featureSprite);
                elementController.OnChangeApperenceElement += OnChangeApperenceElementHandler;
                _elementControllers.Add(elementController);
                
                int index = 0;
                dictionary.TryGetValue(featureSprite.ApperenceFeature, out index);
                elementController.Index = index;
            }
        }

        public void SavePlayerApperenceChanges() {
            Dictionary<ApperenceFeature, int> dictionary = new Dictionary<ApperenceFeature, int>();
            foreach (var apperenceElementController in _elementControllers) {
                dictionary.Add(apperenceElementController.ApperenceFeature, apperenceElementController.Index);
            }
            Serializator.SerializeData(dictionary, SavePath);
        }
        
        private void OnDestroy() {
            foreach (var apperenceElementController in _elementControllers) {
                apperenceElementController.Dispose();
                apperenceElementController.OnChangeApperenceElement -= OnChangeApperenceElementHandler;
            }
        }

        private void OnChangeApperenceElementHandler(ApperenceFeature _apperence, Sprite _sprite) {
            switch (_apperence) {
                case ApperenceFeature.Ears:
                    _playerApperence.ChangeEars(_sprite);
                    break;
                case ApperenceFeature.Eyes:
                    _playerApperence.ChangeEyes(_sprite);
                    break;
                case ApperenceFeature.Hair:
                    _playerApperence.ChangeHair(_sprite);
                    break;
                case ApperenceFeature.Mouth:
                    _playerApperence.ChangeMouth(_sprite);
                    break;
                case ApperenceFeature.Beard:
                    _playerApperence.ChangeBeard(_sprite);
                    break;
                case ApperenceFeature.Eyesbrows:
                    _playerApperence.ChangeEyesbrows(_sprite);
                    break;
            }
            
        }
        
    }
    
}
