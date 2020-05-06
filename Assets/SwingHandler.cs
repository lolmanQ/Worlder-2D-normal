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
			if (!(ropeLength > Vector3.Distance(transform.position, currentHook.transform.position)))
			{
				if (!(Vector3.Distance(GetNextPos(), currentHook.transform.position) < ropeLength))
				{
					float currentDist = Vector3.Distance(GetNextPos(), currentHook.transform.position);
					float forceScale = currentDist - ropeLength;
					Vector3 forceVector = currentHook.transform.position - transform.position;
					forceVector.Normalize();
					forceVector = forceVector * forceScale;
					rb.velocity = new Vector3(rb.velocity.x + forceVector.x, rb.velocity.y + forceVector.y);
				}
			}
		}
	}

	Vector3 GetNextPos()
	{
		Vector3 nextPos = transform.position + (rb.velocity * Time.deltaTime);
		return nextPos;
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
