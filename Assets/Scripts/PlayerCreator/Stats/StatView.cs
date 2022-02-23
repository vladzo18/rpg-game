using System;
using ObjectPooling;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Stats {
    
    public class StatView : MonoBehaviour, IPoolable {

        [SerializeField] private Transform _statsButtonsContainer;
        [SerializeField] private Button _decreaseButton;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private TMP_Text _statHeader;
        [SerializeField] private TMP_Text _statValue;

        public Transform StatsButtonsContainer => _statsButtonsContainer;
        public Button DecreaseButton => _decreaseButton;
        public Button IncreaseButton => _increaseButton;
        public TMP_Text StatHeader => _statHeader;
        public TMP_Text StatValue => _statValue;
        
        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        
        public event Action<IPoolable> OnReturnToPool;
        
        public void ReturnToPool() {
            OnReturnToPool?.Invoke(this);
        }
        
    }
    
}