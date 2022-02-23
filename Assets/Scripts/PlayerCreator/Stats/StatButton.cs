using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats {
    
    public class StatButton : MonoBehaviour {

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        private bool _isInitialized;
        
        public event Action<StatButton> OnClicked;

        public void Initialize() {
            if (_isInitialized) return;
            _button.onClick.AddListener(ButtonClicked);
            _isInitialized = true;
        }

        public void SetState(bool active) {
            if (active) {
                _image.color = Color.cyan;
            } else {
                _image.color = Color.white;
            }
        }
        
        private void ButtonClicked() {
            OnClicked?.Invoke(this);
        }
        
        private void OnDestroy() {
            _button.onClick.RemoveListener(ButtonClicked);
        }
        
    }
    
}
