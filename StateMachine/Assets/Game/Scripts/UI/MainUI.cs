// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using TMPro;
using InGame.ForUnit;

namespace InGame.ForUI 
{ 
    public class MainUI : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("State UI")]
        [SerializeField] private TextMeshProUGUI _TMP_UnitState = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void ChangeStateTmp(Unit.EUnitState unitState) 
        {
            _TMP_UnitState.text = unitState.ToString();
        }
    }
}