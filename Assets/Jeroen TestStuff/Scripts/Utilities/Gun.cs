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

    [Header("Sounds")]
    [SerializeField] private AudioClip[] plasmaShots = null;
    [SerializeField] private AudioClip reloadSounds = null;

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
    private AudioSource source = null;

    private void Start()
    {
        cam = transform.parent.parent.GetComponent<Camera>();
        anim = transform.parent.GetComponent<Animator>();
        fpsController = transform.parent.parent.parent.GetComponent<FirstPersonController>();
        source = GetComponent<AudioSource>();

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
            if (Input.GetKey(KeyCode.Mouse0) && !invKeypress.GetIsInventoryOpen)
            {
                if (fireTimer < fireRate) return;
                handlePistolShooting();
            }
            if (currentAmmo <= 0)
            {
                handleReload();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
                handlePistolShooting();
        }

        aimWeapon();
        handleAmmoUI();
    }

    private void handleReloadSounds()
    {
        if (reloadSounds != null)
        {
            int index = Random.Range(0, plasmaShots.Length - 1);
            if (source != null)
                source.PlayOneShot(reloadSounds, 0.5f);
            else
                Debug.Log("Source is null");
        }
    }

    private void handleShootSounds()
    {
        if (plasmaShots.Length > 0)
        {
            int index = Random.Range(0, plasmaShots.Length - 1);
            if (source != null)
                source.PlayOneShot(plasmaShots[index], 0.5f);
            else
                Debug.Log("Source is null");
        }
    }

    private void handleAmmoUI() => ammoUI.text = $"{currentAmmo} / {totalAmmo}";

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
            handleShootSounds();
            fireTimer = 0f;
        }
        else if (currentAmmo > 0)
        {
            shootGun();
            handleShootSounds();
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
            handleReloadSounds();
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
        if (Physics.Raycast(cam.transform.position, shootDirection, out hitInfo, range))
        {
            Debug.Log(hitInfo.transform.name);
            var otherStats = hitInfo.transform.GetComponentInParent<EnemyStats>();
            if (otherStats != null)
                otherStats.TakeDamage(damage);
            if (hitInfo.transform.tag == "Enemy")
            {
                var enemyBehaviour = hitInfo.transform.GetComponentInParent<EnemyBehaviour>();
                if (enemyBehaviour != null)
                {
                    enemyBehaviour.EnemyGetsHit(this.transform);
                }
            }
        }
    }
}
