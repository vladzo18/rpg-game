using System;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;

namespace PlayerCreator.Specialization {
    
    public class SpecializationChanger : MonoBehaviour {

        [SerializeField] private PlayerSpecializationView _specializationView;
        [SerializeField] private SpecializationConfigsStorage _specializationConfigsStorage;
        
        private int _currentIndex = 0;
        private List<SkillView> _skillViews;
        private List<StatView> _statViews;
        private ObjectPool _objectPool;

        public event Action<int> OnSpecializationChange;

        private void Start() {
            _skillViews = new List<SkillView>();
            _statViews = new List<StatView>();
            _objectPool = ObjectPool.Instance;
            _specializationView.LeftArrow.onClick.AddListener(previousSpecialization);
            _specializationView.RightArrow.onClick.AddListener(nextSpecialization);
            changeSpecialization();
        }

        private void nextSpecialization() {
            _currentIndex++;
            if (_currentIndex > _specializationConfigsStorage.SpecializationConfigs.Count - 1) {
                _currentIndex = 0;
            }
            changeSpecialization();
        }

        private void previousSpecialization() {
            _currentIndex--;
            if (_currentIndex < 0) {
                _currentIndex = _specializationConfigsStorage.SpecializationConfigs.Count - 1;
            }
            changeSpecialization();
        }
        
        private void changeSpecialization() {
            foreach (var skillView in _skillViews) {
               skillView.ReturnToPool();
            }
            foreach (var statView in _statViews) {
               statView.ReturnToPool();
            }
            _skillViews.Clear();
            _statViews.Clear();
            
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            _specializationView.SpecializationIcon.sprite = config.SpecializationIcon;
            _specializationView.SpetializationName.text = config.SpecializationName;
            _specializationView.Description.text = config.SpecializationDescription;

            foreach (var stat in config.StartStats) {
                StatView statView = _objectPool.GetObject(_specializationView.StatView);
                statView.transform.SetParent(_specializationView.StatContainer);
                statView.transform.localScale = Vector3.one;
                statView.StatAmount.text = stat.Amount.ToString();
                statView.StatType.text = stat.StatType.ToString();
                _statViews.Add(statView);
            }
           
            foreach (var skill in config.StartSkills) {
                SkillView skillView = _objectPool.GetObject(_specializationView.SkillView);
                skillView.transform.SetParent(_specializationView.SkillContainer);
                skillView.transform.localScale = Vector3.one;
                skillView.SkillDescription.text = skill.SkillDescription;
                skillView.SkillName.text = skill.SkillName;
                skillView.SkillImage.sprite = skill.SkillSprite;
                _skillViews.Add(skillView);
            }
            
            OnSpecializationChange?.Invoke(_currentIndex);
        }
        
    }
    
}