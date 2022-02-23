using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoreUI;
using Serialization;
using UnityEngine;

namespace PlayerCreator.Apperance {
    
    public class AppearanceChanger : IViewController {

        private readonly AppearanceView _appearanceView;
        private readonly PlayerApperence _playerApperence;
        private readonly ApperenceFeaturesSpritesStorage _apperenceFeatureSpritesStorage;
        private AppearanceModel _appearanceModel;

        private List<PlayerApperenceElementController> _elementControllers;
        
        private string SavePath => Path.Combine(Application.dataPath, "Serialization/Player", "PlayerApperence.json");
        private Dictionary<ApperenceFeature, int> dictionary;
        
        public AppearanceChanger(PlayerApperence playerApperence, AppearanceView appearanceView, ApperenceFeaturesSpritesStorage apperenceFeatureSpritesStorage) {
            _playerApperence = playerApperence;
            _appearanceView = appearanceView;
            _apperenceFeatureSpritesStorage = apperenceFeatureSpritesStorage;
            
            dictionary = Serializator.DeserializeData<Dictionary<ApperenceFeature, int>>(SavePath);
            dictionary ??= new Dictionary<ApperenceFeature, int>();
            _elementControllers = new List<PlayerApperenceElementController>();
            
            foreach (var featureSprite in _apperenceFeatureSpritesStorage.ApperenceFeatureSpriteses) {
                PlayerApperanceElementView elementView = GameObject.Instantiate(_appearanceView.PlayerApperanceElementView, _appearanceView.ElementsGrid);
                PlayerApperenceElementController elementController = new PlayerApperenceElementController(elementView, featureSprite);
                elementController.OnChangeApperenceElement += OnChangeApperenceElementHandler;
                _elementControllers.Add(elementController);

                int index = 0;
                dictionary.TryGetValue(featureSprite.ApperenceFeature, out index);
                elementController.Index = index;
            }
        }

        public void Initialize(params object[] args) {
            if (args == null || args.Length < 1 || !args.Any(arg => arg is AppearanceModel)) {
                throw new NullReferenceException($"There is no args for {nameof(AppearanceChanger)}");
            }

            object model = args.First(arg => arg is AppearanceModel);
            _appearanceModel = model as AppearanceModel;
            
            foreach (var elementController in _elementControllers) {
                elementController.OnChangeApperenceElement += OnChangeApperenceElementHandler;
                elementController.Initialize();
            }

            _appearanceView.Show();
        }
        
        public void Complete() {
            _appearanceView.Hide();
            foreach (var elementController in _elementControllers) {
                elementController.OnChangeApperenceElement -= OnChangeApperenceElementHandler;
                elementController.Dispose();
            }
        }
        
        public void SavePlayerApperenceChanges() {
            Dictionary<ApperenceFeature, int> dictionary = new Dictionary<ApperenceFeature, int>();
            foreach (var apperenceElementController in _elementControllers) {
                dictionary.Add(apperenceElementController.ApperenceFeature, apperenceElementController.Index);
            }
            Serializator.SerializeData(dictionary, SavePath);
        }
        
        private void OnChangeApperenceElementHandler(ApperenceFeature apperence, Sprite sprite, int index) {
            switch (apperence) {
                case ApperenceFeature.Ears:
                    _playerApperence.ChangeEars(sprite);
                    break;
                case ApperenceFeature.Eyes:
                    _playerApperence.ChangeEyes(sprite);
                    break;
                case ApperenceFeature.Hair:
                    _playerApperence.ChangeHair(sprite);
                    break;
                case ApperenceFeature.Mouth:
                    _playerApperence.ChangeMouth(sprite);
                    break;
                case ApperenceFeature.Beard:
                    _playerApperence.ChangeBeard(sprite);
                    break;
                case ApperenceFeature.Eyesbrows:
                    _playerApperence.ChangeEyesbrows(sprite);
                    break;
            }

            if (_appearanceModel != null) {
                _appearanceModel.UpdateAppearanceFeatureSprite(apperence, index); 
            }
        }
        
    }
    
}
