using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ScaleBody : MonoBehaviour
{
    [SerializeField] GameObject neckObj;
    [SerializeField] GameObject legObj;
    [SerializeField] float _offset;
    void Update()
    {
        BoxCollider2D bellyCol = GetComponent<BoxCollider2D>();
        Vector3 legOffset = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y - (bellyCol.size.y * gameObject.transform.localScale.y ) + _offset,gameObject.transform.position.z);
        Vector3 neckOffset = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (bellyCol.size.y * gameObject.transform.localScale.y ) - _offset, gameObject.transform.position.z);

        legObj.transform.position = legOffset;
        neckObj.transform.position = neckOffset;
    }
}
