using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : TrackTrigger
{
    public Item item;	
	public Item Take(){
		Destroy(this.gameObject);
		return item;
	}
}
