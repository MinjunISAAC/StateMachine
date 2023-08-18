// ----- C#
using System.Collections;
using System.Collections.Generic;

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
        [SerializeField] private MovePad _movePad = null;
        [SerializeField] private TapPad  _tapPad  = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void VisiableControlPad(bool isShow)
        {
            _movePad.gameObject.SetActive(isShow);
            _tapPad. gameObject.SetActive(isShow);
        }

        public void SetToTargetUnit(Unit targetUnit) => _movePad.SetToTargetUnit(targetUnit);
        public void UsedJoyStickEvent(bool isOn) => _movePad.UsedJoyStickEvent(isOn);
    }
}