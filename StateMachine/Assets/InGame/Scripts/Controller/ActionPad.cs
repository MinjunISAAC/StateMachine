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
        Unknown = 0,
        Punch   = 1,
        Jump    = 2,
    }

    // --------------------------------------------------
    // Components
    // --------------------------------------------------
    [SerializeField] private Button _BTN_Punch = null;
    [SerializeField] private Button _BTN_Jump  = null;

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

    public void SetOnClickBtn(Action punchOnClick, Action jumpOnClick)
    {
        _BTN_Punch.onClick.AddListener(() => { punchOnClick?.Invoke(); });
        _BTN_Jump. onClick.AddListener(() => { jumpOnClick?.Invoke();  });
    }
}
