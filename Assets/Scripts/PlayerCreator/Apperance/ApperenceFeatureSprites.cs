using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCreator.Apperance {
    
    [Serializable]
    public class ApperenceFeatureSprites {
        
        [SerializeField] private ApperenceFeature _apperenceFeature;
        [SerializeField] private List<Sprite> _sprites;

        public ApperenceFeature ApperenceFeature => _apperenceFeature;
        public List<Sprite> Sprites => _sprites;

    }
    
}