using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public bool isAmmo;
    public bool isHealth;

    [Header("Ammo")]
    public int ammoAmount;

    [Header("Health")]
    public int healthAmount;
}
