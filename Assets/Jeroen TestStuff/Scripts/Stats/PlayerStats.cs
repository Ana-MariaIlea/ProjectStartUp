using UnityEngine;

public class PlayerStats : CharacterStats
{

    [Header("Health Regeneration")]
    [SerializeField] private float regenWaitTime = 2f;
    [SerializeField] [Tooltip("This is the percentage of the max health that will regenerate over time. 0.01 = 1% & 1 = 100%")]
    [Range(0f, 1f)] private float regenPercentage = 0.01f;
    [SerializeField] private float regenerateUntil = 20f;
    [SerializeField] private float regenerateFrom = 5f;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] takingDamageSounds;
    [SerializeField] private AudioClip medKitSound = null;

    [Header("References")]
    [SerializeField] private Healthbar healthbar = null;

    private bool canRegenerate = false;

    private float healthPercentage;
    private float time = 0f;

    private AudioSource source = null;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (regenPercentage < 0f || regenPercentage > 1f)
            Debug.LogException(new System.Exception("regenPercentage out of range. Value must be between 0 and 1"));
        healthbar.SetMaxHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.SetHealth(CurrentHealth);
        regenHealth();

        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(5);
        
    }

    public override void Heal(float healAmount)
    {
        base.Heal(healAmount);
        handleMedKitSound();
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        handleMedKitSound();
    }

    private void handleMedKitSound()
    {
        if (medKitSound != null)
        {
            if (source != null)
                source.PlayOneShot(medKitSound);
            else
                Debug.Log("Source is null");
        }
    }

    private void handlePlayerHitSounds()
    {
        if (takingDamageSounds.Length > 0)
        {
            int index = Random.Range(0, takingDamageSounds.Length - 1);
            if (source != null)
                source.PlayOneShot(takingDamageSounds[index]);
            else
                Debug.Log("Source is null");
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        handlePlayerHitSounds();
    }

    /// <summary>
    /// This regenerates health when the players health reaches a certain percentage of the maxHealth
    /// </summary>
    private void regenHealth()
    {
        healthPercentage = (CurrentHealth / MaxHealth) * 100;

        if (healthPercentage > 0f && healthPercentage <= regenerateFrom)
        {
            canRegenerate = true;
        }

        if (healthPercentage < regenerateUntil && canRegenerate)
        {
            time += Time.deltaTime;

            if (time >= regenWaitTime)
            {
                time = 0f;
                Heal(MaxHealth * regenPercentage);
            }
        }
        else
        {
            canRegenerate = false;
        }
    }
}
