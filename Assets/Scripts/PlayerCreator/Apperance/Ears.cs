using System;
using UnityEngine;

namespace PlayerCreator {
    
    [Serializable]
    public struct Ears {

        [SerializeField] private SpriteRenderer _leftEar;
        [SerializeField] private SpriteRenderer _righrEar;
        
        public SpriteRenderer LeftEar => _leftEar;
        public SpriteRenderer RighrEar => _righrEar;
        
    }
    
}