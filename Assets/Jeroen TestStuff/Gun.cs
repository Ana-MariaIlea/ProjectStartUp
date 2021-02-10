using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool isPistol = false; // If true gun has unlimited ammo

    [SerializeField] private int damage = 5;
    [SerializeField] private int ammoPerMag = 30;
    [SerializeField] private int currentAmmo = 0;

    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float bulletSpreadFactor = 0.1f;

    private float nextFire = 0f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if (cam != null)
            Debug.Log("Cam is not null");
        currentAmmo = ammoPerMag;
    }

    private void Update()
    {
        if (!isPistol)
        {
            if (Input.GetButton("Fire1"))
                handleShooting();
            if (currentAmmo <= 0)
                handleReload();
        }
        else
        {
            if(Input.GetButtonDown("Fire1"))
                handleShooting();
        }
    }

    private void handleShooting()
    {
        if (currentAmmo > 0 && !isPistol)
        {
            currentAmmo--;
            shootGun();
        }
        else if(currentAmmo > 0)
            shootGun();
    }

    private void handleReload()
    {
        //Makes sure ammo count doesn't go below 0
        if (currentAmmo <= 0)
            currentAmmo = 0;
        //Reloads ammo
        if (Input.GetKey(KeyCode.R))
        {
            currentAmmo = ammoPerMag;
        }
    }

    private void shootGun()
    {
        RaycastHit hitInfo;

        Vector3 shootDirection = cam.transform.forward;
        shootDirection.x += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);
        shootDirection.y += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);

        if (Physics.Raycast(cam.transform.position, shootDirection, out hitInfo, range))
        {

            //TODO:
            //Make alien take damage when shot
            var otherStats = hitInfo.transform.GetComponent<CharacterStats>();
            if (otherStats != null)
            {
                otherStats.TakeDamage(damage);
                Debug.Log(hitInfo.transform.name);
            }
        }
    }
}
