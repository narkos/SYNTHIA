using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private float _moveSpeed = 10.0f;
    [SerializeField] private float _maxSpeed = 20.0f;
    [SerializeField] private float _jumpSpeed = 100.0f;
    [SerializeField] private float _disableGravityTime = 0.5f;
    private float _lastActionTime;
    private float _lastJumpTime;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _startTime = _disableGravityTime + 0.5f;
    }

    void Update()
    {
        UpdateTimers();

        distanceTraveled = transform.localPosition.x;
        _wantedDirection = _controller.velocity;

        //

        if ((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f))
        {
            //_wantedDirection += GetInputVector().normalized * _moveSpeed * Time.deltaTime;
            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                _wantedDirection.y = _jumpSpeed * Input.GetAxisRaw("Vertical");
                _lastJumpTime = 0.0f;
            }
            if (Input.GetAxisRaw("Horizontal") != 0.0f)
            {
                _wantedDirection.x += _jumpSpeed * Input.GetAxisRaw("Horizontal");
                _wantedDirection.y = 0.0f;
                _lastActionTime = 0.0f;
            }

        }

        
        if (InActionState())
        {
            _wantedDirection.y = 0.0f;
        }


        if (!InActionState())
        {
            _wantedDirection.x = Mathf.Min(Mathf.Abs(_wantedDirection.x) + _moveSpeed * Time.deltaTime, 20.0f);
            if (_wantedDirection.x >= _maxSpeed)
            {
                _wantedDirection.x = _wantedDirection.x - (15.0f * Time.deltaTime);
            }
        }
        print(_wantedDirection.y);
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
        return _lastActionTime <= _disableGravityTime;
    }

    private bool InJumpActionState()
    {
        return _lastJumpTime <= _disableGravityTime;
    }

    private void UpdateTimers()
    {
        _lastActionTime += Time.deltaTime;
        _lastJumpTime += Time.deltaTime;
    }
    #endregion
}
