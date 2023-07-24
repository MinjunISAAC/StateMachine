// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;

public class SimpleStateMachine<EStateType>
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    protected MonoBehaviour                                   _coroutineExecutor = null;
    protected Dictionary<EStateType, SimpleState<EStateType>> _stateSet          = null;
    protected SimpleState<EStateType>                         _currentState      = null;

    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public virtual EStateType CurrentState
    {
        get
        {
            return _currentState.State;
        }
    }

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Public
    public virtual void OnInit(Dictionary<EStateType, SimpleState<EStateType>> stateSet,
                               MonoBehaviour                                   coroutineExecutor,
                               object                                          param)
    {
        OnRelease();

        if (stateSet == null)
        {
            Debug.LogError("[SimpleStateMachine.OnInit] �ʱ�ȭ �� State Set�� Null �����Դϴ�.");
            return;
        }

        if (coroutineExecutor == null)
        {
            Debug.LogError("[SimpleStateMachine.OnInit] �ʱ�ȭ �� Coroutine �����ڰ� Null �����Դϴ�.");
            return;
        }

        _stateSet          = stateSet;
        _coroutineExecutor = coroutineExecutor;
    }

    public virtual void OnUpdate()
    {
        _currentState?.Update();
    }

    public virtual void OnRelease()
    {
        _currentState      = null;
        _coroutineExecutor = null;
    }
}