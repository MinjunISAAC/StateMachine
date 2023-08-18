// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForMain;
using InGame.ForUI;
using InGame.ForUI.Control;
using InGame.ForUnit.Manage;
using InGame.ForCam;

namespace InGame.ForState
{
    public class State_Play : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main           _owner          = null;

        // ----- Manage
        private UnitController _unitController = null;
        private CamController  _camController  = null;

        // ----- UI
        private PlayView       _playView       = null;
        private ControlView    _controlView    = null;

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

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"[State_Play._Start] Unit Controller가 Null 상태입니다.");
                return;
            }

            _camController = _owner.CamController;
            if (_camController == null)
            {
                Debug.LogError($"[State_Play._Start] Cam Controller가 Null 상태입니다.");
                return;
            }

            _playView = (PlayView)_owner.MainUI.GetStateUI();
            if (_playView == null)
            {
                Debug.LogError($"[State_Play._Start] Play View가 Null 상태입니다.");
                return;
            }

            _controlView = _owner.MainUI.ControlView;
            if (_controlView == null)
            {
                Debug.LogError($"[State_Play._Start] Control View가 Null 상태입니다.");
                return;
            }
            #endregion

            // UI 초기화
            _playView.   gameObject.SetActive(true);
            _controlView.gameObject.SetActive(true);
            _controlView.VisiableControlPad(true);

            // Unit 조작 시스템 초기화
            _unitController.OnInit();
            _controlView.SetToTargetUnit(_unitController.TargetUnit);

            // Cam 시스템 초기화
            _camController.OnInit(_unitController.TargetUnit);
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