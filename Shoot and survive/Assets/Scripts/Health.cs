using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int enemyHealth;

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            UIManager.Instance.ShowCrossHairKill();
            Destroy(this.gameObject);
        }
    }
}
