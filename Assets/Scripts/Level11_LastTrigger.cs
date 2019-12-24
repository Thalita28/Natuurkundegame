using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level11_LastTrigger : MonoBehaviour
{
    bool TriggerEnter = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TriggerEnter = true;
            Debug.Log("TriggerEnter is true");
            Invoke("FinishDelay", 5);
        }
        if (other.gameObject.CompareTag("LevelBoundry"))
        {
            Debug.Log("Crash");
            TriggerEnter = false;
        }
    }
    private void FinishDelay()
    {
        if (TriggerEnter == true)
        {
            Debug.Log("TriggerEnter is true after delay");
            Level11.LastTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("TriggerExit");
            TriggerEnter = false;
        }
    }

}
