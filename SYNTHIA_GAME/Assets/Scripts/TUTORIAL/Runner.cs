using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    public float accel;
	public Vector3 jumpVelocity;
    private bool _touchingPlatform;
    public static float distanceTraveled;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		if (_touchingPlatform && Input.GetButtonDown("Jump")) {
			rb.AddForce(jumpVelocity, ForceMode.Impulse);
			_touchingPlatform = false;
		}
        distanceTraveled = transform.localPosition.x;
    }

    void FixedUpdate()
    {
        if (_touchingPlatform)
        {
            rb.AddForce(accel, 0, 0, ForceMode.Acceleration);
        }
        //transform.Translate(5.0f * Time.deltaTime, 0, 0);
    }

    
   void OnCollisionEnter(Collision other)
    {
        _touchingPlatform = true;
    }

    
   void OnCollisionExit(Collision other)
    {
        _touchingPlatform = false;
    }
}
