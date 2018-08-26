using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAreaController : MonoBehaviour {

	private PlayerControllerBoxed _player;
	private CharacterController _controller;

	[SerializeField] private float _maxVelocity_x = 2.0f;
	[SerializeField] private float _currentVelocity_x;

	[SerializeField] private float _gravity = -9.81f;

	static public float distanceTraveled;
	[Header("GUI")]
	[SerializeField] private Text _velGUI;
	[SerializeField] private Text _maxGUI;
	[SerializeField] private Text _magnitudeGUI;

	// Use this for initialization
	void Start () {
		_player = GetComponentInChildren<PlayerControllerBoxed>();	
		_controller = GetComponent<CharacterController>();
	}

	public void RegisterDash(float power)
	{

	}
	

	// Update is called once per frame
	void Update () {
		Vector3 diff = _player.transform.position - transform.position;
		Vector3 nextMove = _controller.velocity;

		if (diff.magnitude > 3.0f)
		{
			//print (diff);
			if (_magnitudeGUI != null) _magnitudeGUI.text = diff.magnitude.ToString();
			nextMove += diff.normalized * 14.0f * Time.deltaTime;
		}

		nextMove.y += _gravity * Time.deltaTime;
		nextMove.z = 0.0f;
		_controller.Move(nextMove * Time.deltaTime);

		distanceTraveled = transform.position.x;
	}

	void OnGUI()
	{
		if(_velGUI != null)
		{
			_velGUI.text = _controller.velocity.ToString();
		}

		if (_maxGUI != null)
		{
			_maxGUI.text = _maxVelocity_x.ToString();
		}

		if (_magnitudeGUI != null)
		{
			//_magnitudeGUI.text = _m
		}
	}
}
