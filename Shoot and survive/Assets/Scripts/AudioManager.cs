using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();

    }

    private static AudioManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new AudioManager();
            }

            return instance;
        }
    }
}
