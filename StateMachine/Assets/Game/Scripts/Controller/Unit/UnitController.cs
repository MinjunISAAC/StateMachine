// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.UI;

namespace InGame.ForUnit.Manage 
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("JoyPad")]
        [SerializeField] private JoyPadView _joyPadView = null;

        [Space(1.5f)]
        [Header("Unit Collection")]
        [SerializeField] private Unit _targetUnit = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        
        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Unit TargetUnit
        { 
            get { return _targetUnit; }
        }

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        public void Start()
        {
            UsedJoyPad(true);
            _SetJoyPad();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void UsedJoyPad(bool isOn)
        {
            _joyPadView.UsedJoyStickEvent(isOn);

            if (!isOn) _joyPadView.FrameRect.gameObject.SetActive(isOn);
        }

        // ----- Private
        private void _SetJoyPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[UnitController._SetJoyPad] Target Unit이 할당되지 않았습니다.");
                return;
            }

            _joyPadView.SetToTargetUnit(_targetUnit);
        }
    }
}