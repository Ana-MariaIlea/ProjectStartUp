using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyObject : MonoBehaviour
{
    [SerializeField]
    public GameObject[] objectsToDestroy;
    // Start is called before the first frame update


    public void DestroyObjects()
    {
        if (objectsToDestroy.Length >= 1)
        {
            for (int i = 0; i < objectsToDestroy.Length; i++)
            {
                Destroy(objectsToDestroy[i]);
            }
        }
    }
}
