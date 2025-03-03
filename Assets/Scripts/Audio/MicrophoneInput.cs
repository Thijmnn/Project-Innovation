using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System.Linq;
using TMPro;

public class MicrophoneInput : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] micNameHolder;
    void Start()
    {
        for(int i = 0; i < micNameHolder.Length; i++)
        {
            micNameHolder[i].text = Microphone.devices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

        /*AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        Microphone.End("Built-in Microphone");

        audioSource.Play();
        audioSource.clip = null;*/
        

        foreach (var device in Microphone.devices)
        {
            print("name: " + device);
        }
    }
}
