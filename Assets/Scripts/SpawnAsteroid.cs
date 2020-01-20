using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour
{
    public GameObject Asteroid;     //De prefab die gespawnt wordt
    public float spawnTime = 3f;    //Tijd tussen de spawns
    public float zMin, zMax, yMin, yMax, spawnPointX;    

    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    /*private void FixedUpdate()
    {
        Spawn();
    }*/

    void Spawn()
    {
        float spawnPointY = Random.Range(yMin, yMax);
        float spawnPointZ = Random.Range(zMin, zMax);
        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        Instantiate(Asteroid, spawnPosition, Quaternion.identity);
    }
}
