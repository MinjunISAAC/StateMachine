// ----- C#
using System;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Control;
using InGame.ForUnit;

namespace InGame.ForUI.Control
{
    public class ControlView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private MovePad    _movePad   = null;
        [SerializeField] private ActionPad  _activePad = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Unit _targetUnit = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public MovePad MovePad => _movePad;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void VisiableControlPad(bool isShow)
        {
            _movePad.  gameObject.SetActive(isShow);
            _activePad.gameObject.SetActive(isShow);
        }

        public void SetOnClickTapPad(Action leftPunchOnClick, Action rightPunchOnClick) => _activePad.SetOnClickBtn(leftPunchOnClick, rightPunchOnClick);
        public void UsedJoyStickEvent(bool isOn)                              => _movePad.UsedJoyStickEvent(isOn);
        public void SetToTargetUnit(Unit targetUnit) 
        {
            if (_targetUnit != null)
                return;

            _targetUnit = targetUnit;
            
            _movePad.  SetToTargetUnit(_targetUnit);
            _activePad.SetToTargetUnit(_targetUnit);
        }
    }
}