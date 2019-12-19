using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryTrigger : MonoBehaviour
{

    public GameObject warning;
    private bool IsOutOfBounds = false;
    private float timer;
    private float interval = 10f;

  

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timer > interval && IsOutOfBounds)
        {
            Debug.Log("Do something terrible to the player");
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
