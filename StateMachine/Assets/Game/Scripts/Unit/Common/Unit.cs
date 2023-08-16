// ----- C#
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
            Unknown = 0,
            Idle_Empty = 1,
            Walk_Empty = 2,
            Run_Empty = 3,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Physic Collection")]
        [SerializeField] private Rigidbody _rigidBody = null;

        [Space(1.5f)][Header("Animate Collection")]
        [SerializeField] private Animator _anim = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private EUnitState _unitState = EUnitState.Unknown;

        private Coroutine _co_CurrntState = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody { get => _rigidBody; }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit() { }


        // ----- Private
        private void _ChangeToUnitState() 
        { 
        
        }

        // ---- State 
        private void _State_IdleEmpty()
        {

        }

        private void _State_WalkEmpty() 
        { 
            
        }
        
        private void _State_RunEmpty() 
        { 
        
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_IdleEmpty() 
        {
            while (_unitState == EUnitState.Idle_Empty)
            {
                yield return null;
            }
        }

        private IEnumerator _Co_WalkEmpty()
        {
            while (_unitState == EUnitState.Idle_Empty)
            {
                yield return null;
            }
        }
        private IEnumerator _Co_RunEmpty() { yield return null; }
    }
}