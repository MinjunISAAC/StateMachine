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

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public override EStateType State => EStateType.Play;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"[State_Play._Start] Play State�� �����Ͽ����ϴ�.");

            #region    <Main Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"[State_Play._Start] Main�� Null �����Դϴ�.");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"[State_Play._Start] Unit Controller�� Null �����Դϴ�.");
                return;
            }

            _camController = _owner.CamController;
            if (_camController == null)
            {
                Debug.LogError($"[State_Play._Start] Cam Controller�� Null �����Դϴ�.");
                return;
            }

            _playView = (PlayView)_owner.MainUI.GetStateUI();
            if (_playView == null)
            {
                Debug.LogError($"[State_Play._Start] Play View�� Null �����Դϴ�.");
                return;
            }
            #endregion

            // UI �ʱ�ȭ
            _playView.   gameObject.SetActive(true);

            // Unit ���� �ý��� �ʱ�ȭ
            _unitController.OnInit();
            _unitController.UsedJoyPad(true);

            // Cam �ý��� �ʱ�ȭ
            _camController.OnInit(_unitController.TargetUnit);
        }

        protected override void _Update()
        {

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"[State_Play._Finish] Play State���� �������Խ��ϴ�.");
        }
    }
}