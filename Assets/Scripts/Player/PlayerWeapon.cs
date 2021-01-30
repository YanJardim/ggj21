using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : ScriptableObject
{
	[Header("Settings")]
	public new string name;
	public float cooldown = 1;
	[Header("Recurring settings")]
	public bool recurring = false;
	public float frequency = 1;

	[Header("References")]
	public GameObject model;
	
    public abstract void Perform(Vector3 position);
}
