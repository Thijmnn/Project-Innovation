using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAudio : MonoBehaviour
{
    enum sounds
    {
        menu,
        tutorial,
        game
    }
    sounds currentSound;
    AudioSource m_AudioSource;
    [SerializeField] List<AudioClip> clips = new List<AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.clip = clips[0];
        m_AudioSource.Play();
        currentSound = sounds.menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOn && currentSound == sounds.menu)
        {
            m_AudioSource.loop = false;
            if(m_AudioSource.isPlaying == false)
            {
                currentSound = sounds.game;
                m_AudioSource.loop = true;
                m_AudioSource.Stop();
                m_AudioSource.clip = clips[2];
                m_AudioSource.Play();
            }
        }
        else if(!GameManager.Instance.gameOn && currentSound == sounds.game)
        {
            m_AudioSource.loop = false;
            if (m_AudioSource.isPlaying == false)
            {
                currentSound = sounds.game;
                m_AudioSource.loop = true;
                m_AudioSource.Stop();
                m_AudioSource.clip = clips[1];
                m_AudioSource.Play();
            }
        }
    }  
}
