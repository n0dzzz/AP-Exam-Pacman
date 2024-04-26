using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject ghostPrefab;
    
    void Start()
    {
        StartCoroutine(SpawnGhost());
    }

    IEnumerator SpawnGhost()
    {
        while(true)
        {
            yield return new WaitForSeconds(20f);
        
            Instantiate(ghostPrefab);
        }
    }
}
