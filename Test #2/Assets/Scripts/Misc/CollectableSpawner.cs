using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectibles;
    public Transform[] spawnLocations;

    private void Start()
    {
        // Select a random collectible from the list
        int collectibleIndex = Random.Range(0, collectibles.Length);

        // Spawn the collectible in the 5 specified spawn locations
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            Instantiate(collectibles[collectibleIndex], spawnLocations[i].position, Quaternion.identity);
        }
    }
}