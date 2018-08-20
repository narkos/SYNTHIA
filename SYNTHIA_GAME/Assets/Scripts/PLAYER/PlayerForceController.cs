using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerForceController : MonoBehaviour {

	[SerializeField]
	private float _accel = 5.0f;
	[SerializeField]
	private float _airAccel = 2.5f;

	[SerializeField]
	private float _jumpForce = 10.0f;
	private bool _grounded;

	public static float distanceTraveled;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		
		if (CheckDirectionalInput())
		{
			Vector3 wantedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f).normalized;
			rb.AddForce(wantedDirection * _jumpForce, ForceMode.VelocityChange);
		}

		distanceTraveled = transform.localPosition.x;
	}

	void FixedUpdate()
	{
		if (_grounded)
		{
			rb.AddForce(_accel, 0, 0, ForceMode.Acceleration);
		} else {
			rb.AddForce(_airAccel, 0,0, ForceMode.Acceleration);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		_grounded = true;
	}

	void OnCollisionExit(Collision other)
	{
		_grounded = false;
	}

	private bool CheckDirectionalInput()
	{
		return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W);
	}
}
