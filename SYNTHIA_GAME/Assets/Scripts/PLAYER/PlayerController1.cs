using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController1 : MonoBehaviour
{

    public AnimationCurve jumpCurve;

    public AnimationCurve returnCurve;

    private Vector3 _startPosition;
    private Vector3 _wantedPosition;
    private Vector3 _wantedDirection;
    private float _startTime;

    // New members
    private CharacterController _controller;
    private Vector3 _velocity;

    [SerializeField]
    private float _gravity;
    public static float distanceTraveled;

    [SerializeField]    private float _acceleration = 10.0f;
    [SerializeField]    private float _deceleration = 150.0f;
    [SerializeField]    private float _maxSpeed = 5.0f;
                        private float _currentMaxSpeed;
    [SerializeField]    private float _jumpSpeed = 100.0f;
    [SerializeField]    private float _disableGravityDuration = 0.5f;
    [SerializeField]    private float _jumpActionDuration;
    private float _lastActionTime;
    private float _lastJumpTime;

    //DEBUG
    [SerializeField] private Text ui_velText;
    [SerializeField] private Text ui_xMax;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _lastJumpTime = _jumpActionDuration + 0.5f;
        _lastActionTime = _disableGravityDuration + 0.5f;
        _currentMaxSpeed = _maxSpeed;
    }

    void OnGUI()
    {
        ui_velText.text = _controller.velocity.ToString();
        ui_xMax.text = _currentMaxSpeed.ToString();
    }

    void Update()
    {
        UpdateTimers();

        distanceTraveled = transform.localPosition.x;
        _wantedDirection = _controller.velocity;

        //

        if ((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f))
        {
            if (Input.GetAxisRaw("Vertical") > 0.0f && _lastJumpTime > 0.2f)
            {
                _wantedDirection.y = _jumpSpeed * Input.GetAxisRaw("Vertical");
                _lastJumpTime = 0.0f;
                _lastActionTime = _disableGravityDuration + 0.5f;
            }
            if (Input.GetAxisRaw("Horizontal") != 0.0f && _lastActionTime > 0.2f)
            {
                _wantedDirection.x += _jumpSpeed * Input.GetAxisRaw("Horizontal");
                _wantedDirection.y = 0.0f;
                _lastActionTime = 0.0f;
                _lastJumpTime = _jumpActionDuration + 0.5f;
                if(!_controller.isGrounded)
                {
                    _currentMaxSpeed += 5.0f;
                    print(_currentMaxSpeed);
                }
            }

        }

        
        if (InActionState())
        {
            _wantedDirection.y = 0.0f;
        } else if (!InJumpActionState()) {
            _wantedDirection.y += _gravity * Time.deltaTime;
        }


        if (!InActionState())
        {
            //_wantedDirection.x = Mathf.Min(Mathf.Abs(_wantedDirection.x) + _acceleration * Time.deltaTime, 20.0f);
            if (_wantedDirection.x > _currentMaxSpeed)
            {
                _wantedDirection.x = _wantedDirection.x - (_deceleration * Time.deltaTime);
            } else {
                _wantedDirection.x = Mathf.Min(Mathf.Abs(_wantedDirection.x) + _acceleration * Time.deltaTime, _currentMaxSpeed);
            }
        }
        _controller.Move(_wantedDirection * Time.deltaTime);
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        print("COLLAJDOR");
    }

    #region Helpers
    private Vector3 GetVector3(float x, float y)
    {
        return new Vector3(x, y, 0.0f);
    }

    private Vector3 GetInputVector()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
    }
    
    private bool InActionState()
    {
        return _lastActionTime <= _disableGravityDuration;
    }

    private bool InJumpActionState()
    {
        return _lastJumpTime <= _jumpActionDuration;
    }

    private void UpdateTimers()
    {
        _lastActionTime += Time.deltaTime;
        _lastJumpTime += Time.deltaTime;
    }
    #endregion
}
