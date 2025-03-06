using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{

    [Header("Layer1")]
    [SerializeField] List<Material> _materialL1 = new List<Material>();
    [SerializeField]
    GameObject bg1;
    [SerializeField] private GameObject bg2;
    [SerializeField] float speedScaleL1;

    [Header("Layer2")]
    [SerializeField] List<Sprite> _spritesL2 = new List<Sprite>();
    [SerializeField] float speedScaleL2;

    enum bgType
    {
        Land,
        Sky,
        Orbit,
        Space
    }
    bgType bgLevel = bgType.Land;
    Camera cam;
    [SerializeField] int curLevel = 0;
    bool switching = false;

    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main;
        Layer1Start();
        Layer2Start();
    }
    void Layer1Start()
    {
        bg1.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width *2 , Screen.height*2, 100));
        bg2.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width*2, Screen.height*2, 100));
        bg1.transform.position = new Vector3(0, 0 + bg1.transform.localScale.y, 4);
        bg2.transform.position = new Vector3(0, 0, 4);
    }

    void Layer2Start()
    {
        //cam.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height * 2, 100));
    }
    void Update()
    {
        Layer1Update();
        Layer2Update();
    }

    void Layer1Update()
    {
        if (bg1.transform.position.y <= transform.position.y - bg1.transform.localScale.y)
        {
            bg1.transform.position = new Vector3(0, bg2.transform.position.y + bg1.transform.localScale.y, 4);
            NextLevel(bg1);

        }
        if (bg2.transform.position.y <= transform.position.y - bg1.transform.localScale.y)
        {
            NextLevel(bg2);
            bg2.transform.position = new Vector3(0, bg1.transform.position.y + bg2.transform.localScale.y, 4);
        }
        bg1.transform.position -= new Vector3(0, GameManager.Instance.speed / speedScaleL1 * Time.deltaTime, 0);
        bg2.transform.position -= new Vector3(0, GameManager.Instance.speed / speedScaleL1 * Time.deltaTime, 0);
    }
    void NextLevel(GameObject obj)
    {
        Renderer sp = obj.GetComponent<Renderer>();
        if (!sp.materials[0].name.Contains(_materialL1[curLevel].name))
        {
            print(sp.materials[0].name);
            print(_materialL1[curLevel].name);
            sp.material = (_materialL1[curLevel]);
        }
        if (switching)
        {
            sp.material = (_materialL1[curLevel]);
            curLevel += 1;
            switching = false;
        }
        if (GameManager.Instance.height > 20 && bgLevel == bgType.Land)
        {
            curLevel += 1;
            bgLevel = bgType.Sky;
            switching = true;
        }

    }
    
    void Layer2Update()
    {
        
    }
}
