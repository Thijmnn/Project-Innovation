using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tutorialscript : MonoBehaviour
{
    [SerializeField] GameObject tilt;
    [SerializeField] GameObject blow;
    // Start is called before the first frame update
    void Awake()
    {
        tilt.SetActive(true);
        blow.SetActive(false);
        Invoke("Blow", 1.5f);
    }

    void Blow()
    {
        blow.SetActive(true);
        tilt.SetActive(false);
    }
}
