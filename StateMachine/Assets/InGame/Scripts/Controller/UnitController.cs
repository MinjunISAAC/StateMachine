// ----- C#
using System;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUI;
using InGame.ForUnit.Control;
using InGame.ForUI.Control;

namespace InGame.ForUnit.Manage 
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Joy Pad")]
        [SerializeField] private ControlView _controlView = null;

        [Space(1.5f)][Header("Unit")]
        [SerializeField] private Unit _targetUnit = null;

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
            _SetControlPad();
            _controlView.MovePad.OnChangeMoveStateEvent += (moveType) => { _ChangeUnitState(moveType); };
            _controlView.SetOnClickTapPad(_SetOnClickLeftPunchBtn, _SetOnClickRightPunchBtn);
        }

        public void UsedJoyPad(bool isOn)
        {
            _controlView.gameObject.SetActive(isOn);
            _controlView.UsedJoyStickEvent(isOn);
        }

        // ----- Private
        private void _SetControlPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"[UnitController._SetControlPad] Target Unit이 할당되지 않았습니다.");
                return;
            }

            _controlView.SetToTargetUnit(_targetUnit);
        }

        private void _SetOnClickLeftPunchBtn()
        {
            _targetUnit.ChangeTapInput(Unit.ETapType.Left_Punch);
        }
        
        private void _SetOnClickRightPunchBtn()
        {
            _targetUnit.ChangeTapInput(Unit.ETapType.Right_Punch);
        }

        private void _ChangeUnitState(MovePad.EMoveType moveType)
        {
            switch (moveType)
            {
                case MovePad.EMoveType.Idle:
                    _targetUnit.ChangeToUnitState(Unit.EMoveType.Idle_Empty);
                    break;
                    
                case MovePad.EMoveType.Walk:
                    _targetUnit.ChangeToUnitState(Unit.EMoveType.Walk_Empty);
                    break;

                case MovePad.EMoveType.Run:
                    _targetUnit.ChangeToUnitState(Unit.EMoveType.Run_Empty);
                    break;
            }
        }
    }
}