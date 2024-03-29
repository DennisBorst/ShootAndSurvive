﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int amountOfBullets;
    [SerializeField] private int totalAmountOfBullets;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float reloadTime;
    [SerializeField] private float maxShootDistance;

    [SerializeField] private int currentAmountOfBullets;
    private float currentReloadTime;
    private float nextTimeToFire = 0f;
    private bool isReloading;
    private bool pickedUp = false;

    [Header("Weapon attachments")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impaktEffect;
    [SerializeField] private LayerMask layersAbleToShoot;
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    private Camera cam;
    private Animator anim;

    //key
    private KeyCode shootKey;
    private KeyCode reloadKey;

    // Start is called before the first frame update
    void Start()
    {
        currentAmountOfBullets = amountOfBullets;
        currentReloadTime = reloadTime;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        shootKey = WrapperInput.Instance.shootKey;
        reloadKey = WrapperInput.Instance.reloadKey;
    }

    private void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pickedUp)
        {
            return;
        }

        Reloading();
        ShootCheck();
    }

    private void Reloading()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmountOfBullets <= 0 || Input.GetKeyDown(reloadKey) && totalAmountOfBullets > 0 && currentAmountOfBullets != amountOfBullets)
        {
            ShowAmountOfBullets();
            StartCoroutine(Reload());
            return;
        }
    }

    private void ShootCheck()
    {
        if(!isReloading && Input.GetKey(shootKey) && currentAmountOfBullets > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / rateOfFire;
            UIManager.Instance.AmmoInGun(currentAmountOfBullets);
            currentAmountOfBullets--;

            Shoot();
        }

        if (!isReloading && Input.GetKey(KeyCode.Mouse0) && currentAmountOfBullets > 0)
        {
            anim.SetBool("Shooting", true);
        }
        else
        {
            anim.SetBool("Shooting", false);
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxShootDistance, layersAbleToShoot))
        {
            Health healthScript = hit.transform.GetComponent<Health>();

            if(healthScript != null)
            {
                healthScript.TakeDamage(damage);
                UIManager.Instance.ShowCrossHairHit();
            }

            GameObject impakt = Instantiate(impaktEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impakt, 2f);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);


        if (amountOfBullets < totalAmountOfBullets + currentAmountOfBullets)
        {
            totalAmountOfBullets = totalAmountOfBullets - (amountOfBullets - currentAmountOfBullets) + 1;
            currentAmountOfBullets = amountOfBullets;
        }
        else
        {
            currentAmountOfBullets += totalAmountOfBullets;
            totalAmountOfBullets = 0;
        }
        ShowAmountOfBullets();

        isReloading = false;
    }

    public void PickedUp()
    {
        cam = GetComponentInParent<Camera>();
        pickedUp = true;
        ShowAmountOfBullets();
        anim.SetTrigger("PickedUp");
    }

    public void IncreaseAmmo(int ammo)
    {
        totalAmountOfBullets += ammo;
    }

    public void ShowAmountOfBullets()
    {
        UIManager.Instance.AmmoInBag(totalAmountOfBullets);
        UIManager.Instance.AmmoInGun(currentAmountOfBullets);
    }
}
