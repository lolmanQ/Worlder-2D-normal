using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gun settings")]
public class GunSettings : ScriptableObject
{
	public int Ammo;
	public float Firerate;
	public float ReloadTime;
}