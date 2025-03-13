using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HawkTuah : MonoBehaviour
{

    int samplesize = 128;

    string micro;

    AudioClip audio;

    public float level;


    void Start()
    {
        audio = GetComponent<AudioClip>();

        if (micro == null)
        {
            micro = Microphone.devices[0];

        }

        audio = Microphone.Start(micro, true, 1, 441000);

    }


    void Update()
    {

        float[] spectrum = new float[samplesize];

        int mic_pos = Microphone.GetPosition(null) - (samplesize + 1);
        if (mic_pos < 0)
        {

            return;

        }

        audio.GetData(spectrum, mic_pos);

        for (int i = 0; i < spectrum.Length; i++)
        {

            float peak = spectrum[i] * spectrum[i];

            if (level < peak)
            {
                MicrophoneInput.instance.blowCharge++;
            }
            else
            {
                print("Silence");
            }


        }

    
}
}
