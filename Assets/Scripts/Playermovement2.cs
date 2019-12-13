using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playermovement2 : MonoBehaviour
{
    private Rigidbody rb;
    public bool RotateControls;
    public TextMeshProUGUI MotionFeedback;
    private int fuel_used;
    private float start_time;
    private float elapsed_time;
    public float thruster_Newton;
    public float front_thruster_Newton;
    public float rotate_speed;

    void Start()
    {
        start_time = 0;
        fuel_used = 0;
        elapsed_time = 0;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        var move_vec = rb.velocity;
        float player_speed_x = rb.velocity.x;
        float player_speed_z = rb.velocity.z;

        if (start_time != 0)
        {
            elapsed_time = Time.time - start_time;
        }

        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + fuel_used + "\nTime on Course: " + elapsed_time;
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * front_thruster_Newton, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * thruster_Newton, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * thruster_Newton, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * thruster_Newton, ForceMode.Impulse);
            fuel_used++;
        }

        if (RotateControls == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0.0f, rotate_speed, 0.0f, Space.Self);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0.0f, -rotate_speed, 0.0f, Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start"))
        {
            start_time = Time.time;
        }
        if (other.CompareTag("Finish"))
        {
            start_time = 0;
        }

    }
}
