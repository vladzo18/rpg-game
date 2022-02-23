using System.Collections.Generic;
using GamePlay;

namespace PlayerCreator.Specialization {
    
    public class SpecializationStats {
        
        public List<Stat> Stats { get; }
        public SpecializationType SpecializationType { get; private set; }

        public SpecializationStats(List<Stat> stats, SpecializationType specializationType) {
            Stats = stats;
            SpecializationType = specializationType;
        }
        
    }
    
}