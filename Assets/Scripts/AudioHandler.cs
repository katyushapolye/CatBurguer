using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioHandler : MonoBehaviour
{


    public static AudioHandler instance;
    public Audio[] Audios = new Audio[10];


    private void Awake()
    {
        gameObject.name = "AudioHandler";


        foreach (Audio Audio in Audios)
        {

            //maybe delete audios that are null or dont have a name <- good ideia actually
            Audio.Source = gameObject.AddComponent<AudioSource>();
            Audio.Source.clip = Audio.AudioClip;
            Audio.Source.name = Audio.Name;
            Audio.Source.loop = Audio.loop;
            Audio.Source.volume = Audio.Volume;


        }
    }
    public void Play(string Audioname)
    {
        if(Menu.isAudioEnabled == false)
        {
            return;
        }
        Audio s = Array.Find(Audios, Audio => Audio.Name == Audioname);
        if (s == null)
        {
            Debug.Log("Audio Not Found //" + Audioname);
            return;
        }
        s.Source.Play();

    }

    public void Stop(string Audioname)
    {

        Audio s = Array.Find(Audios, Audio => Audio.Name == Audioname);
        if (s == null)
        {
            Debug.Log("Audio Not Found //" + Audioname);
            return;
        }
        s.Source.Stop();
    }

    public void StopAllAudio()
    {
        foreach(Audio a in Audios)
        {
            a.Source.Stop();
        }
    }






}