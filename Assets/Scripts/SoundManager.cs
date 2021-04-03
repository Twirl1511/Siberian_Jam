using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
//public class Clip
//{
//    public AudioClip audioClip;
//}


public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    public AudioSource AudioSource;
    public AudioClip WaterUp;
    public AudioClip Toilet;
    public AudioClip StupidFish;
    public AudioClip OMG;
    public AudioClip Splash;
    void Start()
    {
        singleton = this;
    }

    public void PlaySoud(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}

