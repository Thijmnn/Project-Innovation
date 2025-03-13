using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using TMPro;
using UnityEngine.Audio;
using Unity.VisualScripting;
using UnityEngine.UI;


public class MicrophoneInput : MonoBehaviour
{
    public static MicrophoneInput instance { get; private set; }

    [HeaderAttribute("References")]
    [SerializeField] GameObject freakBird;
    [SerializeField] Animator birdExpand;

    [SerializeField] private GameObject startButton;

    [HeaderAttribute("Audio")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioSource _startAudioSource;
    [SerializeField] private AudioMixerGroup _micMix;
    [SerializeField] private int loudnessThreshold = 800;


    [SerializeField] public float maxCharge;

    private float _maxCharge;

    public float blowCharge;

    private int startRecordingLength = 4;

    private float loudnessSensibility = 1000;

    int sampleWindow = 64;
    
    string microphoneName;

    

    [SerializeField, HideInInspector] public bool blown;

    public bool once;
    void Start()
    {
        GameManager.gameRestart += Reset;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
        microphoneName = Microphone.devices[0].ToString();
        _audioSource.outputAudioMixerGroup = _micMix;
        _startAudioSource.outputAudioMixerGroup = _micMix;
    }

    void Update()
    {
        if (_startAudioSource.isPlaying)
        {
            AnimatePlayer();
            GetMaxCharge(maxCharge, loudnessThreshold);
        }
        else if(!_startAudioSource.isPlaying && !_audioSource.isPlaying && once)
        {
            _maxCharge = maxCharge;
            SetMaxCharge(blowCharge);
            blown = true;
        }
        else if (_audioSource.isPlaying)
        {
            AnimatePlayer();
            RechargeOstrich(loudnessThreshold);
        }
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
    private void GetMaxCharge(float _totalMaxCharge, float _loudnessThreshold)
    {
        if (Mathf.RoundToInt(GetLoudnessFromAudioClip(_startAudioSource.timeSamples, _startAudioSource.clip) * loudnessSensibility) > _loudnessThreshold)
        {
            if (blowCharge < _totalMaxCharge) {
                
                blowCharge++;
            }
        }
    }
    private void SetMaxCharge(float _maxCharge)
    {
        maxCharge = _maxCharge;
    }
    private void RechargeOstrich(float _loudnessThreshold)
    {
        if (Mathf.RoundToInt(GetLoudnessFromAudioClip(_audioSource.timeSamples, _audioSource.clip) * loudnessSensibility) > _loudnessThreshold)
        {
            if (blowCharge < maxCharge)
            {
                blowCharge++;
            }
        }
    }
    private void AnimatePlayer()
    {
        SetAnimationFrame(birdExpand);
    }
    public void StartBlowing()
    {
        _startAudioSource.clip = Microphone.Start(microphoneName, false, startRecordingLength, AudioSettings.outputSampleRate);
        StartCoroutine(PlayAudioClip(_startAudioSource));
    }
    public void RecordInGame()
    {
        _audioSource.clip = Microphone.Start(microphoneName, false, 3599, AudioSettings.outputSampleRate);
        StartCoroutine(PlayAudioClip(_audioSource));
    }
    private IEnumerator PlayAudioClip(AudioSource _audioSource)
    {
        bool first = true;
        while (true)
        {
            if (first)
            {
                first = false;
                yield return new WaitForSeconds(0.1f);
            }
            _audioSource.Play();
            once = true;
            yield break;
        }
    }
    private void SetAnimationFrame(Animator anim)
    {
        for (float i = 14; i > 1; i--)
        {
            if (blowCharge <= maxCharge / i && blowCharge > maxCharge / i - 1)
            {
                while (true)
                {
                    float animState = 1 / i;
                    anim.Play("Base Layer.OstrichExpand", 0, animState);
                    break;
                }
            }
            else if(blowCharge == 0)
            {
                anim.Play("Base Layer.OstrichExpand", 0, 0f);
            }
        }
    }

    public void Reset()
    {
        birdExpand.Play("Base Layer.OstrichExpand",0,0f);
        blowCharge = 0f;
        _audioSource.Stop();
        blown = false;
        once = false;
        maxCharge = _maxCharge;
        startButton.SetActive(true);
    }
}