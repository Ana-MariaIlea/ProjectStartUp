using UnityEngine;

public class HotbarSelect : MonoBehaviour
{
    [SerializeField] private HotbarSlot[] hotbarSlots = new HotbarSlot[3];

    private HotbarSlot currentHotbarSlot = null;

    private KeyCode keyCode;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentHotbarSlot = hotbarSlots[0];
            currentHotbarSlot.UseSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentHotbarSlot = hotbarSlots[1];
            currentHotbarSlot.UseSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentHotbarSlot = hotbarSlots[2];
            currentHotbarSlot.UseSlot(2);
        }

    }

    
}
