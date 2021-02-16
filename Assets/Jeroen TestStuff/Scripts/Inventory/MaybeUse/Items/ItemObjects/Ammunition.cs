using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition", menuName = "Items/Ammunition")]
public class Ammunition : InventoryItem
{

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();
        
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        return builder.ToString();
    }

}
