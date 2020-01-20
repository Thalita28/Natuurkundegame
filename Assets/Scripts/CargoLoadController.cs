using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class CargoLoadController : MonoBehaviour
{
    public UnityEvent CargoClicked;
    private Animator animator;
   

    private Vector3 offset = new Vector3(-50, -200, -50);
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
    
    }


    private void OnMouseEnter()
    {
        animator.SetTrigger("MouseEnter");
    }

    private void OnMouseExit()
    {
        animator.SetTrigger("MouseExit");
    }

    private void OnMouseDown()
    {
        CargoClicked.Invoke();
        
    }
}
