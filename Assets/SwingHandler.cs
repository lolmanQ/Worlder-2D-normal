using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHandler : MonoBehaviour
{
	public float ropeLength;
	public bool active = false;
	public Hook currentHook;

	Rigidbody rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	void LateUpdate()
	{
		if (active)
		{
			if (Vector3.Distance(transform.position, currentHook.transform.position) > ropeLength)
			{
				
				// Rope Resolvement position
				Vector3 toPlayerVector = transform.position - currentHook.transform.position;
				toPlayerVector.Normalize();
				toPlayerVector = toPlayerVector * ropeLength;
				transform.position = currentHook.transform.position + toPlayerVector;

				// Rope Resolvement velocity
				Vector3 toAncour = currentHook.transform.position - transform.position;
				toAncour.Normalize();

				Vector2 fDir = new Vector2(toAncour.x, toAncour.y);
				Vector2 fNormal = new Vector2(-fDir.y, fDir.x);
				
				Vector2 vel = new Vector2(rb.velocity.x, rb.velocity.y);

				Vector2 line1p1 = Vector2.zero;
				Vector2 line1p2 = fNormal;

				Vector2 line2p1 = vel;
				Vector2 line2p2 = vel + fDir;

				Vector2 intersection = lineLineIntersection(line1p1, line1p2, line2p1, line2p2);

				Vector2 repulseVector = intersection - vel;

				rb.velocity = new Vector3(rb.velocity.x + repulseVector.x, rb.velocity.y + repulseVector.y);


				//if (Vector3.Distance(GetNextPos(), currentHook.transform.position) > ropeLength)
				//{
				//	float currentDist = Vector3.Distance(GetNextPos(), currentHook.transform.position);
				//	float forceScale = currentDist - ropeLength;
				//	Vector3 forceVector = currentHook.transform.position - transform.position;
				//	forceVector.Normalize();
				//	forceVector = forceVector * forceScale;
				//	rb.velocity = new Vector3(rb.velocity.x + forceVector.x, rb.velocity.y + forceVector.y);
				//}
			}
		}
	}

	Vector3 GetNextPos()
	{
		Vector3 nextPos = transform.position + (rb.velocity * Time.deltaTime);
		return nextPos;
	}

	public static Vector2 lineLineIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
	{
		// Line AB represented as a1x + b1y = c1 
		float a1 = B.y - A.y;
		float b1 = A.x - B.x;
		float c1 = a1 * (A.x) + b1 * (A.y);

		// Line CD represented as a2x + b2y = c2 
		float a2 = D.y - C.y;
		float b2 = C.x - D.x;
		float c2 = a2 * (C.x) + b2 * (C.y);

		float determinant = a1 * b2 - a2 * b1;

		if (determinant == 0)
		{
			// The lines are parallel. This is simplified 
			// by returning a pair of FLT_MAX 
			return new Vector2(float.MaxValue, float.MaxValue);
		}
		else
		{
			float x = (b2 * c1 - b1 * c2) / determinant;
			float y = (a1 * c2 - a2 * c1) / determinant;
			return new Vector2(x, y);
		}
	}

	public void ActivateGrappler(float length)
	{
		ropeLength = length;
		active = true;
	}

	public void DeactivateGrappler()
	{
		active = false;
	}
}
