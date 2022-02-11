using System.Collections.Generic;
using GamePlay;
using UnityEngine;

namespace PlayerCreator.Specialization {
    
    [CreateAssetMenu(fileName = "SpecializationConfigsStorage", menuName = "PlayerCreator/SpecializationConfigs")]
    public class SpecializationConfigsStorage : ScriptableObject {

        [SerializeField] private List<SpecializationConfig> _specializationConfigs;
        
        public List<SpecializationConfig> SpecializationConfigs => _specializationConfigs;

        public List<Stat> GetStartStats(int classIndex) => _specializationConfigs[0].StartStats;
        
    }
    
}