using UnityEngine;

namespace PlayerCreator.Apperance {
    
    public class PlayerApperence : MonoBehaviour {

        [SerializeField] private SpriteRenderer _heirSprite;
        [SerializeField] private SpriteRenderer _beardSprite;
        [SerializeField] private SpriteRenderer _eyesSprite;
        [SerializeField] private SpriteRenderer _eyesbrows;
        [SerializeField] private SpriteRenderer _mouth;
        [SerializeField] private Ears _ears;
        
        public void ChangeEars(Sprite sprite) {
            _ears.LeftEar.sprite = sprite;
            _ears.RighrEar.sprite = sprite;
        }
        public void ChangeEyes(Sprite sprite) => _eyesSprite.sprite = sprite;
        public void ChangeMouth(Sprite sprite) => _mouth.sprite = sprite;
        public void ChangeHair(Sprite sprite) => _heirSprite.sprite = sprite;
        public void ChangeBeard(Sprite sprite) => _beardSprite.sprite = sprite;
        public void ChangeEyesbrows(Sprite sprite) => _eyesbrows.sprite = sprite;
        
    }
    
}
    

