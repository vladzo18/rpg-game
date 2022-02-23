﻿using UnityEngine;

namespace CoreUI {
    
    public abstract class BaseView : MonoBehaviour, ITab {

        [SerializeField] private Canvas _rootCanvas;
        
        public virtual void Show() => _rootCanvas.enabled = true;
        public virtual void Hide() => _rootCanvas.enabled = false;

    }
    
}