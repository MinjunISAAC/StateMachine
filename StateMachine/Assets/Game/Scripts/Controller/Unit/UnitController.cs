// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.UI;
using InGame.ForUI;

namespace InGame.ForUnit.Manage 
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Joy Pad")]
        [SerializeField] private JoyPad _joyPad     = null;

        [Space(1.5f)] [Header("Unit")] 
        [SerializeField] private Unit   _targetUnit = null;

        [Space(1.5f)] [Header("UI")]
        [SerializeField] private MainUI _mainUI     = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Unit TargetUnit => _targetUnit;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit() 
        {
            // Unit Chain Event 등록
            _joyPad.OnCheckJoyPadState += 
                (state) => { _ChangeUnitState(state); };

            _SetJoyPad();
        }
        
        public void UsedJoyPad(bool isOn)
        {
            _joyPad.UsedJoyStickEvent(isOn);

            if (!isOn) _joyPad.FrameRect.gameObject.SetActive(isOn);
        }

        // ----- Private
        private void _SetJoyPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[UnitController._SetJoyPad] Target Unit이 할당되지 않았습니다.");
                return;
            }

            _joyPad.SetToTargetUnit(_targetUnit);
        }

        private void _ChangeUnitState(JoyPad.ETouchState joyPadState) 
        {
            switch (joyPadState) 
            {
                case JoyPad.ETouchState.Down:
                    _targetUnit.ChangeToUnitState(Unit.EUnitState.Walk_Empty);
                    break;

                case JoyPad.ETouchState.Walk_Stay:
                    _targetUnit.ChangeToUnitState(Unit.EUnitState.Walk_Empty);
                    break;

                case JoyPad.ETouchState.Run_Stay:
                    _targetUnit.ChangeToUnitState(Unit.EUnitState.Run_Empty);
                    break;

                case JoyPad.ETouchState.Up:
                    _targetUnit.ChangeToUnitState(Unit.EUnitState.Idle_Empty);
                    break;
            }

            _mainUI.ChangeStateTmp(_targetUnit.UnitState);
        }
    }
}