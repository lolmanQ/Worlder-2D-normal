using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckHandler : MonoBehaviour
{
	PlayerController playerController;
	void Start()
	{
		playerController = GetComponentInParent<PlayerController>();
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ground")
		{
			//playerController.onGround = true;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.tag == "Ground")
		{
			//playerController.onGround = false;
		}
	}
}
