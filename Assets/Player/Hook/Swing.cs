using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
	[SerializeField]
	public Pendulum pendulum;

	// Start is called before the first frame update
	void Start()
	{
		pendulum.Initialise();
	}

	// Update is called once per frame
	void Update()
	{
		pendulum.MoveBob(transform.localPosition, Time.deltaTime);
	}
}
