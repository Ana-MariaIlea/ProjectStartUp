using UnityEngine;

public abstract class InventoryItem : Item
{
    [Header("Item Data")]
    [Min(0)] private int maxStack = 1;

    public int MaxStack => maxStack;
}
