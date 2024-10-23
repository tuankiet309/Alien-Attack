using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjectsToSpawn;
    [SerializeField] Transform spawnTransform;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public bool StartSpawn()
    {
        if(gameObjectsToSpawn.Length == 0) 
            return false;
        if(animator != null)
        {
            animator.SetTrigger("spawn");
        }
        else 
        {
            SpawnIPL();
        }
        return true;
     }

    public void SpawnIPL()
    {
        int ramdomPick = Random.Range(0, gameObjectsToSpawn.Length);
        GameObject newSpawn = Instantiate(gameObjectsToSpawn[ramdomPick],spawnTransform.position,spawnTransform.rotation);
        ISpawnInterface spawnInterface = newSpawn.GetComponent<ISpawnInterface>();
        if (spawnInterface != null) 
        {
            spawnInterface.SpawnBy(gameObject);
        }
    }
}
