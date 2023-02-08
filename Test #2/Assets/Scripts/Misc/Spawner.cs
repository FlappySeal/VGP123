using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> pickUp;

    
    
    // Start is called before the first frame update
    void Start()
    {
        int collectibleIndex = Random.Range(0, pickUp.Count);
        
            Instantiate(pickUp[collectibleIndex], transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
