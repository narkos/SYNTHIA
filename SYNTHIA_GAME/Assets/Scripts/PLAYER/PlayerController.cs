using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AnimationCurve jumpCurve;

    public AnimationCurve returnCurve;

    [SerializeField]
    private Vector3 _origin = Vector2.zero;
    [SerializeField]
    private float   _range = 2.0f;
	[SerializeField]
    private float   _resetForce = 1.0f;

    private Vector3 _startPosition;
    private Vector3 _wantedPosition;
    private Vector3 _wantedDirection;
    private float   _startTime;

    // New members
    private CharacterController _controller;
    private Vector3             _velocity;

    private bool _isReturning = false;
    private bool _isMoving = false;

    private float _journey;
    
    public float jumpDuration = 1.0f;
    public float returnDuration = 1.0f;
    public float returnDelay = 0.5f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f) && Time.time - _startTime > 0.25f)
        {
            _startTime = Time.time;
            _startPosition = transform.position;
            _wantedPosition = transform.position + GetVector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * _range;
            print(_wantedPosition);
            _isMoving = true;
            _isReturning = false;
            _journey = 0;
        }
        
        if(_isMoving)
        {
            _journey += Time.deltaTime;
            if (_journey <= jumpDuration + 0.01f)
            {
                float percent = Mathf.Clamp01(_journey/jumpDuration);
                percent = jumpCurve.Evaluate(percent);
                
                Vector3 nextPos = Vector3.LerpUnclamped(_startPosition, _wantedPosition, percent);
                if(percent == 1.0)
                {
                    print(percent);
                    print("oll " + nextPos);
                }
                //_controller.Move(nextPos);
                transform.position = nextPos;
            } else if (_journey <= jumpDuration + returnDelay){
                transform.position = _wantedPosition;
            }
            else {
                _isMoving = false;
                _isReturning = true;
                _journey = 0.0f;
                _wantedPosition = transform.position;
            }
        }

        if(_isReturning)
        {
            _journey += Time.deltaTime;
            if(_journey <= returnDuration)
            {
                float percent = Mathf.Clamp01(_journey/returnDuration);
                percent = returnCurve.Evaluate(percent);
                transform.position = Vector3.LerpUnclamped(_wantedPosition, _origin, percent);
            } else {
                transform.position = _origin;
                _isReturning = false;
            }
        }
        
    }



    private Vector3 GetVector3(float x, float y)
    {
        return new Vector3(x, y, 0.0f);
    }



    // void Update () {
    //     if ((Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f) && Time.time - _startTime > 0.5f)
    //     {
    //         //StopCoroutine(_jumpRoutine.);
    //         if (_jumpRoutine != null || _returnRoutine != null) 
    //         {
    //             StopCoroutine(_returnRoutine);
    //             StopCoroutine(_jumpRoutine);
    //         }
    //         _startTime = Time.time;
    //         _jumpRoutine = AnimateMove(transform.position, transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * 5.0f, 1.0f);
    //         StartCoroutine(_jumpRoutine);
    //     }

    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         StopCoroutine(_jumpRoutine);
    //         print(_jumpRoutine);
    //     }
    // }
 
    // IEnumerator AnimateMove(Vector3 origin, Vector3 target, float duration)
    // {
    //     float journey = 0.0f;
    //     while (journey <= duration)
    //     {
    //         journey += Time.deltaTime;
    //         float percent = Mathf.Clamp01(journey/duration);
    //         float curvePercent = jumpCurve.Evaluate(percent);
    //         transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);
 
    //         yield return null;
    //     }
    //     print("lasodloas");
    //     _jumpRoutine = null;
    //     _returnRoutine = AnimateReturn(transform.position, _origin, 0.5f);
    //     StartCoroutine(_returnRoutine);
    // }

    // IEnumerator AnimateReturn(Vector3 origin, Vector3 target, float duration)
    // {
    //     float journey = 0.0f;
    //     while (journey <= duration)
    //     {
    //         journey += Time.deltaTime;
    //         float percent = Mathf.Clamp01(journey/duration);
    //         float curvePercent = jumpCurve.Evaluate(percent);
    //         transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);
 
    //         yield return null;
    //     }
    //     _returnRoutine = null;
    // }
}
