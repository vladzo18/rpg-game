using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats {
    
    public class StatButton : MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        
        public event Action<StatButton> OnClicked;

        public void Initialize() {
            _button.onClick.AddListener(ButtonClicked);
        }

        public void SetState(bool active) {
            if (active) {
                _image.color = Color.cyan;
            } else {
                _image.color = Color.white;
            }
        }

        public void Dispose() {
            _button.onClick.RemoveListener(ButtonClicked);
        }
        
        private void OnDestroy() {
            Dispose();
        }
        
        private void ButtonClicked() {
            OnClicked?.Invoke(this);
        }
        
    }
    
}
