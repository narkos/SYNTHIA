using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	private CharacterController _controller;
	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vel = _controller.velocity;
		_controller.Move(vel + (new Vector3(0, -9.81f, 0)*Time.deltaTime) * Time.deltaTime);
	}
}
