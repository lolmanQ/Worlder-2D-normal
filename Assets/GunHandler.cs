using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunHandler : MonoBehaviour
{
	[SerializeField]
	private GunSettings settings;
	public float Angle { get => transform.eulerAngles.z; set => transform.eulerAngles = new Vector3(0, 0, value); }
	[SerializeField]
	private int currentAmmo;

	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private Transform spawnPosBullets;

	[SerializeField]
	private TMPro.TextMeshProUGUI ammoCountText;
	[SerializeField]
	private GameObject reloadingTextObj;

	private float reloadTimer;
	private Vector3 mousePos;
	private bool isReloading = false;
	// Start is called before the first frame update
	void Start()
	{
		currentAmmo = settings.Ammo;
	}

	// Update is called once per frame
	void Update()
	{
		mousePos = new Vector3(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y + 50 - Screen.height/2 );
		mousePos.Normalize();
		CalcAngle();

		if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && !isReloading)
		{
			GameObject bulletCur = Instantiate(bullet);
			bulletCur.GetComponent<BulletScript>().SetupBullet(new Vector3(mousePos.normalized.x, mousePos.normalized.y) * 1000f, spawnPosBullets.position);
			currentAmmo -= 1;
		}

		if (Input.GetKeyDown(KeyCode.R) && !isReloading)
		{
			isReloading = true;
			reloadTimer = settings.ReloadTime;
		}

		if (isReloading)
		{
			reloadingTextObj.SetActive(true);
			reloadTimer -= Time.deltaTime;
			if(reloadTimer < 0)
			{
				isReloading = false;
				reloadingTextObj.SetActive(false);
				currentAmmo = settings.Ammo;
			}
		}
		ammoCountText.SetText(currentAmmo+"");
	}
	/// <summary>
	/// Calculates the angle of the mouse from the center of the screen
	/// </summary>
	void CalcAngle()
	{
		Angle = Vector3.SignedAngle(Vector3.right, mousePos, Vector3.forward);
	}
}
