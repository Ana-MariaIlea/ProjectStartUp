using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    [SerializeField] private string title;

    public ItemType itemType { get; private set; }

    public enum ItemType
    {
        Pistol,
        Rifle,
        MedKit
    }

    public string GetTitle()
    {
        return title;
    }
}
