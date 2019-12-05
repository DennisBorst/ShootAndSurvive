using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthAmount;

    [SerializeField] private TextMeshProUGUI ammoInGun;
    [SerializeField] private TextMeshProUGUI ammoInBag;

    void Awake()
    {
        instance = this;
    }

    public void AmmoInGun(int ammo)
    {
        ammoInGun.text = "" + ammo;
    }

    public void AmmoInBag(int ammo)
    {
        ammoInBag.text = "" + ammo;
    }

    public void HealthAmount(int health)
    {
        healthAmount.text = "" + health;
    }

    #region Singleton
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }

            return instance;
        }
    }
    #endregion
}
