using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using TMPro;
using UnityEngine.Audio;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField] GameObject freakBird;

    [SerializeField] TextMeshProUGUI audioInputText;

    [SerializeField] AudioSource _audioSource;

    [SerializeField] private AudioMixerGroup _micMix;
    [SerializeField] private AudioMixerGroup _masterMix;

    [SerializeField] bool playClip;

    private float loudnessSensibility = 1000;
    private int loudnessThreshold = 600;
    int sampleWindow = 64;
    string microphoneName;

    public float blowCharge;
    void Start()
    {
        microphoneName = Microphone.devices[0].ToString();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _micMix;
        _audioSource.clip = Microphone.Start(microphoneName, true, 20, AudioSettings.outputSampleRate);
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (playClip)
        {
            PlayBackClip();
            playClip = false;
        }

        if (Mathf.RoundToInt(GetLoudnessFromAudioClip(_audioSource.timeSamples, _audioSource.clip) * loudnessSensibility) > loudnessThreshold)
        {
            /*audioInputText.text = Mathf.RoundToInt(GetLoudnessFromAudioClip(_audioSource.timeSamples, _audioSource.clip) * loudnessSensibility).ToString();*/
            blowCharge++;
        }
        freakBird.transform.localScale = new Vector3(blowCharge, blowCharge, freakBird.transform.localScale.z);
        print(Mathf.RoundToInt(GetLoudnessFromAudioClip(_audioSource.timeSamples, _audioSource.clip) * loudnessSensibility).ToString());
    }

    public float GetLoudnessFromAudioClip(int clipposition, AudioClip audioClip)
    {
        int startposition = clipposition - sampleWindow;
        if(startposition < 0)
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];

        audioClip.GetData(waveData, startposition);
        
        float totalLoudness = 0;

        for(int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
        }
        
        return totalLoudness / sampleWindow; 
    }

    private void PlayBackClip()
    {
        _audioSource.outputAudioMixerGroup = _masterMix;
        _audioSource.Play();
    }
}
