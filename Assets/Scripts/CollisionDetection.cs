using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    public UnityEvent MyEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("MissionObject") && !other.CompareTag("Player1") &&  !other.CompareTag("StationPlatform") && !other.CompareTag("LevelBoundry"))
            MyEvent.Invoke();


    }
}
