using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private float healthPercentage;
    [SerializeField] private float regenWaitTime = 2f;

    private bool canRegenerate = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        regenHealth();
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Test");
            TakeDamage(5);
        }
    }

    private void regenHealth()
    {
        healthPercentage = (CurrentHealth / MaxHealth) * 100;

        if (healthPercentage > 0 && healthPercentage <= 5)
        {
            canRegenerate = true;
            StartCoroutine(healthRegen());
        }
        else
            canRegenerate = false;
    }

    private IEnumerator healthRegen()
    {
        if (canRegenerate)
        {
            yield return new WaitForSeconds(regenWaitTime);
            Heal(MaxHealth / 100);
        }
        else
            yield break;
    }
}
