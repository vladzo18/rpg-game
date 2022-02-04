using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator {
    [CreateAssetMenu(fileName = "ApperenceFeaturesSpritesStorage", menuName = "PlayerCreator/ApperenceFeatures")]
    public class ApperenceFeaturesSpritesStorage : ScriptableObject {

        [SerializeField] private List<ApperenceFeatureSprites> _apperenceFeatureSpriteses;

        public List<ApperenceFeatureSprites> ApperenceFeatureSpriteses => _apperenceFeatureSpriteses;

    }
}