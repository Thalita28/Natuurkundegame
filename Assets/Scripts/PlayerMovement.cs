using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public bool RotateControls;
    public TextMeshProUGUI MotionFeedback;
    private int fuel_used;
    private float start_time;
    private float elapsed_time;

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

        if(start_time != 0)
        {
            elapsed_time = Time.time - start_time;
        }

        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + fuel_used + "\nTime on Course: " + elapsed_time;
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.forward * 5000, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddRelativeForce(Vector3.back * 5000, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left * 5000, ForceMode.Impulse);
            fuel_used++;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right * 5000, ForceMode.Impulse);
            fuel_used++;
        }

        if (RotateControls == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.Rotate(0.0f, 5.0f, 0.0f, Space.Self);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.Rotate(0.0f, -5.0f, 0.0f, Space.Self);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start"))
        {
            start_time = Time.time;
        }
        if( other.CompareTag("Finish"))
        {
            start_time = 0;
        }
       
    }
}
