using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level11_FirstTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("FirstTriggerEnter");
            Level11.FirstTrigger = true;
        }
    }
}
