using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShot : MonoBehaviour
{
	public Vector3 hitPos;
	public bool hitTarget;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (hitTarget)
		{
			transform.position = hitPos;
		}
	}
}
