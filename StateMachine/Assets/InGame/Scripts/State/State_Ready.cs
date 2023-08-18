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
            Debug.Log($"[State_Ready._Start] Ready State에 진입하였습니다.");

            #region    <Main Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"[State_Ready._Start] Main이 Null 상태입니다.");
                return;
            }

            _readyView = (ReadyView)_owner.MainUI.GetStateUI();
            if (_readyView == null)
            {
                Debug.LogError($"[State_Ready._Start] ReadyView가 Null 상태입니다.");
                return;
            }
            #endregion

            // UI 초기화
            _readyView.gameObject.SetActive(true);
        }

        protected override void _Update() 
        { 
        
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"[State_Ready._Finish] Ready State에서 빠져나왔습니다.");
        }
    }
}