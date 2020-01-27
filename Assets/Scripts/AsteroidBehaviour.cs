using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public float xSpeed = 10.0f;
    public float zSpeed = 0.0f;
    public GameObject [] Asteroid;
    public int DestroyDistance;
    private Vector3 startPoint;

    private void Start()
    {
        Asteroid[Random.Range(0, Asteroid.Length - 1)].SetActive(true);
        transform.eulerAngles = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        startPoint = transform.position;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float moveSpeedx = xSpeed;
        float moveSpeedz = zSpeed;
        transform.position = transform.position + new Vector3(moveSpeedx * Time.deltaTime, 0, moveSpeedz * Time.deltaTime);

        if(Vector3.Distance(startPoint, transform.position) > DestroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
