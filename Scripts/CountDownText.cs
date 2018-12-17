using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountDownText : MonoBehaviour {

    public delegate void CountdownFinish();
    public static event CountdownFinish OnCountFinish;
    Text CountText;

    void OnEnable()
    {
        CountText = GetComponent<Text>();
        CountText.text = "3";
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        int count = 3;
        for (int i = 0; i < count; i++)
        {
            CountText.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }        
        OnCountFinish(); 
    }
   
}
