using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool player;
    public int playerHealth;
    public static int playerHealthStatic;
    public int enemyHealth;

    private void Start()
    {
        if (player)
        {
            playerHealthStatic = playerHealth;
        }
    }

    private void Update()
    {
        if (player)
        {
            UIManager.Instance.HealthAmount(playerHealthStatic);
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0 && !player)
        {
            UIManager.Instance.ShowCrossHairKill();
            Destroy(this.gameObject);
        }
    }
}
