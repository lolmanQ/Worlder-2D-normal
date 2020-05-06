using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pendulum
{
	public Transform bob_tr;
	public Tether tether;
	public Arm arm;
	public Bob bob;

	Vector3 previousPosition;

	public void Initialise()
	{
		bob_tr.transform.parent = tether.tether_tr;
		arm.length = Vector3.Distance(bob_tr.transform.position, tether.position);
	}

	public void MoveBob(Vector3 pos, float time)
	{
		bob.velocity = GetConstrainedVelocity(pos, previousPosition, time);
		Debug.Log(GetConstrainedVelocity(pos, previousPosition, time));
		Debug.Log(Vector3.Distance(pos, tether.position));

		if (Vector3.Distance(pos, tether.position) < arm.length)
		{
			pos = Vector3.Normalize(pos - tether.position) * arm.length;
			arm.length = Vector3.Distance(pos, tether.position);
			bob.rb.transform.localPosition = pos;
		}

		previousPosition = pos;
	}

	public Vector3 GetConstrainedVelocity(Vector3 currentPos, Vector3 previousPosition, float time)
	{
		float distanceToTether;
		Vector3 constrainedPosition;
		Vector3 predictedPosition;

		distanceToTether = Vector3.Distance(currentPos, tether.position);

		Debug.Log(distanceToTether);
		if(distanceToTether > arm.length)
		{
			constrainedPosition = Vector3.Normalize(currentPos - tether.position) * arm.length;
			predictedPosition = (constrainedPosition - previousPosition) * time;
			return predictedPosition;
		}
		return Vector3.zero;
		
	}
}
