// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForUI
{
    public class ReadyView : StateView
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("UI Group")]
        [SerializeField] private Button _BTN_Start = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------

        // --------------------------------------------------
        // Fucntions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit(Action onClickAction)
        {
            _BTN_Start.onClick.AddListener(() => { onClickAction?.Invoke(); });
        }
    }
}