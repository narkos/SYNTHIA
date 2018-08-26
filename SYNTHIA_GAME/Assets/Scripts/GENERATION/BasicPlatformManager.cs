using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlatformManager : MonoBehaviour {
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;

	public Vector3 minSize;
	public Vector3 maxSize;
	public Vector3 minGap, maxGap;
	[Space]
	public float yRotation, zRotation, minY, maxY;
	[Space]
	public Material[] materials;
	public PhysicMaterial[] physicMaterials;

	private Vector3 _nextPosition;
	private Queue<Transform> objectQueue;
	// Use this for initialization
	void Start () {
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			Transform newObject = (Transform)Instantiate(prefab);
			newObject.transform.parent = transform;
			objectQueue.Enqueue(newObject);
		}
		_nextPosition = startPosition;
		for (int i = 0; i < numberOfObjects; i++) {
			Recycle();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < PlayAreaController.distanceTraveled) {
			Recycle();
		}
	}


	private void Recycle () 
	{
		Vector3 scale = new Vector3(Random.Range(minSize.x, maxSize.x), Random.Range(minSize.y, maxSize.y), Random.Range(minSize.z, maxSize.z));

		Vector3 position = _nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		position.z += scale.z * 0.5f;

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		o.rotation = Quaternion.AngleAxis(zRotation, Vector3.forward);
		o.rotation = Quaternion.AngleAxis(yRotation, Vector3.up);
		int materialIndex = Random.Range(0, materials.Length);
		o.GetComponent<Renderer>().material = materials[materialIndex];
		//o.GetComponent<Collider>().material = physicMaterials[materialIndex];

		//_nextPosition.x += scale.x;
		objectQueue.Enqueue(o);

		_nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));
		if (_nextPosition.y < minY) {
			_nextPosition.y = minY + maxGap.y;
		} else if (_nextPosition.y > maxY) {
			_nextPosition.y = maxY - maxGap.y;
		}
	}
}
