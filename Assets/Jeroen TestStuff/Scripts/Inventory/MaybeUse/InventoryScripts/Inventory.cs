using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
    [SerializeField] private List<ItemSlot> itemSlots = new List<ItemSlot>();

    public ItemContainer ItemContainer { get; } = new ItemContainer(8);

    public void OnEnable() => ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;

    private void OnDisable() => ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;

    [ContextMenu("Test Add Med-Kit")]
    public void AddMedKit() => ItemContainer.AddItem(itemSlots[0]);
}
