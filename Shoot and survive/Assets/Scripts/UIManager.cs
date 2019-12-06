using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthAmount;

    [SerializeField] private TextMeshProUGUI ammoInGun;
    [SerializeField] private TextMeshProUGUI ammoInBag;

    [SerializeField] private Image crosshairHit;
    [SerializeField] private Image crosshairKill;

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

    public void ShowCrossHairHit()
    {
        StartCoroutine(CrossHairShowTime(1));
    }

    public void ShowCrossHairKill()
    {
        StartCoroutine(CrossHairShowTime(2));
    }

    IEnumerator CrossHairShowTime(int hitInfo)
    {
        if(hitInfo == 1)
        {
            crosshairHit.enabled = true;
            yield return new WaitForSeconds(0.1f);
            crosshairHit.enabled = false;
        }
        if (hitInfo == 2)
        {
            crosshairKill.enabled = true;
            yield return new WaitForSeconds(0.15f);
            crosshairKill.enabled = false;
        }
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
