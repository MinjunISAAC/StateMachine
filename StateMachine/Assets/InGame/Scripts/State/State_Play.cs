// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForMain;

namespace InGame.ForState
{
    public class State_Play : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main _owner = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.Play;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"[State_Play._Start] Play State에 진입하였습니다.");

            #region    <Main Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"[State_Play._Start] Main이 Null 상태입니다.");
                return;
            }
            #endregion
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"[State_Play._Finish] Play State에서 빠져나왔습니다.");
        }
    }
}