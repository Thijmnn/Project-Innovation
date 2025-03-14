using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HawkTuah : MonoBehaviour
{
    public static HawkTuah instance;

    int samplesize = 128;

    string micro;

    AudioClip _audio;

    public float level;

    public bool isBeingLoud;

    float peak;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        _audio = GetComponent<AudioClip>();

        if (micro == null)
        {
            micro = Microphone.devices[0];

        }

        _audio = Microphone.Start(micro, true, 1, 9999999);

    }


    void Update()
    {

        float[] spectrum = new float[samplesize];

        int mic_pos = Microphone.GetPosition(null) - (samplesize + 1);
        if (mic_pos < 0)
        {
            return;
        }

        _audio.GetData(spectrum, mic_pos);

        for (int i = 0; i < spectrum.Length; i++)
        {

            peak = spectrum[i] * spectrum[i];
            
            if (level <= peak)
            {
                isBeingLoud = true;
            }
            else
            {
                isBeingLoud = false;
            }
        }

    
}
}
