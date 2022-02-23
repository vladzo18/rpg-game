using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerCreator.Apperance {
    
    public class PlayerApperanceElementView : MonoBehaviour {

        [SerializeField] private TMP_Text _elementHeader;
        [SerializeField] private TMP_Text _styleHeader;
        [SerializeField] private Button _leftArrow;
        [SerializeField] private Button _rightArrow;

        public TMP_Text ElementHeader => _elementHeader;
        public TMP_Text StyleHeader => _styleHeader;
        public Button LeftArrow => _leftArrow;
        public Button RightArrow => _rightArrow;

    }
    
}
