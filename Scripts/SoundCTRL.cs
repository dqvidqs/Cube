using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCTRL : MonoBehaviour {

	public static AudioClip jumpSound, coinSound, deadSound;
	static AudioSource source;
	// Use this for initialization
	void Start () {
		jumpSound = Resources.Load<AudioClip>("Jump");
		coinSound = Resources.Load<AudioClip>("Coin");
		deadSound = Resources.Load<AudioClip>("Dead");
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound(string clip){
		switch(clip){
		case "Jump":
			source.PlayOneShot(jumpSound);
			break;
		case "Coin":
			source.PlayOneShot(coinSound);
			break;
		case "Dead":
			source.PlayOneShot(deadSound);
			break;
		}
	}
}
