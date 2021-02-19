using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
    [SerializeField] private Image aimDot = null;
    [SerializeField] private TextMeshProUGUI ammoUI = null;
    [SerializeField] private ToggleActiveWithKeyPress invKeypress = null;
    
    private float fireTimer = 0f;
    private float originFoV = 0f;
    private float aimSpreadFactor = 0f;
    private float originSpreadFactor = 0f;

    private Vector3 originPos = Vector3.zero;

    private Camera cam = null;
    private Animator anim = null;
    private FirstPersonController fpsController;

    private void Start()
    {
        cam = transform.parent.parent.GetComponent<Camera>();
        anim = transform.parent.GetComponent<Animator>();
        fpsController = transform.parent.parent.parent.GetComponent<FirstPersonController>();

        fireTimer = fireRate;
        originPos = transform.localPosition;
        originFoV = cam.fieldOfView;
        originSpreadFactor = aimSpreadFactor;
        aimSpreadFactor = bulletSpreadFactor / 2;

        reload();
    }

    private void Update()
    {

        if (fireTimer < fireRate) fireTimer += Time.deltaTime;

        if (!isPistol)
        {
            if (Input.GetButton("Fire1") && !invKeypress.GetIsInventoryOpen)
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
        handleAmmoUI();
    }

    private void handleAmmoUI()
    {
        ammoUI.text = $"{currentAmmo} / {totalAmmo}";
    }

    private void aimWeapon()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, Time.deltaTime * aimSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, weaponAimCamZoom, Time.deltaTime * aimSpeed);
            fpsController.SetMoveSpeedToAimSpeed(true);
            aimDot.enabled = false;
            bulletSpreadFactor = aimSpreadFactor;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originPos, Time.deltaTime * aimSpeed);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, originFoV, Time.deltaTime * aimSpeed);
            fpsController.SetMoveSpeedToAimSpeed(false);
            aimDot.enabled = true;
            bulletSpreadFactor = originSpreadFactor;
        }
    }

    public void UpdateTotalAmmo(int ammo)
    {
        Debug.Log(ammo);
        currentAmmo += ammo;
    }

    private void handlePistolShooting()
    {
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
        anim.Play("RifeHandFire");

        Vector3 shootDirection = cam.transform.forward;
        shootDirection.x += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);
        shootDirection.y += Random.Range(-bulletSpreadFactor, bulletSpreadFactor);

        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, shootDirection, out hitInfo, range) &&
            transform.parent.parent.parent.name != "FPS Player")
        {
            var otherStats = hitInfo.transform.GetComponentInParent<CharacterStats>();
            if (otherStats != null)
            {
                otherStats.TakeDamage(damage);
            }
        }
    }
}
