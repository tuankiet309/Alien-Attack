using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    static int EnemyCounts = 0;
    void Start()
    {
        ++EnemyCounts;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        --EnemyCounts;
        if(EnemyCounts <= 0)
        {
            LevelManager.LevelFinishes();
        }
    }
}
