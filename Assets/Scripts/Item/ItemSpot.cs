using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : TrackTrigger
{
    public Item item;
	public GameObject top;
	public ParticleSystem particle;
	public Item Take(){
		if(!item) return null;
		var auxItem = item;
		item = null;
		Destroy(top);
		return auxItem;
	}

	public void StartDig(){
		particle.Play();
	}
	public void StopDig(){
		particle.Stop();
	}
}
