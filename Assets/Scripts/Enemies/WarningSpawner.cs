using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> warningTypes;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * GameManager.Instance.scalingObj.localScale.x, gameObject.transform.localScale.y * GameManager.Instance.scalingObj.localScale.y, 0.1f);
    }
    private void OnEnable()
    {
        SpawnerScript.warning += SpawnWarning;
    }
    private void DisEnable()
    {
        SpawnerScript.warning -= SpawnWarning;
    }

    void SpawnWarning(Vector3 pos, Info.spawnPosition spawnPos, Info.warningTypes warn, bool left)
    {
        Vector3 warningPos = Vector3.zero;
        GameObject warningType = null;
        switch (spawnPos)
        {
            case Info.spawnPosition.Up:
                warningPos = new Vector3(pos.x, transform.position.y + transform.localScale.y / 2, pos.z);
                break;

            case Info.spawnPosition.Down:
                warningPos = new Vector3(pos.x, transform.position.y - transform.localScale.y / 2, pos.z);
                break;

            case Info.spawnPosition.Left:
                warningPos = new Vector3(transform.position.x - transform.localScale.x / 2, pos.y, pos.z);
                break;

            case Info.spawnPosition.Right:
                warningPos = new Vector3(transform.position.x + transform.localScale.x / 2, pos.y, pos.z);
                break;

            case Info.spawnPosition.Sides:
                if (left)
                {
                    warningPos = new Vector3(transform.position.x - transform.localScale.x / 2, pos.y, pos.z);
                }
                else
                {
                    warningPos = new Vector3(transform.position.x + transform.localScale.x / 2, pos.y, pos.z);
                }
                break;

            case Info.spawnPosition.SidesTop:
                if (left)
                {
                    warningPos = new Vector3(transform.position.x - transform.localScale.x / 2, pos.y, pos.z);
                }
                else
                {
                    warningPos = new Vector3(transform.position.x + transform.localScale.x / 2, pos.y, pos.z);
                }
                break;

            case Info.spawnPosition.SidesBottom:
                if (left)
                {
                    warningPos = new Vector3(transform.position.x - transform.localScale.x / 2, pos.y, pos.z);
                }
                else
                {
                    warningPos = new Vector3(transform.position.x + transform.localScale.x / 2, pos.y, pos.z);
                }
                break;
        }
        switch (warn)
        {
            case Info.warningTypes.small:
                warningType = warningTypes[0];
                break;
            case Info.warningTypes.big:
                warningType = warningTypes[1];
                break;

        }
        GameObject warnSpawn = Instantiate(warningType, warningPos, Quaternion.identity);
    }
}
