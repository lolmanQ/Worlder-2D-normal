using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTaget : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.TryGetComponent<BulletScript>(out BulletScript bulletS))
		{
			bulletS.BulletHitTarget();
			BulletHit();
		}
	}

	void BulletHit()
	{
		Destroy(this.gameObject, 0.5f);
	}
}
