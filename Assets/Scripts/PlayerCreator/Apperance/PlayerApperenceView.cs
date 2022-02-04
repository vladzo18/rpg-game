using UnityEngine;

namespace PlayerCreator {
    
    public class PlayerApperenceView : MonoBehaviour {

        [SerializeField] private PlayerApperanceElementView _playerApperanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public PlayerApperanceElementView PlayerApperanceElementView => _playerApperanceElementView;
        public Transform ElementsGrid => _elementsGrid;

    }
    
}
