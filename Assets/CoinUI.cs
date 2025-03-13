using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    int coins;
    TextMeshProUGUI m_TextMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        m_TextMeshPro = GetComponent<TextMeshProUGUI>();
        coins = GameManager.Instance.money;
        m_TextMeshPro.text = coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(coins < GameManager.Instance.money)
        {
            coins = GameManager.Instance.money;
            m_TextMeshPro.text = coins.ToString();
        }
    }
}
