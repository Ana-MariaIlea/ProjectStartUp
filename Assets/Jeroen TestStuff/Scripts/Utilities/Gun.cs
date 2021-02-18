using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Tooltip("If True gun as unlimited ammo")]
    [SerializeField] private bool isPistol = false; // If true gun has unlimited ammo

    [SerializeField] private int damage = 5;
    [SerializeField] private int ammoPerMag = 30;
    [SerializeField] private int currentAmmo = 0;
    [SerializeField] private int totalAmmo = 120;

    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 0.15f;
    [Min(0)][SerializeField] private float bulletSpreadFactor = 0.1f;
    [Min(1)][SerializeField] private float aimSpeed = 8f;
    [SerializeField] private float weaponAimCamZoom = 0f;

    [SerializeField] Vector3 aimPosition = Vector3.zero;

    [Header("References")]
    [SerializeField] private ParticleSystem muzzleFlash = null;
    [SerializeField] private Inventory inventory = null;
    
    private float fireTimer = 0f;
    private float originFoV = 0f;

    private int isShooting = 0;

    private Vector3 originPos = Vector3.zero;

    private Camera cam = null;
    private LineRenderer line = null;
    private Animator anim = null;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        cam = transform.parent.GetComponent<Camera>();
        anim = GetComponent<Animator>();
        originPos = transform.localPosition;
        fireTimer = fireRate;
        originFoV = cam.fieldOfView;

        line.enabled = false;
        line.SetVertexCount(2);
        line.SetWidth(0.1f, 0.1f);

        reload();
    }

    private void Update()
    {

        if (fireTimer < fireRate) fireTimer += Time.deltaTime;

        if (!isPistol)
        {
            if (Input.GetButton("Fire1"))
            {
                if (fireTimer < fireRate) return;
                handlePistolShooting();
            }
            if (currentAmmo <= 0)
                handleReload();
        }
        else
        {
            if(Input.GetButtonDown("Fire1"))
                handlePistolShooting();
        }

        aimWeapon();
    }

    private void aimWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("YEET");
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
            //cam.fieldOfView = weaponAimCamZoom;
        }
        //else
        //{
        //    transform.localPosition = Vector3.Lerp(transform.localPosition, originPos, Time.deltaTime * aimSpeed);
        //    //cam.fieldOfView = originFoV;
        //}
    }

    public void UpdateTotalAmmo(int ammo)
    {
        Debug.Log(ammo);
        currentAmmo += ammo;
    }

    private void handlePistolShooting()
    {
        isShooting = 1;
        if (currentAmmo > 0 && !isPistol)
        {
            currentAmmo--;
            shootGun();
            fireTimer = 0f;
        }
        else if (currentAmmo > 0)
        {
            shootGun();
            fireTimer = 0f;
        }
        isShooting = 0;
    }

    private void handleReload()
    {
        //Makes sure ammo count doesn't go below 0
        if (currentAmmo <= 0)
            currentAmmo = 0;
        //Reloads ammo
        if (Input.GetKey(KeyCode.R)) reload();
    }

    private void reload()
    {
        if (totalAmmo >= ammoPerMag)
        {
            currentAmmo = ammoPerMag;
            totalAmmo -= ammoPerMag;
        }
        else
        {
            currentAmmo = totalAmmo;
            totalAmmo = 0;
        }
    }

    private void shootGun()
    {
        muzzleFlash.Play();
        anim.Play("RifleHandShoot");
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
            }
        }
    }
}
