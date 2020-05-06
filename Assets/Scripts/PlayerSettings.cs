using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "player settings")]
public class PlayerSettings : ScriptableObject
{
	public int amountOfJumps;
	
	public float moveDamp;
	public float moveSlow;
	public float speed;
	
	public float maxSpeed;
	public float sprintAmp;
	public float jumpSpeed;
	
	public float gravity;

	public float dashCooldown;
	public float dashPower;
	public float dashLength;

	public float glideSpeed;
}
