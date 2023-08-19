// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForUnit;

public class ActionPad : MonoBehaviour
{
    // --------------------------------------------------
    // Enum Action Enum
    // --------------------------------------------------
    public enum EActionType
    {
        Unknown    = 0,
        LeftPunch  = 1,
        RightPunch = 2,
    }

    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Button _BTN_LeftPunch  = null;
    [SerializeField] private Button _BTN_RightPunch = null;

    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    private Unit _targetUnit = null;

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    public void SetToTargetUnit(Unit targetUnit)
    {
        if (null != _targetUnit)
            return;

        _targetUnit = targetUnit;
    }

    public void SetOnClickBtn(Action leftPunchOnClick, Action rightPunchOnClick)
    {
        _BTN_LeftPunch. onClick.AddListener(() => { leftPunchOnClick?.Invoke();  });
        _BTN_RightPunch.onClick.AddListener(() => { rightPunchOnClick?.Invoke(); });
    }
}
