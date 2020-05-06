using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	private float timeFromStart = 0;
	private float timeLength = 0;
	private float lastTime = 0;
	private bool isActive = false;

	public float Time { get => timeFromStart; }
	
	public void Start()
	{
		lastTime = timeFromStart;
		isActive = true;
		timeFromStart = 0;
	}

	public void Pause()
	{
		isActive = false;
	}

	public void Resume()
	{
		isActive = true;
	}

	public float End()
	{
		isActive = false;
		return timeFromStart;
	}

	public void Update(float timePast)
	{
		if (isActive)
		{
			timeFromStart += timePast;
		}
	}
}
