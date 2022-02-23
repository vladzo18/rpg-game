using GamePlay;

namespace PlayerCreator.Stats {
    
    public class StatViewData {
        
        public StatController StatController { get; }
        public Stat Stat { get;  }
        public int MinValue { get; }
        
        public StatViewData(StatController statController, Stat stat, int minValue) {
            StatController = statController;
            Stat = stat;
            MinValue = minValue;
        }
        
    }
    
}