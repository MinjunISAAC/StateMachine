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
        public enum EUnitState
        {
            Unknown    = 0,
            Idle_Empty = 1,
            Walk_Empty = 2,
            Run_Empty  = 3,
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
        private EUnitState _unitState       = EUnitState.Unknown;

        private Coroutine  _co_CurrentState = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody  UnitRigidBody { get => _rigidBody; }
        public EUnitState UnitState     { get => _unitState; }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit() 
        { 
        
        }

        public void ChangeToUnitState(EUnitState unitState, float duration = 0.0f, Action doneCallBack = null) => _ChangeToUnitState(unitState, duration, doneCallBack);

        // ---- State 
        private void _ChangeToUnitState(EUnitState unitState, float duration = 0.0f, Action doneCallBack = null)
        {
            if (!Enum.IsDefined(typeof(EUnitState), unitState))
            {
                Debug.LogError($"[Unit._ChangeToUnitState] {Enum.GetName(typeof(EUnitState), unitState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            if (_unitState == unitState)
                return;

            _unitState = unitState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_unitState)
            {
                case EUnitState.Idle_Empty: _State_IdleEmpty(); break;
                case EUnitState.Walk_Empty: _State_WalkEmpty(); break;
                case EUnitState.Run_Empty:  _State_RunEmpty();  break;
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

            while (_unitState == EUnitState.Idle_Empty)
            {
                yield return null;
            }
        }

        private IEnumerator _Co_WalkEmpty()
        {
            _anim.SetTrigger($"Empty_Walk");

            while (_unitState == EUnitState.Walk_Empty)
            {
                yield return null;
            }
        }
        private IEnumerator _Co_RunEmpty()
        {
            _anim.SetTrigger($"Empty_Run");
            while (_unitState == EUnitState.Run_Empty)
            {
                yield return null;
            }
        }
    }
}