using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject currentTarget;
	public Vector2 offSet;
	public float cameraDistance;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		transform.position = new Vector3(currentTarget.transform.position.x + offSet.x, currentTarget.transform.position.y + offSet.y, cameraDistance);
	}
}
