using System.Collections.Generic;
using PlayerCreator.Specialization;

namespace PlayerCreator.Apperance {

    public class AppearanceModel {
        
        public List<AppearanceFeatureSprite> AppearanceFeatureSprites { get; private set; }

        public AppearanceModel() {
            AppearanceFeatureSprites = new List<AppearanceFeatureSprite>();
        }

        public void UpdateAppearanceFeatureSprite(ApperenceFeature apperenceFeature, int spriteIndex) {
            AppearanceFeatureSprite afs = AppearanceFeatureSprites.Find(afs => afs.ApperenceFeature == apperenceFeature);
            if (afs != null) {
                afs.UpdateAppearanceFeatureSprite(apperenceFeature, spriteIndex);
               
            } else {
                AppearanceFeatureSprites.Add(new AppearanceFeatureSprite(apperenceFeature, spriteIndex));
            }
        }
        
    }
}