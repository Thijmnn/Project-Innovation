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
    [SerializeField] List<GameObject> l2 = new List<GameObject>();
    [SerializeField] List<Material> _materialL2 = new List<Material>();
    [SerializeField] float speedScaleL2;
    bool switchl2;
    int layerLevel = 0;

    enum bgType
    {
        Land,
        Sky,
        Orbit,
        Space
    }
    [SerializeField] bgType bgLevel = bgType.Land;
    Camera cam;
    [SerializeField] int curLevel = 0;
    bool switching = false;

    float scaleL1;
    float scaleL2;
    float scaleL3;

    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main;
        Layer1Start();
        Layer2Start();
    }
    void Layer1Start()
    {
        scaleL1 = bg1.transform.localScale.y;
        bg1.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width *2 , Screen.height*2, 100));
        bg2.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width*2, Screen.height*2, 100));
        bg1.transform.position = new Vector3(0, 0 + bg1.transform.localScale.y, 4);
        bg2.transform.position = new Vector3(0, 0, 4);
    }

    void Layer2Start()
    {
        scaleL2 = l2[0].transform.localScale.y;
        for (int i = 0; i < l2.Count; i++)
        {
            l2[i].transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height, 100));
            if (i == 0) l2[i].transform.position = new Vector3(0, -3.4f, 3);
            else l2[i].transform.position = new Vector3(0, l2[i-1].transform.position.y + l2[i].transform.localScale.y, 3);
        }
    }
    void Update()
    {
        Layer1Update();
        LayerUpdate(l2,_materialL2,switchl2,speedScaleL2,scaleL2);
        
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
        if(GameManager.Instance.height > 40 && bgLevel == bgType.Sky)
        {
            curLevel += 1;
            bgLevel = bgType.Orbit;
            switching = true;
            switchl2 = true;
        }
        if (GameManager.Instance.height > 60 && bgLevel == bgType.Orbit)
        {
            curLevel += 1;
            bgLevel = bgType.Space;
            switching = true;
        }
    }
    
    void LayerUpdate(List<GameObject> Layer, List<Material> materials,bool layerSwitch, float speedScale,float sizeScale)
    {
        for (int i = 0; i < Layer.Count; i++)
        {
            if (Layer[i].transform.position.y <= 0 - bg1.transform.localScale.y / (scaleL1 / sizeScale))
            {
                if (i == 0) { Layer[i].transform.position = new Vector3(0, Layer[Layer.Count - 1].transform.position.y + Layer[i].transform.localScale.y, 3); }
                else { Layer[i].transform.position = new Vector3(0, Layer[i - 1].transform.position.y + Layer[i].transform.localScale.y, 3); }
                Renderer sp = Layer[i].GetComponent<Renderer>();
                if (layerSwitch && bgLevel == bgType.Orbit)
                {
                    layerLevel += 1;
                    sp.material = (materials[layerLevel]);
                    layerSwitch = false;
                    if (switchl2) switchl2 = false;
                }
                else if (!sp.materials[0].name.Contains(materials[layerLevel].name))
                {
                    sp.material = (materials[layerLevel]);
                }

            }
            Layer[i].transform.position -= new Vector3(0, GameManager.Instance.speed / speedScale * Time.deltaTime, 0);
        }
    }
}
