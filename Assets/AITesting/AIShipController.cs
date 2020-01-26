using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class AIShipController : MonoBehaviour
{
    public GameObject[] Waypoints;
    public GameObject[] Endpoints;
    private Rigidbody rb;
    public ParticleSystem ThrusterUp;
    public ParticleSystem ThrusterDown;
    public ParticleSystem ThrusterForward;
    public ParticleSystem ThrusterBack;

    private bool checkingForStop;
    private float stopTimer;
    [SerializeField] int ThrusterPower;
    [SerializeField] int TargetSpeed;

    [SerializeField] float StoppingSpeed;

    private float ZAxisMovement;
    private float XAxisMovement;
    private float TargetXAxis, TargetZAxis = 0;

    private Vector3 TargetPosition;
    private int WaypointIndex = 0;
    private int EndPointIndex = 0;
    private bool ActiveEndpoint = false;

    private bool IsTransitioning = false;

    void Start()
    {
        FindNearestWayPoint();
        TargetPosition = new Vector3(Waypoints[WaypointIndex].transform.position.x, transform.position.y, Waypoints[WaypointIndex].transform.position.z);
        checkingForStop = false;

        rb = GetComponent<Rigidbody>();
        Movement();

    }

    public void Update()
    {
        GetToVelocity();
        GetToPosition();
        CheckForWayPoints();
    }

    private void CheckForWayPoints()
    {

        if (ActiveEndpoint)
        {
            if (Vector3.Distance(TargetPosition, transform.position) < 4 && rb.velocity.magnitude < 0.5 && !IsTransitioning)
            {
                IsTransitioning = true;
                StartCoroutine(NextWayPoint(2));
            }
        }
        else
        {
            if (Vector3.Distance(TargetPosition, transform.position) < TargetSpeed * 1.3 && !IsTransitioning)
            {
                IsTransitioning = true;
                StartCoroutine(NextWayPoint(0));
            }
        }
    }

    IEnumerator NextWayPoint(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        WaypointIndex++;
        if (WaypointIndex == Waypoints.Length) WaypointIndex = 0;

        TargetPosition = new Vector3(Waypoints[WaypointIndex].transform.position.x, transform.position.y, Waypoints[WaypointIndex].transform.position.z);
        if (WaypointIndex == 0 || WaypointIndex == 3 || WaypointIndex == 4) ActiveEndpoint = true;
        else ActiveEndpoint = false;
        IsTransitioning = false;
    }

    public void FixedUpdate()
    {
        Movement();
        CheckForCompleteStop();
    }

    private void CheckForCompleteStop()
    {
        if (rb.velocity.magnitude < StoppingSpeed && rb.velocity.magnitude != 0 && !checkingForStop)
        {
            stopTimer = Time.time;
            checkingForStop = true;
        }

        if (checkingForStop && rb.velocity.magnitude < StoppingSpeed && Time.time - stopTimer > 0.5f)
        {
            rb.velocity = Vector3.zero;
            checkingForStop = false;
        }
    }

    private void Movement()
    {
            rb.AddForce(Vector3.forward * ZAxisMovement * ThrusterPower, ForceMode.Impulse);
            rb.AddForce(Vector3.right * XAxisMovement * ThrusterPower, ForceMode.Impulse);
            MotorAnimator(ZAxisMovement, XAxisMovement);
     
    }
    public void MotorAnimator(float VerticalControl, float HorizontalControl)
    {
        bool ZAnimatorDown = false;
        bool ZAnimatorUp = false;
        bool XAnimatorForward = false;
        bool XAnimatorBack = false;


        if (VerticalControl > 0.0f)
        {
            ZAnimatorUp = true;
        }
        if (VerticalControl < 0.0f)
        {
            ZAnimatorDown = true;
        }
        if (HorizontalControl > 0.0f)
        {
            XAnimatorForward = true;
        }
        if (HorizontalControl < 0.0f)
        {
            XAnimatorBack = true;
        }

        if (ZAnimatorDown == true)
        {
            ThrusterDown.Play();
        }
        if (ZAnimatorUp == true)
        {
            ThrusterUp.Play();
        }
        if (XAnimatorForward == true)
        {
            ThrusterForward.Play();
        }
        if (XAnimatorBack == true)
        {
            ThrusterBack.Play();
        }

        if (VerticalControl == 0.0f)
        {
            ThrusterUp.Stop();
            ThrusterDown.Stop();
        }

        if (HorizontalControl == 0.0f)
        {
            ThrusterBack.Stop();
            ThrusterForward.Stop();
        }
    }

    public void trusterUp(int TargetAcc)
    {

        TargetZAxis += TargetAcc;
    
    }

    public void trusterDown(int TargetAcc)
    {
        TargetZAxis -= TargetAcc;
      
    }
    public void trusterRight(int TargetAcc)
    {
        TargetXAxis += TargetAcc;
      
    }
    public void trusterLeft(int TargetAcc)
    {
        TargetXAxis -= TargetAcc;
  
    }

    private void trusterStopVertical()
    {
        ZAxisMovement = 0;
    }

    private void trusterStopHorizontal()
    {
        XAxisMovement = 0;
    }

    private void GetToVelocity()
    {
        float accuracy = 1f;

        //float diff = Mathf.Abs(rb.velocity.x - TargetXAxis);
        if (rb.velocity.x - TargetXAxis > accuracy) XAxisMovement = -1;// *(diff/50);
        else if (TargetXAxis - rb.velocity.x > accuracy) XAxisMovement = 1;// * (diff / 50);
        else XAxisMovement = 0;

        //diff = Mathf.Abs(rb.velocity.z - TargetZAxis);
        if (rb.velocity.z - TargetZAxis > accuracy) ZAxisMovement = -1; // *(diff / 50);
        else if (TargetZAxis - rb.velocity.z > accuracy) ZAxisMovement = 1;// * (diff / 50);
        else ZAxisMovement = 0;
    }

    private void GetToPosition()
    {
        Vector3 diff = transform.position - TargetPosition;
        float factor, factor2;

        TargetXAxis = -diff[0];
        TargetZAxis = -diff[2];

        factor = Mathf.Abs(TargetZAxis) / (Mathf.Abs(TargetXAxis) + Mathf.Abs(TargetZAxis));
        factor2 = Mathf.Abs(TargetXAxis) / (Mathf.Abs(TargetXAxis) + Mathf.Abs(TargetZAxis));
        if (TargetXAxis > TargetSpeed) TargetXAxis = TargetSpeed;
        if (TargetXAxis < -TargetSpeed) TargetXAxis = -TargetSpeed;
        if (TargetZAxis > TargetSpeed) TargetZAxis = TargetSpeed;
        if (TargetZAxis < -TargetSpeed) TargetZAxis = -TargetSpeed;

        TargetZAxis *= factor;
        TargetXAxis *= factor2;

    }


    private void FindNearestWayPoint()
    {
        float distance = 9999999;
        int NearestWayPoint = 0;
        int i;
        for (i = 0; i < Waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, Waypoints[i].transform.position) < distance)
            { 
                distance = Vector3.Distance(transform.position, Waypoints[i].transform.position);
                NearestWayPoint = i;
            }
        }

        WaypointIndex = NearestWayPoint;
    }

}
