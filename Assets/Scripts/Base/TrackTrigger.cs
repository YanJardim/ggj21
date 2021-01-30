using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrackTrigger : MonoBehaviour
{
	public static event Action<bool, GameObject> OnPlayer;

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			OnPlayer?.Invoke(true, this.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			OnPlayer?.Invoke(false, this.gameObject);
		}
	}
}
