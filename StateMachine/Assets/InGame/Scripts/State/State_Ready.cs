// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForMain;
using InGame.ForUI;

namespace InGame.ForState 
{
    public class State_Ready : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main _owner = null;

        // ----- UI
        private ReadyView _readyView = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.Ready;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"[State_Ready._Start] Ready State�� �����Ͽ����ϴ�.");

            #region    <Main Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"[State_Ready._Start] Main�� Null �����Դϴ�.");
                return;
            }

            _readyView = (ReadyView)_owner.MainUI.GetStateUI();
            if (_readyView == null)
            {
                Debug.LogError($"[State_Ready._Start] ReadyView�� Null �����Դϴ�.");
                return;
            }
            #endregion

            // UI �ʱ�ȭ
            _readyView.gameObject.SetActive(true);
        }

        protected override void _Update() 
        { 
        
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"[State_Ready._Finish] Ready State���� �������Խ��ϴ�.");
        }
    }
}