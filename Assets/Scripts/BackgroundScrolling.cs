using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{

    [SerializeField]
    private Renderer bgRenderer;
    [SerializeField]
    float speedScale;

    // Update is called once per frame
    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0,GameManager.Instance.speed / speedScale * Time.deltaTime);
    }
}
