using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSpeed : MonoBehaviour
{
    public float xMin = 10.0f;
    public float zMin = -100.0f;
    public float xMax = 100.0f;
    public float zMax = 100.0f;
    

    // Update is called once per frame
    void Update()
    {
        float moveSpeedx = Random.Range(xMin, xMax);
        float moveSpeedz = Random.Range(zMin, zMax);
        transform.position = transform.position + new Vector3(moveSpeedx * Time.deltaTime, 0, moveSpeedz * Time.deltaTime);
    }
}
