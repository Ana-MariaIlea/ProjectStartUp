using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private bool hasPistol = true;
    [SerializeField] private bool hasRifle = false;
    [SerializeField][Min(0)] private int medKitAmount = 3;
    [SerializeField][Min(1)] private int maxMedKitAmount = 5;
    [SerializeField] [Min(1)] private float healAmount = 5;

    private PlayerStats playerStats;

    private void Start()
    {
        playerStats.GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (medKitAmount > 0)
            {
                medKitAmount--;
                playerStats.Heal(healAmount);
            }
        }
    }

    public void AddMedKit()
    {
        medKitAmount++;
    }

    public bool GetHasPistol()
    {
        return hasPistol;
    }

    public bool GetHasRifle()
    {
        return hasRifle;
    }

    public int GetMedKitAmount()
    {
        return medKitAmount;
    }


}
