using UnityEngine;

public abstract class InventoryItem : Item
{
    [Header("Item Data")]
    [SerializeField][Min(1)] private int maxStack = 1;

    public int MaxStack => maxStack;

    public override string ColouredName => Name;


}
