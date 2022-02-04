using System;
using UnityEngine;

namespace GamePlay {
    
    [Serializable]
    public class Stat {

        [SerializeField] private StatType _statType;
        [SerializeField] private int _amount;

         public StatType StatType => _statType;
         public int Amount => _amount;
         
         public Stat(StatType statType, int value) {
             _statType = statType;
             _amount = value;
         }

         public void SetValue(int value) {
             _amount = value;
         }
         
    }
    
}