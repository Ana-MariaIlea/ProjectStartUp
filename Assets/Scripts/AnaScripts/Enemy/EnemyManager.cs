using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    int numberOfEnemies = 0;
    void Start()
    {
        numberOfEnemies = FindObjectsOfType<EnemyBehaviour>().Length;
        Debug.Log(numberOfEnemies);

    }

    public void EnemySpawns()
    {
        numberOfEnemies++;
    }

    public void EnemyDies()
    {
        if (numberOfEnemies > 0)
        {
            numberOfEnemies--;
            Debug.Log(numberOfEnemies);
            if(numberOfEnemies == 0)
            {
                if (GetComponent<QuestCompletion>() != null) GetComponent<QuestCompletion>().Compltion();
            }
        }

    }

}
