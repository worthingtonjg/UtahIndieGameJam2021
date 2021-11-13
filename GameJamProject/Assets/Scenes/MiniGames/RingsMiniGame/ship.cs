using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ship : MonoBehaviour
{
    private int ringsCount = 0;
    public TMP_Text ringsCollected;

    public AudioSource audioSource;

    public AudioClip clip;
    
    private void OnTriggerEnter(Collider other) 
    {
        print("OnTriggerEnter");
        if(other.tag != "Ring") return;

        other.gameObject.SetActive(false);

        ++ringsCount;

        ringsCollected.text = ringsCount.ToString();
        audioSource.PlayOneShot(clip);
    }
}
