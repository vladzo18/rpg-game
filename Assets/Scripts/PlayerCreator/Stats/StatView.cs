using System;
using System.Collections.Generic;
using System.Linq;
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

        public Transform Transform => this.transform;
        public GameObject GameObject => this.gameObject;
        
        private List<StatButton> _statButtons;
        
        public event Action<StatView> OnStateViewIncreaseClicked;
        public event Action<StatView> OnStateViewDecreaseClicked;
        public event Action<StatView, int> OnStateViewValueClicked;
        public event Action<IPoolable> OnReturnToPool;

        public int MaxValue => _statButtons.Count;

        public void Initialize(String statText) {
            _statButtons = _statsButtonsContainer.GetComponentsInChildren<StatButton>().ToList();
            _statHeader.text = statText;
            _decreaseButton.onClick.AddListener(OnDecreseButtonClicked);
            _increaseButton.onClick.AddListener(OnIncreseButtonClicked);
            foreach (var statButton in _statButtons) {
                statButton.Initialize();
                statButton.OnClicked += OnStatButtonClisked;
            }
        }

        public void UpdateView(bool canIncrease, bool canDecrease, int value) {
            _decreaseButton.interactable = canDecrease;
            _increaseButton.interactable = canIncrease;
            ChangeStat(value);
        }

        public void ReturnToPool() {
            Dispose();
            OnReturnToPool?.Invoke(this);
        }
        
        private void SetButtonsState(int value) {
            for (int i = 0; i < _statButtons.Count; i++) {
                _statButtons[i].SetState(i < value);
            }
        }
        
        private void OnIncreseButtonClicked() {
          OnStateViewIncreaseClicked?.Invoke(this);
        }
        
        private void OnDecreseButtonClicked() {
            OnStateViewDecreaseClicked?.Invoke(this);
        }

        private void OnStatButtonClisked(StatButton statButton) {
            OnStateViewValueClicked?.Invoke(this, _statButtons.IndexOf(statButton) + 1);
        }

        private void ChangeStat(int value) {
            _statValue.text = value.ToString();
            SetButtonsState(value);
        }

        private void Dispose() {
            _decreaseButton.onClick.RemoveListener(OnDecreseButtonClicked);
            _increaseButton.onClick.RemoveListener(OnIncreseButtonClicked);
            foreach (var statButton in _statButtons) {
                statButton.OnClicked -= OnStatButtonClisked;
                statButton.Dispose();
            }
        }
        
        private void OnDestroy() {
            Dispose();
        }
        
    }
    
}