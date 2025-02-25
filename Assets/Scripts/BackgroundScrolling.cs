using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{

    [SerializeField]
    private Renderer bgRenderer;
    [SerializeField]
    float speedScale;
    Camera cam;

    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main;
        ScaleBG();
    }
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0,GameManager.Instance.speed / speedScale * Time.deltaTime);
    }

    void ScaleBG()
    {
        transform.localScale = cam.ScreenToWorldPoint(new Vector3(Screen.width*2,Screen.height*2,100));
    }
}
