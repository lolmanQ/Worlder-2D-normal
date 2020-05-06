using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
	public GameObject hitEffect;
	Rigidbody rb;
	Vector3 vel;
	int bounceCount;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		Destroy(this.gameObject, 10);
		rb.AddForce(vel);
	}

	// Update is called once per frame
	void Update()
	{
		if(bounceCount <= 0)
		{
			Destroy(this.gameObject, 0.2f);
		}
	}

	public void SetupBullet(Vector3 direction, Vector3 pos)
	{
		vel = direction;
		transform.position = pos;
		bounceCount = 3;
	}

	public void BulletHitTarget()
	{
		GameObject boomEffect = Instantiate(hitEffect, transform.position, transform.rotation);
		Destroy(boomEffect, 2);
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		bounceCount -= 1;
	}
}
