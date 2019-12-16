using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playermovement2 : MonoBehaviour
{
    private Rigidbody rb;
    public bool RotateControls;
    public TextMeshProUGUI MotionFeedback;
    private int FuelUsedScaled;
    public float thruster_Newton;
    public float rotate_speed;
    public static float playerSpeedX;
    public static float playerSpeedZ;
    private float FuelUsed = 0.0f; 

     void Start()
    {
        FuelUsed = 0;
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        var move_vec = rb.velocity;
        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + FuelUsed;

        float ZAxisMovement = Input.GetAxis("Vertical");
        float XAxisMovement = Input.GetAxis("Horizontal");

        rb.AddRelativeForce(Vector3.forward * ZAxisMovement * thruster_Newton, ForceMode.Impulse);
        FuelUsed += Mathf.Abs((int)(ZAxisMovement * thruster_Newton));

        rb.AddRelativeForce(Vector3.right * XAxisMovement * thruster_Newton, ForceMode.Impulse);
        FuelUsed += Mathf.Abs((int)(XAxisMovement * thruster_Newton));


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
}
