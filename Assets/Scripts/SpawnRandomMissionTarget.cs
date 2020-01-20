using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomMissionTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(-800,800), 0, Random.Range(-100, 700));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
