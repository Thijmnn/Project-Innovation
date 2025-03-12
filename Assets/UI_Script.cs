using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Script : MonoBehaviour
{
    [SerializeField] GameObject shop;
    GameObject currentUI;
    // Start is called before the first frame update
    void Start()
    {
        shop.SetActive(false);
    }

    // Update is called once per frame
    public void OpenShop()
    {
        currentUI.SetActive(false);
        shop.SetActive(true);
        currentUI = shop;
    }
    public void CloseShop()
    {
        shop.SetActive(false);
        currentUI = null;
    }
}
