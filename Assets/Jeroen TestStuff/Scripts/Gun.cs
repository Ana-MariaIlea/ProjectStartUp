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

    [SerializeField] private ParticleSystem muzzleFlash;
    private Camera cam;
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        cam = transform.parent.GetComponent<Camera>();
        if (cam != null)
            Debug.Log("Cam is not null");
        currentAmmo = ammoPerMag;

        line.enabled = false;
        line.SetVertexCount(2);
        line.SetWidth(0.1f, 0.1f);
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
        muzzleFlash.Play();
        RaycastHit hitInfo;

        Vector3 shootDirection = cam.transform.forward;
        shootDirection.x += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);
        shootDirection.y += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);

        if (Physics.Raycast(cam.transform.position, shootDirection, out hitInfo, range))
        {

            line.enabled = true;
            line.SetPosition(0, cam.transform.position);
            line.SetPosition(1, hitInfo.point);

            var otherStats = hitInfo.transform.GetComponentInParent<CharacterStats>();
            if (otherStats != null)
            {
                

                otherStats.TakeDamage(damage);
                Debug.Log(hitInfo.transform.name + " gets " + damage + " damage");
            }
        }
    }
}
