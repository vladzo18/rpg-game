using PlayerCreator.PlayerView;
using UnityEngine;

namespace PlayerCreator {
    
    public class PlayerApperenceView : MonoBehaviour, IWindow {

        [SerializeField] private PlayerApperanceElementView _playerApperanceElementView;
        [SerializeField] private Transform _elementsGrid;

        public PlayerApperanceElementView PlayerApperanceElementView => _playerApperanceElementView;
        public Transform ElementsGrid => _elementsGrid;
        
        public void Show() => this.gameObject.SetActive(true);
        public void Hide() => this.gameObject.SetActive(false);

    }
    
}
