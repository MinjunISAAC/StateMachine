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
    public class State_End : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main _owner = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.End;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"[State_End._Start] End State�� �����Ͽ����ϴ�.");

            #region    <Main Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"[State_End._Start] Main�� Null �����Դϴ�.");
                return;
            }
            #endregion
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"[State_End._Finish] End State���� �������Խ��ϴ�.");
        }
    }
}