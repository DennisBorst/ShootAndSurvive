using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioSource mainThemeSource;

    [SerializeField] private AudioClip mainThemePC;
    [SerializeField] private AudioClip mainThemeController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

#if USE_KEY_BOARD
        mainThemeSource.clip = mainThemePC;
#endif //USE_KEY_BOARD

#if USE_CONTROLLER
        mainThemeSource.clip = mainThemeController;
#endif //USE_CONTROLLER

        mainThemeSource.Play();
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
