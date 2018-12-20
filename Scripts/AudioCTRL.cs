using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCTRL : MonoBehaviour {
	private AudioSource src;
	// Use this for initialization
	void Start () {
		src = GetComponent<AudioSource>();
		src.volume = PlayerPrefs.GetFloat("MusicVolume");
	}
}
