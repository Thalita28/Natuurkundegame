using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    public float xAxis, yAxis, zAxis;
    void Update()
    {
        transform.Rotate(xAxis, yAxis, zAxis);
    }
}
