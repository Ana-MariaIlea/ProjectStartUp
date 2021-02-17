using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField] private VoidEvent onInventoryItemsUpdated = null;
    [SerializeField] private ItemSlot itemSlot = new ItemSlot();

    public ItemContainer ItemContainer { get; } = new ItemContainer(8);

    public void OnEnable() => ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;

    private void OnDisable() => ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;

    [ContextMenu("Test Add")]
    public void AddItem()
    {
        ItemContainer.AddItem(itemSlot);
    }

    public ItemSlot GetItemSlot()
    {
        return itemSlot;
    }
}
