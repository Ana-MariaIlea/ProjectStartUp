using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Med-Kit")]
public class HealthItem : InventoryItem
{
    [Header("Health Item Data")]
    [SerializeField] private string useText = "Does something";

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        return builder.ToString();
    }

}
