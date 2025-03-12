using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostScript : MonoBehaviour
{
    public int cost;
    [SerializeField] float costScale;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.SetText(cost.ToString());
    }

    // Update is called once per frame
    public void UpdateCost()
    {
        if(GameManager.Instance.money > cost)
        {
            GameManager.Instance.money -= cost;
            float newCost = (float)cost * costScale;
            cost = (int) newCost;
        }
        text.SetText(cost.ToString());
    }
}
