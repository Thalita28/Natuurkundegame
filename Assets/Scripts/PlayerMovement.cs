using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * 5000, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(Vector3.back * 5000, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(Vector3.left * 5000, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(Vector3.right * 5000, ForceMode.Impulse);
        }

    }
}
