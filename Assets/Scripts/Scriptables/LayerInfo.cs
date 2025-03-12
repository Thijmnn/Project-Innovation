using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LayerInfo", menuName = "ScriptableObjects/Layers/LayerInfo")]
public class LayerInfo : ScriptableObject
{
    public List<Material> _material = new List<Material>();
    public float speedScale;
    public bool switching;
    public int layerLevel = 0;
    public float scale;
}
