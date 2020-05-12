using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunHandler : MonoBehaviour
{
	public Transform lineSpawnPos;
	public GameObject grapplePoint;
	public Transform firePoint;
	public GameObject gun;
	public SwingHandler swingHandler;
	public GameObject showPoint;
	LineRenderer lineRenderer;
	GameObject player;
	Rigidbody playerRB;
	Vector3 hitPosition;

	float Angle { get => gun.transform.eulerAngles.z; set => gun.transform.eulerAngles = new Vector3(0, 0, value); }
	Vector3 mousePos;

	bool isFired = false, hooked;
	// Start is called before the first frame update
	void Start()
	{
		lineRenderer = GetComponentInChildren<LineRenderer>();
		player = GetComponentInParent<PlayerController>().gameObject;
		playerRB = player.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		mousePos = new Vector3(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y + 50 - Screen.height / 2);
		mousePos.Normalize();
		CalcAngle();
		/*
		if (isFired && hooked)
		{
			Vector3 forceToAdd = (hitPosition - transform.position) * 10;
			forceToAdd.Scale(new Vector3(3, 1, 1));
			playerRB.AddForce(forceToAdd);
		}*/
		if(!isFired)
		{
			grapplePoint.transform.position = firePoint.position;
			Ray showRay = new Ray(firePoint.position, mousePos);
			if (Physics.Raycast(showRay, out RaycastHit showHitInfo, 18))
			{
				if (showHitInfo.transform.CompareTag("Ground"))
				{
					showPoint.transform.position = showHitInfo.point;
					Debug.DrawLine(showRay.origin, showHitInfo.point, Color.cyan, 2);
				}
			}
		}

		if(isFired && hooked && Input.GetMouseButton(0))
		{
			swingHandler.ropeLength -= 2f * Time.deltaTime;
		}

		
		

		if (Input.GetMouseButtonDown(1))
		{

			
			Ray ray = new Ray(firePoint.position, mousePos);
			if(Physics.Raycast(ray, out RaycastHit hitInfo, 18))
			{
				if(hitInfo.transform.CompareTag("Ground"))
				{
					isFired = true;
					hooked = true;
					swingHandler.ActivateGrappler(Vector3.Distance(transform.position, hitInfo.point));
					hitPosition = hitInfo.point;
					grapplePoint.GetComponent<GrappleShot>().hitPos = hitPosition;
					grapplePoint.GetComponent<GrappleShot>().hitTarget = true;
					//GetComponentInParent<PlayerController>().GetComponent<ConfigurableJoint>().connectedBody = grapplePoint.GetComponent<Rigidbody>();
					Debug.DrawLine(ray.origin, hitInfo.point, Color.cyan, 2);
				}
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			isFired = false;
			hooked = false;
			grapplePoint.GetComponent<GrappleShot>().hitTarget = false;
			swingHandler.DeactivateGrappler();
			//lineSpawnPos.gameObject.GetComponent<SpringJoint>().connectedBody = null;
		}
		

		lineRenderer.SetPosition(0, lineSpawnPos.position);
		lineRenderer.SetPosition(1, grapplePoint.transform.position);
	}

	void CalcAngle()
	{
		Angle = Vector3.SignedAngle(Vector3.right, mousePos, Vector3.forward);
	}
}
