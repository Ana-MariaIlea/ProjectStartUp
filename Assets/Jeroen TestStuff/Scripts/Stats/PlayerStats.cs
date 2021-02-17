using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private float healthPercentage;
    private float time = 0f;

    [Header("Health Regeneration")]
    [SerializeField] private float regenWaitTime = 2f;
    [SerializeField][Tooltip("This is the percentage of the max health that will regenerate over time. 0.01 = 1% & 1 = 100%")]
    [Range(0f, 1f)] private float regenPercentage = 0.01f;
    [SerializeField] private float regenerateUntil = 20f;
    [SerializeField] private float regenerateFrom = 5f;

    //[SerializeField] Inventory inventory;

    [Header("Test Stuff")]
    [SerializeField] private float healAmount = 5;
    [SerializeField] private float currentMedKitAmount = 3;
    [SerializeField] private float maxMedKitAmount = 5;

    private bool canRegenerate = false;
    

    private void Start()
    {
        if (regenPercentage < 0f || regenPercentage > 1f)
            Debug.LogException(new System.Exception("regenPercentage out of range. Value must be between 0 and 1"));
    }

    // Update is called once per frame
    void Update()
    {
        regenHealth();

        // For test purposes
        if (Input.GetKeyDown(KeyCode.T))
            TakeDamage(5);
        if (Input.GetKeyDown(KeyCode.L))
            addMedKit();
    }

    public void addMedKit()
    {
        if (currentMedKitAmount < maxMedKitAmount)
        {
            currentMedKitAmount++;
            //inventory.AddItem();
        }
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
