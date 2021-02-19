using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ToggleActiveWithKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode keyCode = KeyCode.None;
    [SerializeField] private GameObject objectToToggle = null;
    [SerializeField] private FirstPersonController fpsCont = null;

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
            objectToToggle.SetActive(!objectToToggle.activeInHierarchy);
        fpsCont.SetCanLookAround(!objectToToggle.activeInHierarchy);
    }

    public bool GetIsInventoryOpen => objectToToggle.activeInHierarchy;
}
