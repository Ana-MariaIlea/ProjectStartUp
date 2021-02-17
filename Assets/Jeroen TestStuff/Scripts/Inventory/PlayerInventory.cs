using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private float currentMedKitAmount = 3;
    [SerializeField] private float maxMedKitAmount = 5;
    [SerializeField] private bool hasPistol = true;
    [SerializeField] private bool hasRifle = false;
    [SerializeField] TextMeshProUGUI MedKitAmountText;
    [SerializeField] Image[] images = new Image[3];

    public void AddMedKit()
    {
        currentMedKitAmount++;
        updateMedText();
    }

    private void updateMedText()
    {
        if (images[2].IsActive())
        {
            MedKitAmountText.text = currentMedKitAmount.ToString();
        }
        else
            return;
    }
}
