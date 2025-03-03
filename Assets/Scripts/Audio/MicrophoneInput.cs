using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Android;
using System.Linq;

public class MicrophoneInput : MonoBehaviour
{ 
    void Start()
    {
       
       
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
