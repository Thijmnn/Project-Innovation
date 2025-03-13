using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class LaunchManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] List<GameObject> launchers = new List<GameObject>();
    [SerializeField] GameObject launcher;
    [SerializeField] MoveBehaviour _mBhehaviour;


    private int launcherIndex;
    
    Camera cam;
    void Start()
    {
        cam = Camera.main;
        GameManager.gameRestart += Reset;
        
        launcher = launchers[0];
    }

    // Update is called once per frame
    void Update()
    {     
        launcher.SetActive(!GameManager.Instance.gameOn);
        AnimateLauncher();
    }

    public void UpgradeLauncher()
    {
        launcherIndex++;
        if(launcherIndex >= 0 && launcherIndex < 5 )
        {
            return;
        }
        else if (launcherIndex >= 5 && launcherIndex < 9)
        {
            launcher.SetActive(false);
            launcher = launchers[1];
            launcher.SetActive(true);
        }
        else if (launcherIndex >= 9)
        {
            launcher.SetActive(false);
            launcher = launchers[2];
            launcher.SetActive(true);
        }
    }

    private void Reset()
    {
        launcher.SetActive(true);
    }

    public void AnimateLauncher()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            Vector3 fingerPos = cam.ScreenToWorldPoint(Input.touches[0].position);
            if (fingerPos.y < transform.position.y)
            {
                Vector3 draggedPos = cam.ScreenToWorldPoint(new Vector3(0, Input.touches[0].position.y, 0));
                float dist = draggedPos.y - transform.position.y;
                print(dist);

            }
            else
            {
                return;
            }

        }
        else if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && _mBhehaviour.launching)
        {
            return;
        }
    }
}
