using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpot : MonoBehaviour
{
    public Item item;
	public static event Action<bool, ItemSpot> OnPlayer;
	
	public Item Take(){
		Destroy(this.gameObject);
		return item;
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			OnPlayer?.Invoke(true, this);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			OnPlayer?.Invoke(false, this);
		}
	}
}
