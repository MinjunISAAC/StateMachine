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
            Debug.LogError("[SimpleStateMachine.OnInit] 초기화 할 State Set이 Null 상태입니다.");
            return;
        }

        if (coroutineExecutor == null)
        {
            Debug.LogError("[SimpleStateMachine.OnInit] 초기화 할 Coroutine 실행자가 Null 상태입니다.");
            return;
        }

        _stateSet          = stateSet;
        _coroutineExecutor = coroutineExecutor;

        foreach (var targetState in _stateSet)
        {
            var state = targetState.Value;
            
            if (state == null)
                continue;

            state.Init(ChangeState, _coroutineExecutor, param);
        }
    }

    public virtual void OnUpdate()
    {
        _currentState?.Update();
    }

    public virtual void OnRelease()
    {
        _currentState      = null;
        _coroutineExecutor = null;

        if (_stateSet != null)
        {
            foreach (var statePair in _stateSet)
            {
                var state = statePair.Value;
                if (state == null)
                    continue;

                state.Release();
            }

            _stateSet.Clear();
        }
    }

    public virtual void ChangeState(EStateType targetStateType, object startParam)
    {
        if (null == _stateSet)
        {
            Debug.LogError("[SimpleStateMachine.ChangeState] State Set이 존재하지 않습니다.");
            return;
        }

        if (!_stateSet.TryGetValue(targetStateType, out var state))
        {
            Debug.LogError($"[SimpleStateMachine.ChangeState] state Set에 {nameof(targetStateType)}가 존재하지 않습니다.");
            return;
        }

        if (null == state)
        {
            Debug.LogError($"[SimpleStateMachine.ChangeState] Target State인 SimpleState[{nameof(targetStateType)}]가 존재하지 않습니다.");
            return;
        }

        var prevSimpleState = _currentState;
        if (null != prevSimpleState)
        {
            _currentState.Finish(targetStateType);

            _currentState = state;
            _currentState.Start(prevSimpleState.State, startParam);
        }
        else
        {
            _currentState = state;
            _currentState.Start(default(EStateType), startParam);
        }
    }
}