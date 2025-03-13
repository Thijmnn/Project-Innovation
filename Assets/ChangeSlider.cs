using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSlider : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(MicrophoneInput.instance.maxCharge != 0) { slider.value = CompareValues(MicrophoneInput.instance.blowCharge, MicrophoneInput.instance.maxCharge); }
        else { slider.value = 1; }
    }

    private float CompareValues(float f1, float f2)
    {
        float diff = f1 / f2;
        return diff;
    }
}
