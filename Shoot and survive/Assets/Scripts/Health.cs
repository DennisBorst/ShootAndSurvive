using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    public int enemyHealth;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log(audioSource);
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            AudioManager.Instance.PlaySound(audioClip);
            UIManager.Instance.ShowCrossHairKill();
            Destroy(this.gameObject);
        }
    }
}
