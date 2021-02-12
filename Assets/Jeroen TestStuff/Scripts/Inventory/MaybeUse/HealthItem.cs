using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Med-Kit")]
public class HealthItem : InventoryItem
{
    [Header("Health Item Data")]
    [SerializeField] private string useText = "Does something";

}
