using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int amountOfBullets;
    [SerializeField] private float rateOfFire;
    [SerializeField] private float reloadTime;
    [SerializeField] private float maxShootDistance;
    private int currentAmountOfBullets;
    private float currentReloadTime;
    private bool isReloading;
    private float nextTimeToFire = 0f;

    [Header("Weapon attachments")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impaktEffect;
    private Camera cam;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentAmountOfBullets = amountOfBullets;
        currentReloadTime = reloadTime;

        anim = GetComponent<Animator>();
        cam = GetComponentInParent<Camera>();
    }

    private void OnEnable()
    {
        isReloading = false;
        anim.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        Reloading();
        ShootCheck();
    }

    private void Reloading()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmountOfBullets <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmountOfBullets != amountOfBullets)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    private void ShootCheck()
    {
        if(!isReloading && Input.GetKey(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / rateOfFire;
            currentAmountOfBullets--;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, maxShootDistance))
        {
            Health healthScript = hit.transform.GetComponent<Health>();

            if(healthScript != null)
            {
                healthScript.TakeDamage(damage);
            }

            GameObject impakt = Instantiate(impaktEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impakt, 2f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        anim.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - 0.25f);
        anim.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);

        currentAmountOfBullets = amountOfBullets;
        isReloading = false;
    }
}
