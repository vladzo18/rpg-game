﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoreUI;
using ObjectPooling;
using UnityEngine;

namespace PlayerCreator.Specialization {
    
    public class SpecializationChanger : IViewController {

        private readonly PlayerSpecializationView _specializationView;
        private readonly SpecializationConfigsStorage _specializationConfigsStorage;
        
        private int _currentIndex;
        private List<SkillView> _skillViews;
        private List<StatView> _statViews;
        private ObjectPool _objectPool;
        private SpecializationModel _specializationModel;

        public event Action<int> OnSpecializationChange;

        public SpecializationChanger(PlayerSpecializationView specializationView, SpecializationConfigsStorage specializationConfigsStorage) {
            _specializationView = specializationView;
            _specializationConfigsStorage = specializationConfigsStorage;
            _skillViews = new List<SkillView>();
            _statViews = new List<StatView>();
            _objectPool = ObjectPool.Instance;
        }
        
        public void Initialize(params object[] args) {
            if (args == null || args.Length < 1 || !args.Any(arg => arg is SpecializationModel)) {
                throw new NullReferenceException($"There is no args for {nameof(SpecializationChanger)}");
            }

            object model = args.First(arg => arg is SpecializationModel);
            _specializationModel = model as SpecializationModel;
            
            _specializationView.LeftArrow.onClick.AddListener(previousSpecialization);
            _specializationView.RightArrow.onClick.AddListener(nextSpecialization);
            _specializationView.Show();
            ChangeSpecialization();
        }

        public void Complete() {
            ClearView();
            _specializationView.LeftArrow.onClick.RemoveListener(previousSpecialization);
            _specializationView.RightArrow.onClick.RemoveListener(nextSpecialization);
            _specializationView.Hide();
        }

        private void nextSpecialization() {
            _currentIndex++;
            if (_currentIndex > _specializationConfigsStorage.SpecializationConfigs.Count - 1) {
                _currentIndex = 0;
            }
            ChangeSpecialization();
        }

        private void previousSpecialization() {
            _currentIndex--;
            if (_currentIndex < 0) {
                _currentIndex = _specializationConfigsStorage.SpecializationConfigs.Count - 1;
            }
            ChangeSpecialization();
        }
        
        private void ChangeSpecialization() {
            ClearView();
            
            SpecializationConfig config = _specializationConfigsStorage.SpecializationConfigs[_currentIndex];
            _specializationView.SpecializationIcon.sprite = config.SpecializationIcon;
            _specializationView.SpetializationName.text = config.SpecializationName;
            _specializationView.Description.text = config.SpecializationDescription;
            _specializationModel.ChangeSpecialization(config.SpecializationType, config.StartStats);

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

        private void ClearView() {
            foreach (var skillView in _skillViews) {
                skillView.ReturnToPool();
            }
            foreach (var statView in _statViews) {
                statView.ReturnToPool();
            }
            _skillViews.Clear();
            _statViews.Clear();
        }
        
    }
    
}