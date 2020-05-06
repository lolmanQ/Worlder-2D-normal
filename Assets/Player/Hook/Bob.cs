using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bob
{
	public Rigidbody rb;
	public Vector3 velocity { get => rb.velocity; set { rb.velocity = rb.velocity + value; } }

}
