using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class EventFunctionTrigger : MonoBehaviour
{

    [SerializeField] int triggerType;
    public UnityEvent MyEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && triggerType == 0) MyEvent.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && triggerType == 1) MyEvent.Invoke();
    }
}
