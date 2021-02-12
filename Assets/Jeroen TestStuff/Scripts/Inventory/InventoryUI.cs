using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory playerInventory;

    private void Update()
    {
        if (playerInventory.GetHasPistol())
        {
            //SetImage to pistol image
        }
        if (playerInventory.GetHasRifle())
        {
            //SetImage to rifle image
        }
        if (playerInventory.GetMedKitAmount() > 0)
        {
            //SetImage to medkit image
            //Set medkitCount text to current medkitCount
        }
    }
}
