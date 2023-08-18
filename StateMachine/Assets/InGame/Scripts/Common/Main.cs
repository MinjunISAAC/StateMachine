// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForCam;
using InGame.ForUI;
using InGame.ForState;

namespace InGame.ForMain 
{ 
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Manage Group")]
        [SerializeField] private UnitController _unitController = null;
        [SerializeField] private CamController  _camController  = null;

        [Space(1.5f)] [Header("UI Group")]
        [SerializeField] private MainUI         _mainUI         = null;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public static Main NullableInstance
        {
            get;
            private set;
        } = null;

        public UnitController UnitController => _unitController;
        public CamController  CamController  => _camController;   
        public MainUI         MainUI         => _mainUI;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake() 
        { 
            NullableInstance = this; 
        }

        private IEnumerator Start()
        {
            // UI 초기화
            _mainUI.OnInit();

            // State 초기화 진행 (Ready)
            StateMachine.Instance.ChangeState(Utility.SimpleFSM.EStateType.Ready, null);

            yield return null;

        }
    }
}