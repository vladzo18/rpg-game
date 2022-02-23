using System;
using System.Collections.Generic;
using System.Linq;

namespace PlayerCreator.Stats {
    
    public class StatController {

        private readonly StatView _statView;
        private List<StatButton> _statButtons;
        
        public event Action<StatController> OnStateViewIncreaseClicked;
        public event Action<StatController> OnStateViewDecreaseClicked;
        public event Action<StatController, int> OnStateViewValueClicked;
        
        public int MaxValue => _statButtons.Count;
        
        public StatController(StatView statView) {
            _statView = statView;
        }
        
        public void Initialize(String statText) {
            _statButtons = _statView.StatsButtonsContainer.GetComponentsInChildren<StatButton>().ToList();
            _statView.StatHeader.text = statText;
            _statView.DecreaseButton.onClick.AddListener(OnDecreseButtonClicked);
            _statView.IncreaseButton.onClick.AddListener(OnIncreseButtonClicked);
            foreach (var statButton in _statButtons) {
                statButton.Initialize();
                statButton.OnClicked += OnStatButtonClisked;
            }
        }

        public void UpdateView(bool canIncrease, bool canDecrease, int value) {
            _statView.DecreaseButton.interactable = canDecrease;
            _statView.IncreaseButton.interactable = canIncrease;
            ChangeStat(value);
        }
        
        public void Dispose() {
            _statView.DecreaseButton.onClick.RemoveListener(OnDecreseButtonClicked);
            _statView.IncreaseButton.onClick.RemoveListener(OnIncreseButtonClicked);
            foreach (var statButton in _statButtons) {
                statButton.OnClicked -= OnStatButtonClisked;
            }
            _statView.ReturnToPool();
        }
        
        private void ChangeStat(int value) {
            _statView.StatValue.text = value.ToString();
            SetButtonsState(value);
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
        
    }
    
}