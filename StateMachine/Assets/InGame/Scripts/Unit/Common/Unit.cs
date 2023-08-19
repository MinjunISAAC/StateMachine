// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit 
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Unit State Enum
        // --------------------------------------------------
        public enum EMoveType
        {
            Unknown    = 0,
            Idle_Empty = 1,
            Walk_Empty = 2,
            Run_Empty  = 3,
        }

        public enum ETapType
        {
            Unknown     = 0,
            Left_Punch  = 1,
            Right_Punch = 2,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Physic Collection")]
        [SerializeField] private Rigidbody _rigidBody = null;

        [Space(1.5f)][Header("Animate Collection")]
        [SerializeField] private Animator  _anim      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private EMoveType _unitState       = EMoveType.Unknown;

        private Coroutine  _co_CurrentState = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody  UnitRigidBody { get => _rigidBody; }
        public EMoveType UnitState     { get => _unitState; }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit() 
        { 
        
        }

        public void ChangeToUnitState(EMoveType unitState, float duration = 0.0f, Action doneCallBack = null) => _ChangeToUnitState(unitState, duration, doneCallBack);

        public void ChangeTapInput(ETapType tapType)
        {
            switch (tapType)
            {
                case ETapType.Left_Punch:
                    _anim.SetTrigger("Left_Punch");
                    break;

                case ETapType.Right_Punch:
                    _anim.SetTrigger("Right_Punch");
                    break;
            }
        }

        // ---- State 
        private void _ChangeToUnitState(EMoveType unitState, float duration = 0.0f, Action doneCallBack = null)
        {
            if (!Enum.IsDefined(typeof(EMoveType), unitState))
            {
                Debug.LogError($"[Unit._ChangeToUnitState] {Enum.GetName(typeof(EMoveType), unitState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            if (_unitState == unitState)
                return;

            _unitState = unitState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_unitState)
            {
                case EMoveType.Idle_Empty: _State_IdleEmpty(); break;
                case EMoveType.Walk_Empty: _State_WalkEmpty(); break;
                case EMoveType.Run_Empty:  _State_RunEmpty();  break;
            }
        }

        private void _State_IdleEmpty()
        {
            _co_CurrentState = StartCoroutine(_Co_IdleEmpty());
        }

        private void _State_WalkEmpty() 
        {
            _co_CurrentState = StartCoroutine(_Co_WalkEmpty());
        }
        
        private void _State_RunEmpty() 
        {
            _co_CurrentState = StartCoroutine(_Co_RunEmpty());
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_IdleEmpty() 
        {
            _anim.SetTrigger($"Empty_Idle");

            while (_unitState == EMoveType.Idle_Empty)
            {
                yield return null;
            }
        }

        private IEnumerator _Co_WalkEmpty()
        {
            _anim.SetTrigger($"Empty_Walk");

            while (_unitState == EMoveType.Walk_Empty)
            {
                yield return null;
            }
        }
        private IEnumerator _Co_RunEmpty()
        {
            _anim.SetTrigger($"Empty_Run");
            while (_unitState == EMoveType.Run_Empty)
            {
                yield return null;
            }
        }
    }
}