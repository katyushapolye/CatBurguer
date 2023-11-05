using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Audio
{
    public string Name;
    public AudioClip AudioClip;
    [Range(0.0f, 10.0f)]
    public float Volume = 1.0f;
    [HideInInspector]
    public AudioSource Source;
    public bool loop;

}
