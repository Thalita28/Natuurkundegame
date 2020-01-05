using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Playermovement2 : MonoBehaviour
{
    private Rigidbody rb;
    public TextMeshProUGUI MotionFeedback;
    public Transform FuelBar;

    private bool checkingForStop;
    private float stopTimer;
    public bool RotateControls;
    private bool IsMoving;
    public float ThrusterPower;
    public float RotateSpeed;
    public float FuelUsed = 0.0f;
    public float MaxSpeed = 20.0f;
    private int StartingFuel;

    void Start()
    {
        IsMoving = false;
        checkingForStop = false;
        FuelUsed = 0;
        rb = GetComponent<Rigidbody>();
        StartingFuel = PlayerPrefs.GetInt("StartingFuel", 10000);
    }

    public void Update()
    {
        if (IsMoving) Movement();
        UpdateFuelBar();
        CheckForCompleteStop();
        var move_vec = rb.velocity;
        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + FuelUsed;



    }

    private void CheckForCompleteStop()
    {
        if (rb.velocity.magnitude < 0.5 && rb.velocity.magnitude != 0 && !checkingForStop)
        {
            stopTimer = Time.time;
            checkingForStop = true;
        }

        if (checkingForStop && rb.velocity.magnitude < 0.5 && Time.time - stopTimer > 0.5)
        {
            rb.velocity = Vector3.zero;
            checkingForStop = false;
        }

    }

    private void Movement()
    {
        float ZAxisMovement = Input.GetAxis("Vertical");
        float XAxisMovement = Input.GetAxis("Horizontal");

        if (rb.velocity.x > MaxSpeed && XAxisMovement > 0)
        {
            XAxisMovement = 0;
        }
        else if (rb.velocity.x < -MaxSpeed && XAxisMovement < 0)
        {
            XAxisMovement = 0;
        }
        if (rb.velocity.z > MaxSpeed && ZAxisMovement > 0)
        {
            ZAxisMovement = 0;
        }
        else if (rb.velocity.z < -MaxSpeed && ZAxisMovement < 0)
        {
            ZAxisMovement = 0;
        }

        if (StartingFuel > FuelUsed)
        {
            rb.AddForce(Vector3.forward * ZAxisMovement  *ThrusterPower, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(ZAxisMovement * ThrusterPower));
            rb.AddForce(Vector3.right * XAxisMovement * ThrusterPower, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(XAxisMovement * ThrusterPower));
        }

        if (RotateControls == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0.0f, RotateSpeed, 0.0f, Space.Self);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(0.0f, -RotateSpeed, 0.0f, Space.Self);
            }
        }
    }


    public void allowMovement()
    {
        IsMoving = true;
    }

    public void denyMovement()
    {
        IsMoving = false;
    }

    public void UpdateFuelBar()
    {
        var FuelBarLeft = (StartingFuel - FuelUsed) / StartingFuel;
        if (FuelBarLeft < 0)
        {
            FuelBarLeft = 0;
        }
        FuelBar.transform.localScale = new Vector3(1, FuelBarLeft, 1);
    }
}
