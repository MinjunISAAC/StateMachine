// ----- C#
using System;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForState;
using InGame.ForUI.Control;

namespace InGame.ForUI 
{
    public class MainUI : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("State UI Group")]
        [SerializeField] private ReadyView   _readyView   = null;
        [SerializeField] private PlayView    _playView    = null;

        // --------------------------------------------------
        // Function - Nomal
        // --------------------------------------------------
        // ----- Public 
        public void OnInit()
        {
            _readyView.OnInit
            (
                () =>
                {
                    StateMachine.Instance.ChangeState(Utility.SimpleFSM.EStateType.Play, null);
                    _readyView.gameObject.SetActive(false);
                }
            );

            _readyView.  gameObject.SetActive(false);
            _playView.   gameObject.SetActive(false);
        }

        public StateView GetStateUI() 
        { 
            var currentState = StateMachine.Instance.CurrentState;
            switch (currentState) 
            {
                case Utility.SimpleFSM.EStateType.Ready: return _readyView;
                case Utility.SimpleFSM.EStateType.Play:  return _playView;
                case Utility.SimpleFSM.EStateType.End:   return _readyView;
                default:                                 return null;
            }
        }
    }
}