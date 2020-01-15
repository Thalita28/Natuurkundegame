using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{

    public GameObject warning;
    private bool IsOutOfBounds = false;
    private float timer;
    public float interval;
    public UnityEvent MyEvent;

    void Update()
    {
        if(Time.time - timer > interval && IsOutOfBounds)
        {
            MyEvent.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        IsOutOfBounds = true;
        timer = Time.time;
        warning.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        IsOutOfBounds = false;
        warning.SetActive(false);
    }
}
