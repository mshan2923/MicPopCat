using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicScript : MonoBehaviour
{
    AudioSource _audio;

    public float sensitivity = 100;
    public float loudness = 0;

    public float OverVaule = 3;
    public bool Over = false;
    bool DoOnce = false;

    /*
    void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audio.clip = Microphone.Start(null, true, 1, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        //_audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        loudness = GetAveragedVolume() * sensitivity;
    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.clip.GetData(data, 0);
        //_audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }*///자기 목소리 들림

    public AudioClip aud;
    int SampleRate = 44100;
    float[] samples;

    public delegate void Speak(bool b);
    Speak speak;

    private void Start()
    {
        samples = new float[SampleRate];
        aud = Microphone.Start(null, true, 1, SampleRate);
    }
    private void Update()
    {
        aud.GetData(samples, 0);
        float sum = 0;
        for(int i = 0; i < samples.Length; i++)
        {
            sum += samples[i] * samples[i];
        }
        loudness = Mathf.Sqrt(sum / samples.Length) * sensitivity;


        if(loudness > OverVaule)
        {
            if(DoOnce == false)
            {
                DoOnce = true;
                if(speak != null)
                    speak.Invoke(true);
                Over = true;
            }
        }else
        {
            if(DoOnce)
            {
                DoOnce = false;
                if (speak != null)
                    speak.Invoke(false);
                Over = false;
            }
        }
    }
    public void SetUp(Speak SpeakEvent)
    {
        speak = SpeakEvent;
    }
}
