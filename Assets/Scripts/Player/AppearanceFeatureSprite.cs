using System;
using PlayerCreator.Apperance;

namespace PlayerCreator.Specialization {
    
    [Serializable]
    public class AppearanceFeatureSprite {
        
        public ApperenceFeature ApperenceFeature { get; private set; }
        public int SpriteIndex { get; private set; }
        
        public AppearanceFeatureSprite(ApperenceFeature apperenceFeature, int spriteIndex) {
            ApperenceFeature = apperenceFeature;
            SpriteIndex = spriteIndex;
        }

        public void UpdateAppearanceFeatureSprite(ApperenceFeature apperenceFeature, int spriteIndex) {
           ApperenceFeature = apperenceFeature;
           SpriteIndex = spriteIndex;
        }
        
    }
    
}