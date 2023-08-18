// ----- C#
using System;

// ----- Unity
using UnityEngine;
using UnityEngine.EventSystems;

namespace InGame.ForUnit.Control 
{
    public class MovePad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // --------------------------------------------------
        // State Enum
        // --------------------------------------------------
        public enum EMoveState
        {
            Unknown = 0,
            Idle    = 1,
            Walk    = 2,
            Run     = 3,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Active")]
        [SerializeField] private bool          _isActive    = false;
        
        [Space(1.5f)] [Header("RectTransform Group")]
        [SerializeField] private LayerMask     _area        = -1;
        [SerializeField] private RectTransform _RECT_Frame  = null;
        [SerializeField] private RectTransform _RECT_Stick  = null;

        [Space(1.5f)] [Header("Move Value Group")]
        [SerializeField] private float         _moveSpeed   = 0.0f;
        [SerializeField] private float         _rotateSpeed = 0.0f;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const float WALK_AERA       = 95f;
        private const float MOVEFACTOR_WALK = 1f;
        private const float MOVEFACTOR_RUN  = 2f;

        // ----- Private
        private EMoveState  _moveState      = EMoveState.Unknown;

        private Unit        _targetUnit     = null;

        private bool        _isTouchDown    = false;

        private Vector2     _stickVec       = Vector2.zero;
        private Vector2     _nomalVec       = Vector2.zero;

        private Vector3     _moveForceVec   = Vector3.zero;
        private Vector3     _rotateVec      = Vector3.zero;

        private Quaternion  _rotateQuat     = default;

        private float       _joyStickRadius = 0.0f;
        private float       _moveFactor     = 1f;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public float MoveSpeed
        {
            get { return _moveSpeed; }
        }

        public RectTransform FrameRect
        {
            get { return _RECT_Frame; }
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
        // Functions - Event
        // --------------------------------------------------
        private void Update()
        {
            Debug.Log($"Move State {_moveState}");
            if (_isActive == false)  return;
            if (null == _targetUnit) return;
            
            if (_isTouchDown) 
                _OnTouch(Input.mousePosition);
            else
            {
                _targetUnit.UnitRigidBody.velocity = Vector3.zero;
                _RECT_Stick.localPosition          = Vector2.zero;
                _moveState                         = EMoveState.Idle;

            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isTouchDown == true)
                return;

            if ((_area & (1 << eventData.pointerCurrentRaycast.gameObject.layer)) != 0) 
                _isTouchDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isTouchDown == false)
                return;

            _isTouchDown = false;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void SetToTargetUnit(Unit targetUnit)
        {
            _joyStickRadius = _RECT_Frame.rect.width * 0.5f;

            if (null != _targetUnit)
                return;

            _targetUnit = targetUnit;
        }

        // ----- Private
        private void _OnTouch(Vector2 touchVec)
        {
            _stickVec.x               = touchVec.x - _RECT_Frame.position.x;
            _stickVec.y               = touchVec.y - _RECT_Frame.position.y;
            _RECT_Stick.localPosition = Vector2.ClampMagnitude(_stickVec, _joyStickRadius);
            
            _nomalVec = _stickVec.normalized;

            _moveForceVec.x = _nomalVec.x;
            _moveForceVec.y = 0f;
            _moveForceVec.z = _nomalVec.y;
            _moveForceVec   = _moveForceVec * _moveFactor * _moveSpeed;

            if (_isTouchDown) _targetUnit.UnitRigidBody.velocity = _moveForceVec;
            else              _targetUnit.UnitRigidBody.velocity = Vector3.zero;

            if (null == _targetUnit)
                return;

            _ChangeMoveInfo();

            _rotateVec.y = Mathf.Atan2((float)_nomalVec.x, (float)_nomalVec.y) * (float)Mathf.Rad2Deg;
            _rotateQuat = Quaternion.Euler(_rotateVec);
            _targetUnit.transform.rotation = Quaternion.Lerp(_targetUnit.transform.rotation, _rotateQuat, _rotateSpeed);
        }

        private void _ChangeMoveInfo()
        {
            if (_RECT_Stick.localPosition.magnitude < WALK_AERA &&
                _RECT_Stick.localPosition.magnitude > Mathf.Epsilon)
            {
                _moveState  = EMoveState.Walk;
                _moveFactor = MOVEFACTOR_WALK;
            }
            else
            {
                _moveState  = EMoveState.Run;
                _moveFactor = MOVEFACTOR_RUN;
            }
        }
    }
}