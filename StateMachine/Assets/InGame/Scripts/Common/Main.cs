// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit;
using InGame.ForUnit.Control;

namespace InGame.Main 
{ 
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Unit _unit = null;
        [SerializeField] private JoyPad  _joyPad  = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            _joyPad.SetToTargetUnit(_unit);
        }
    }
}