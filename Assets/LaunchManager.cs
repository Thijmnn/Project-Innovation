using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LaunchManager : MonoBehaviour
{
    public static LaunchManager Instance { get; private set; }
    [Header("References")]
    [SerializeField] List<GameObject> launchers = new List<GameObject>();
    [SerializeField] GameObject launcher;
    [SerializeField] MoveBehaviour _mBhehaviour;


    private int launcherIndex;
    
    Camera cam;

    Animator _launcherAnim;

    public bool canon;

    
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        launcher = launchers[0];
        cam = Camera.main;
        GameManager.gameRestart += Reset; 
    }

    // Update is called once per frame
    void Update()
    {     
        launcher.SetActive(!GameManager.Instance.gameOn);
        if (MicrophoneInput.instance.blown) { _launcherAnim = launcher.GetComponent<Animator>(); AnimateLauncher(); }
        
    }

    public void UpgradeLauncher()
    {
        launcherIndex++;
        if(launcherIndex >= 0 && launcherIndex < 4 )
        {
            canon = false;
            return;
        }
        else if (launcherIndex >= 4 && launcherIndex < 8)
        {
            canon = false;
            launcher.SetActive(false);
            launcher = launchers[1];
            launcher.SetActive(true);
        }
        else if (launcherIndex >= 8)
        {
            canon = true;
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
            Vector3 draggedPos = cam.ScreenToWorldPoint(new Vector3(0, Input.touches[0].position.y, 0));
            float dist = draggedPos.y - transform.position.y;
            if (fingerPos.y < transform.position.y && dist >= -0.7f)
            {
                if(-dist >= 0f && -dist <= 0.2f)
                {
                    _launcherAnim.Play("Base Layer.LaunchPlayer", 0, 0f);
                }
                else if (-dist >= 0.21f && -dist <= 0.4f)
                {
                    _launcherAnim.Play("Base Layer.LaunchPlayer", 0, 0.2f);
                }
                else if (-dist >= 0.4f && -dist <= 0.6f)
                {
                    _launcherAnim.Play("Base Layer.LaunchPlayer", 0, 0.4f);
                }
                else if (-dist >= 0.61f)
                {
                    _launcherAnim.Play("Base Layer.LaunchPlayer", 0, 0.6f);
                }

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

    public void playRestOfAnim()
    {
        _launcherAnim.speed = 1f;
        _launcherAnim.Play("Base Layer.LaunchPlayer", 0, 0.8f);
    }
}
