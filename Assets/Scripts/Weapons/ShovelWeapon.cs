using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shovel", menuName = "Data/Weapons/Shovel")]
public class ShovelWeapon : PlayerWeapon
{
	public override void Perform(Vector3 position)
	{
		Debug.Log("Dig");
	}
}
