using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 400f;
    [Range(0, 1)] [SerializeField] private float _crouchSpeed = .36f;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing = .05f;
    [SerializeField] private bool _airControl = false;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private Transform _ceillingCheck;
    [SerializeField] private Collider2D _crouchDisableColider;

    const float _groundRadius = .2f;
    private bool _grounded;
    const float _ceillingRadius = .2f;
    private Rigidbody2D _rigidbody2d;
    private bool _facingRight = true;
    private Vector3 _velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool _wasCrouching = false;

    private void Awake()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }
    private void FixedUpdate()
    {
        bool wasGrounded = _grounded;
        _grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundRadius, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(_ceillingCheck.position, _ceillingRadius, _whatIsGround))
            {
                crouch = true;
            }
        }
        if (_grounded || _airControl)
        {
            if (crouch)
            {
                if (!_wasCrouching)
                {
                    _wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                move *= _crouchSpeed;

                if (_crouchDisableColider != null)
                    _crouchDisableColider.enabled = false;
            }
            else
            {
                if (_crouchDisableColider != null)
                    _crouchDisableColider.enabled = true;
                if (_wasCrouching)
                {
                    _wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, _rigidbody2d.velocity.y);
            _rigidbody2d.velocity = Vector3.SmoothDamp(_rigidbody2d.velocity, targetVelocity, ref _velocity, _movementSmoothing);

            if (move > 0 && !_facingRight)
            {
                Flip();
            }
            else if (move < 0 && _facingRight)
            {
                Flip();
            }
        }

        if (_grounded && jump)
        {
            _grounded = false;
            _rigidbody2d.AddForce(new Vector2(0f, _jumpForce));
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
