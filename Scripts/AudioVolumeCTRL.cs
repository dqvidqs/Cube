using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeCTRL : MonoBehaviour {
    public Slider Music_Slider;
    public Slider Effect_Slider;

 	void Start(){
    	Music_Slider.value = PlayerPrefs.GetFloat("MusicVolume");
    	Effect_Slider.value = PlayerPrefs.GetFloat("EffectVolume");
 	}

	void Update(){
        PlayerPrefs.SetFloat("MusicVolume", Music_Slider.value);
        PlayerPrefs.SetFloat("EffectVolume", Effect_Slider.value);
 
        Music_Slider.value = PlayerPrefs.GetFloat("MusicVolume");
        Effect_Slider.value = PlayerPrefs.GetFloat("EffectVolume");
 
        PlayerPrefs.Save(); 
    }
}
