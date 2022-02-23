using CoreUI;
using UnityEngine;

namespace PlayerCreator.Apperance {
    
    public class AppearanceView : BaseView {

        [SerializeField] private PlayerApperanceElementView _playerApperanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public PlayerApperanceElementView PlayerApperanceElementView => _playerApperanceElementView;
        public Transform ElementsGrid => _elementsGrid;
        
    }
    
}
