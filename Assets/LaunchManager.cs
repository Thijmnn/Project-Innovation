using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaunchManager : MonoBehaviour
{
    [SerializeField] List<GameObject> launchers = new List<GameObject>();
    [SerializeField] GameObject launcher;
    private int launcherIndex;
    void Start()
    {
        GameManager.gameRestart += Reset;
        
        launcher = launchers[0];
    }

    // Update is called once per frame
    void Update()
    {     
        launcher.SetActive(!GameManager.Instance.gameOn);
        print(!GameManager.Instance.gameOn);
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
        
    }
}
