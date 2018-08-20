using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	private Transform _target;
	[SerializeField]
	private Vector3 _viewDistance;
	[SerializeField]
	private float _damp = 0.5f;

	private Vector3 _velocity = Vector3.zero;

	void Start () {
		if (_target == null)
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		SmoothFollow();	
	}

	private void SmoothFollow()
	{
		Vector3 wantedPosition = _target.position;
		wantedPosition += _viewDistance;
		Vector3 currentPosition = Vector3.SmoothDamp(transform.position, wantedPosition, ref _velocity, _damp);
		transform.position = currentPosition;
	}
}
