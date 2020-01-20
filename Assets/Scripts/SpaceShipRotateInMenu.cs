using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipRotateInMenu : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0.0f, 0.2f, 0.0f, Space.Self);
    }
}
