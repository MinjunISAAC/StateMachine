// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit 
{ 
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Physic Collection")]
        [SerializeField] private Rigidbody _rigidBody = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody { get => _rigidBody; }
    }
}