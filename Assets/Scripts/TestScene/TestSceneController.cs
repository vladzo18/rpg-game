using System.IO;
using Player.Config;
using Serialization;
using UnityEngine;

namespace TestScene {
    
    public class TestSceneController : MonoBehaviour {
        
        private PlayerConfig _playerConfig;

        private void Start() {
            _playerConfig = Serializator.DeserializeData<PlayerConfig>(Path.Combine(Application.dataPath, "Serialization/Player", "PlayerConfig.json"));
        }

        private void OnGUI() {
            GUI.Label (new Rect (10, 10, 100, 20), $"Id: {_playerConfig.Id}");
            GUI.Label (new Rect (110, 10, 100, 20), $"Name: {_playerConfig.Name}");
            GUI.Label (new Rect (210, 10, 100, 20), $"Specialization: {_playerConfig.SpecializationType}");

            for (int i = 1; i <= _playerConfig.Stats.Count; i++) {
                GUI.Label (new Rect (10, i * 40, 100, 20), $"{_playerConfig.Stats[i - 1].StatType}: {_playerConfig.Stats[i - 1].Amount}");
            }
            
            for (int i = 1; i <= _playerConfig.AppearanceFeatureSprites.Count; i++) {
                GUI.Label (new Rect (200, i * 40, 100, 20), $"{_playerConfig.AppearanceFeatureSprites[i - 1].ApperenceFeature}: {_playerConfig.AppearanceFeatureSprites[i - 1].SpriteIndex}");
            }
            
        }
        
    }
    
}