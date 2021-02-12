using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private new string name = "Item Name";
    [SerializeField] private Sprite icon = null;

    public string Name => name;
    public Sprite Icon => icon;

}
