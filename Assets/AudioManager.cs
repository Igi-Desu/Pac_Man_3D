using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    private static AudioSource sauce;
    void Start(){
        sauce=GetComponent<AudioSource>();
    }

    public static void Play()
    {
        sauce.Play();
    }
}

