using System;
using System.Collections;
using System.Collections.Generic;

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
    int layerLevelL2 = 0;

    [Header("Layer3")]
    [SerializeField] List<GameObject> l3 = new List<GameObject>();
    [SerializeField] List<Material> _materialL3 = new List<Material>();
    [SerializeField] float speedScaleL3;
    bool switchl3;
    int layerLevelL3 = 0;
    [Header("Layer4")]
    [SerializeField] List<GameObject> l4 = new List<GameObject>();
    [SerializeField] List<Material> _materialL4 = new List<Material>();
    [SerializeField] float speedScaleL4;
    bool switchl4;
    int layerLevelL4 = 0;
    [Header("Layer5")]
    [SerializeField] List<GameObject> l5 = new List<GameObject>();
    [SerializeField] List<Material> _materialL5 = new List<Material>();
    [SerializeField] float speedScaleL5;
    bool switchl5;
    int layerLevelL5 = 0;
    [Header("Layer5")]
    [SerializeField] List<GameObject> l6 = new List<GameObject>();
    [SerializeField] List<Material> _materialL6 = new List<Material>();
    [SerializeField] float speedScaleL6;
    bool switchl6;
    int layerLevelL6 = 0;
    [Header("Layer7")]
    [SerializeField] List<GameObject> l7 = new List<GameObject>();
    [SerializeField] List<Material> _materialL7 = new List<Material>();
    [SerializeField] float speedScaleL7;
    bool switchl7;
    int layerLevelL7 = 0;

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
    float scaleL4;
    float scaleL5;
    float scaleL6;
    float scaleL7;
    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main;
        Layer1Start();
        ScreenQuarterStart(l2);
        ScreenQuarterStart(l3);
        ScreenQuarterStart(l6);
        //ScreenQuarterStart(l7);
        ScreenSizeStart(l4);
        ScreenSizeStart(l5);
    }
    void Layer1Start()
    {
        scaleL1 = bg1.transform.localScale.y;
        bg1.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height * 2, 100));
        bg2.transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width * 2, Screen.height * 2, 100));
        bg1.transform.position = new Vector3(0, 0 + bg1.transform.localScale.y, 4);
        bg2.transform.position = new Vector3(0, 0, 4);
    }

    void ScreenQuarterStart(List<GameObject> l)
    {
        scaleL2 = l2[0].transform.localScale.y;
        scaleL3 = l3[0].transform.localScale.y;
        scaleL6 = l6[0].transform.localScale.y;
        scaleL7 = l7[0].transform.localScale.y;
        for (int i = 0; i < l.Count; i++)
        {
            l[i].transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width *1.5f, Screen.height, 100));
            if (i == 0) l[i].transform.position = new Vector3(0, -5f, l[i].transform.position.z + 2);
            else l[i].transform.position = new Vector3(0, l[i - 1].transform.position.y + l[i].transform.localScale.y, l[i].transform.position.z +2);
        }
    }
    void ScreenSizeStart(List<GameObject> l)
    {
        
        scaleL4 = bg2.transform.localScale.y;
        scaleL5 = bg1.transform.localScale.y;
        for (int i = 0; i < l.Count; i++)
        {
            l[i].transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width * 1.5f, Screen.height * 1.5f, 100));
            if (i == 0) l[i].transform.position = new Vector3(0, 0 + bg1.transform.localScale.y, l[i].transform.position.z + 2);
            else l[i].transform.position = new Vector3(0, 0, l[i].transform.position.z +2);
        }

    }
    void Update()
    {
        Layer1Update();
        LayerUpdate(l2, _materialL2, switchl2, speedScaleL2, scaleL2, 2);
        LayerUpdate(l3, _materialL3, switchl3, speedScaleL3, scaleL3, 3);
        LayerUpdate(l4, _materialL4, switchl4, speedScaleL4, scaleL4, 4);
        LayerUpdate(l5, _materialL5, switchl5, speedScaleL5, scaleL5, 5);
        LayerUpdate(l6, _materialL6, switchl6, speedScaleL6, scaleL6, 6);
        //LayerUpdate(l7, _materialL7, switchl7, speedScaleL7, scaleL7, 8);
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
        if (GameManager.Instance.height > 40 && bgLevel == bgType.Sky)
        {
            curLevel += 1;
            bgLevel = bgType.Orbit;
            switching = true;
            switchl2 = true;
            switchl6 = true;
        }
        if (GameManager.Instance.height > 60 && bgLevel == bgType.Orbit)
        {
            curLevel += 1;
            bgLevel = bgType.Space;
            switching = true;
        }
    }

    void LayerUpdate(List<GameObject> Layer, List<Material> materials, bool layerSwitch, float speedScale, float sizeScale, int layer)
    {
        for (int i = 0; i < Layer.Count; i++)
        {
            if (Layer[i].transform.position.y <= 0 - bg1.transform.localScale.y / (scaleL1 / sizeScale))
            {
                if (i == 0) { Layer[i].transform.position = new Vector3(0, Layer[Layer.Count - 1].transform.position.y + Layer[i].transform.localScale.y, Layer[i].transform.position.z); }
                else { Layer[i].transform.position = new Vector3(0, Layer[i - 1].transform.position.y + Layer[i].transform.localScale.y, Layer[i].transform.position.z); }
                Renderer sp = Layer[i].GetComponent<Renderer>();

                switch (layer)
                {
                    case 2: Layer2(layerSwitch, sp, materials,2); break;
                    case 3: Layer3(materials, sp); break;
                    case 4: Layer3(materials, sp); break;
                    case 5: Layer3(materials, sp); break;
                    case 6: Layer2(layerSwitch, sp, materials,6); break;
                    case 7: Layer2(layerSwitch, sp, materials,7); break;
                    default: break;
                }

            }
            Layer[i].transform.position -= new Vector3(0, GameManager.Instance.speed / speedScale * Time.deltaTime, 0);
        }
    }

    void Layer2(bool layerSwitch, Renderer sp, List<Material> materials,int layer)
    {
        if (layerSwitch && bgLevel == bgType.Orbit)
        {
            if(layer == 2)
            layerLevelL2 += 1;
            sp.material = (materials[layerLevelL2]);

                layerSwitch = false;
                if (switchl2) switchl2 = false;
        }
        else if (!sp.materials[0].name.Contains(materials[layerLevelL2].name))
        {
            sp.material = (materials[layerLevelL2]);
        }
    }


    void Layer3(List<Material> materials, Renderer sp)
    {
        sp.material = (materials[1]);
    }

}
