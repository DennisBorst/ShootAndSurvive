using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool player;
    public int health;

    private void Update()
    {
        if (player)
        {
            UIManager.Instance.HealthAmount(health);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
