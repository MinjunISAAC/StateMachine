// ----- C#
using InGame.ForUnit;
using System;

// ----- Unity
using UnityEngine;

namespace InGame.ForJoyStick
{
    public class JoyPad : MonoBehaviour
    {
        // --------------------------------------------------
        // User Touch State Enum
        // --------------------------------------------------
        public enum ETouchState
        {
            Unknown   = 0,
            Down      = 1,
            Up        = 2,
            Walk_Stay = 3,
            Run_Stay  = 4,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Activate Collection")]
        [SerializeField] private bool          _isActived   = true;    // JoyPad 활성화 여부     

        [Space(1f)]
        [Header("RectTransform Collection")]
        [SerializeField] private RectTransform _frameRect   = null;    // Joy Pad 외각 프레임 
        [SerializeField] private RectTransform _stickRect   = null;    // Joy Pad 중앙 스틱 

        [Space(1f)]
        [Header("Origin Value Collection")]
        [SerializeField] private float         _moveSpeed   = 10f;
        [SerializeField] private float         _rotateSpeed = 0.5f;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Private
        private Unit        _targetUnit     = null;

        private ETouchState _touchState     = ETouchState.Unknown;

        private Vector2     _stickVec       = Vector2.zero;
        private Vector2     _nomalVec       = Vector2.zero;

        private Vector3     _moveForceVec   = Vector3.zero;
        private Vector3     _rotateVec      = Vector3.zero;

        private Quaternion  _rotateQuat     = default;

        private float       _joyStickRadius = 0.0f;
        private float       _moveFactor     = 1f;

        private bool        _isDragging     = false;

        // --------------------------------------------------
        // JoyPad - Unit Chain Event
        // --------------------------------------------------
        public event Action<ETouchState> OnCheckJoyPadState;
        public void CheckUnitState(ETouchState joyPadState)
        {
            if (OnCheckJoyPadState != null)
                OnCheckJoyPadState(joyPadState);
        }

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public ETouchState TouchState
        {
            get { return _touchState; }
        }

        public float MoveSpeed
        {
            get { return _moveSpeed; }
        }

        public RectTransform FrameRect
        {
            get { return _frameRect; }
        }

        // --------------------------------------------------
        // JoyStick Factor Event
        // --------------------------------------------------
        public event Action<bool> OnUsedJoyStickEvent;
        public void UsedJoyStickEvent(bool isUsed)
        {
            if (OnUsedJoyStickEvent != null)
                OnUsedJoyStickEvent(isUsed);
        }


        // --------------------------------------------------
        // Function - Event
        // --------------------------------------------------
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _touchState         = ETouchState.Down;
                _isDragging         = true;
                _frameRect.position = Input.mousePosition;

                CheckUnitState(_touchState);
                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _ChangeMoveType();
                _frameRect.gameObject.SetActive(true);

                CheckUnitState(_touchState);
                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (null == _targetUnit) return;
                if (!_isActived)         return;

                _touchState                        = ETouchState.Up;
                _isDragging                        = false;
                _targetUnit.UnitRigidBody.velocity = Vector3.zero;

                CheckUnitState(_touchState);
                _frameRect.gameObject.SetActive(false);
                _stickRect.localPosition = Vector2.zero;
            }
        }

        // --------------------------------------------------
        // Function - Nomal
        // --------------------------------------------------
        // ----- Public
        public void SetToTargetUnit(Unit targetUnit)
        {
            _joyStickRadius = _frameRect.rect.width * 0.5f;

            if (null != _targetUnit)
                return;

            _targetUnit = targetUnit;
        }

        // ----- Private
        private void _OnTouch(Vector2 touchVec)
        {
            _stickVec.x = touchVec.x - _frameRect.position.x;
            _stickVec.y = touchVec.y - _frameRect.position.y;
            _stickRect.localPosition = Vector2.ClampMagnitude(_stickVec, _joyStickRadius);

            _nomalVec = _stickVec.normalized;

            _moveForceVec.x = _nomalVec.x;
            _moveForceVec.y = 0f;
            _moveForceVec.z = _nomalVec.y;
            _moveForceVec = _moveForceVec * _moveFactor * _moveSpeed;

            if (_isDragging) _targetUnit.UnitRigidBody.velocity = _moveForceVec;
            else _targetUnit.UnitRigidBody.velocity = Vector3.zero;

            if (null == _targetUnit)
                return;

            _rotateVec.y = Mathf.Atan2((float)_nomalVec.x, (float)_nomalVec.y) * (float)Mathf.Rad2Deg;
            _rotateQuat = Quaternion.Euler(_rotateVec);
            _targetUnit.transform.rotation = Quaternion.Lerp(_targetUnit.transform.rotation, _rotateQuat, _rotateSpeed);
        }

        private void _ChangeMoveType() 
        {
            if (Vector2.Distance(Vector2.zero, _stickRect.localPosition) <= 80)
            {
                _touchState = ETouchState.Walk_Stay;
                _moveFactor = 1f;
            }
            else 
            { 
                _touchState = ETouchState.Run_Stay;
                _moveFactor = 3f;
            }
        }
    }
}