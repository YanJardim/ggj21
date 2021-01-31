using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
	public AudioSource sfxSource;
    
	public void PlaySFX(AudioClip clip){
		sfxSource.PlayOneShot(clip);
	}
}
