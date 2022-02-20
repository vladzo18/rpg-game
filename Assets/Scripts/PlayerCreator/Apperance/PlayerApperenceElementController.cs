using System;
using UnityEngine;

namespace PlayerCreator {
    
    public class PlayerApperenceElementController {

        private PlayerApperanceElementView _playerApperanceElementView;
        private ApperenceFeatureSprites _apperenceFeatureSprites;
        private int _index;

        public int Index {
            get => _index;
            set {
                _index = value;
                changeApperenceElement();
            }
        }
        
        public ApperenceFeature ApperenceFeature => _apperenceFeatureSprites.ApperenceFeature;

        public event Action<ApperenceFeature, Sprite> OnChangeApperenceElement;

        public PlayerApperenceElementController(PlayerApperanceElementView view, ApperenceFeatureSprites sprites) {
            _playerApperanceElementView = view;
            _apperenceFeatureSprites = sprites;
            _playerApperanceElementView.ElementHeader.text = sprites.ApperenceFeature.ToString();
            _playerApperanceElementView.RightArrow.onClick.AddListener(nextElement);
            _playerApperanceElementView.LeftArrow.onClick.AddListener(previousElement);
        }

        private void nextElement() {
            _index++;
            if (_index > _apperenceFeatureSprites.Sprites.Count - 1) {
                _index = 0;
            }
            changeApperenceElement();
        }

        private void previousElement() {
            _index--;
            if (_index < 0) {
                _index = _apperenceFeatureSprites.Sprites.Count - 1;
            }
            changeApperenceElement();
        }

        private void changeApperenceElement() {
            _playerApperanceElementView.StyleHeader.text = $"{_index}";
            OnChangeApperenceElement?.Invoke(_apperenceFeatureSprites.ApperenceFeature, _apperenceFeatureSprites.Sprites[_index]);
        }
        
        public void Dispose() {
            _playerApperanceElementView.RightArrow.onClick.RemoveListener(nextElement);
            _playerApperanceElementView.LeftArrow.onClick.RemoveListener(previousElement);
        }
        
    }
    
}