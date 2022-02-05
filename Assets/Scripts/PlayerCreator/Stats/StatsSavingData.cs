using System;
using System.Collections.Generic;
using GamePlay;

namespace PlayerCreator.Stats {
    
    [Serializable]
    public class StatsSavingData {
        
        public int Index { get; }
        public List<Stat> Stats { get;  }

        public StatsSavingData(int index, List<Stat> stats) {
            Index = index;
            Stats = stats;
        }
        
    }
    
}