using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Playermovement2 : MonoBehaviour
{
 
    private Rigidbody rb;
    public TextMeshProUGUI MotionFeedback;
    public TextMeshProUGUI FuelText;
    public TextMeshProUGUI Speed;
    public Transform FuelBar;
    public UnityEvent PlayerFail;
    public RawImage FuelBarSprite;
    public GameObject[] Arrows;

    public ParticleSystem ThrusterUp;
    public ParticleSystem ThrusterDown;
    public ParticleSystem ThrusterForward;
    public ParticleSystem ThrusterBack;

    private bool checkingForStop;
    private float stopTimer;
    public bool RotateControls;
    private bool IsMoving;
    private int ThrusterPower;
    public float RotateSpeed;
    public float FuelUsed = 0.0f;
    private float MaxSpeed;
    private int StartingFuel;
    [SerializeField] int LevelFuel;
    [SerializeField] float StoppingSpeed;
    [SerializeField] bool AI = false;
    private float ZAxisMovement;
    private float XAxisMovement;

    void Start()
    {
        if(AI) Arrows[4].SetActive(false);
        IsMoving = false;
        checkingForStop = false;
        FuelUsed = 0;
        rb = GetComponent<Rigidbody>();
        StartingFuel = LevelFuel + PlayerPrefs.GetInt("StartingFuel", 0);
        MaxSpeed = 75 + PlayerPrefs.GetInt("MaxSpeed", 0);
        if (!AI) ThrusterPower = 50 + PlayerPrefs.GetInt("Power", 0);
        else ThrusterPower = 50;
        Movement();
    }

    public void Update()
    {
        if (IsMoving) Movement();
        UpdateFuelBar();
        CheckForCompleteStop();
        var move_vec = rb.velocity;
        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + FuelUsed;
        Speed.text = "Snelheid: " + (int)move_vec.magnitude;
    }

    private void CheckForCompleteStop()
    {
        if (rb.velocity.magnitude < StoppingSpeed && rb.velocity.magnitude != 0 && !checkingForStop)
        {
            stopTimer = Time.time;
            checkingForStop = true;
        }

        if (checkingForStop && rb.velocity.magnitude < StoppingSpeed && Time.time - stopTimer > StoppingSpeed)
        {
            rb.velocity = Vector3.zero;
            checkingForStop = false;
        }
    }

    private void Movement()
    {
        if (!AI)
        {
            ZAxisMovement = Input.GetAxis("Vertical");
            XAxisMovement = Input.GetAxis("Horizontal");
        }

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
            rb.AddForce(Vector3.forward * ZAxisMovement  *ThrusterPower/3, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(ZAxisMovement * ThrusterPower/3));
            rb.AddForce(Vector3.right * XAxisMovement * ThrusterPower/3, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(XAxisMovement * ThrusterPower/3));
            MotorAnimator(ZAxisMovement,XAxisMovement);
            if(AI)ArrowVisualization(ZAxisMovement, XAxisMovement);
        }
        else
        {
            ThrusterUp.Stop();
            ThrusterBack.Stop();
            ThrusterForward.Stop();
            ThrusterDown.Stop();
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

        if (VerticalControl == 0.0f && HorizontalControl == 0.0f)
        {
            ThrusterUp.Stop();
            ThrusterBack.Stop();
            ThrusterForward.Stop();
            ThrusterDown.Stop();
        }
    }

    public void allowMovement()
    {
        if(AI)Arrows[4].SetActive(true);
        IsMoving = true;
    }

    public void denyMovement()
    {
        IsMoving = false;
    }

    public void UpdateFuelBar()
    {
        float FuelBarLeft = (StartingFuel - FuelUsed) / StartingFuel;
        if (FuelBarLeft < 0)
        {
            FuelBarLeft = 0;
            Invoke("Failed",5);
        }
    
        FuelBar.transform.localScale = new Vector3(1, FuelBarLeft, 1);

        var red = Mathf.Clamp(510-(FuelBarLeft*510), 0, 255);
        var green = Mathf.Clamp((FuelBarLeft)* 510, 0, 255);

        FuelBarSprite.color = new Color(red/255, green/255, 0);
        FuelText.text = (int)(StartingFuel -FuelUsed)/100 + "/\n" + (int)StartingFuel/100;
    }

    private void Failed()
    {
        PlayerFail.Invoke();
    }


    public void trusterUp(float UpTime)
    {
        ZAxisMovement = 1;
        Invoke("trusterStopVertical", UpTime);
    }

    public void trusterDown(float UpTime)
    {
        ZAxisMovement = -1;
        Invoke("trusterStopVertical", UpTime);
    }
    public void trusterRight(float UpTime)
    {
        XAxisMovement = 1;
        Invoke("trusterStopHorizontal", UpTime);
    }
    public void trusterLeft(float UpTime)
    {
        XAxisMovement = -1;
        Invoke("trusterStopHorizontal", UpTime);
    }

    private void trusterStopVertical()
    {
        ZAxisMovement = 0;
    }

    private void trusterStopHorizontal()
    {
        XAxisMovement = 0;
    }

    private void ArrowVisualization(float xMove,float  zMove)
    {
        if (xMove > 0) Arrows[0].SetActive(true);
        else Arrows[0].SetActive(false);

        if (xMove < 0) Arrows[1].SetActive(true);
        else Arrows[1].SetActive(false);

        if (zMove > 0) Arrows[2].SetActive(true);
        else Arrows[2].SetActive(false);

        if (zMove < 0) Arrows[3].SetActive(true);
        else Arrows[3].SetActive(false);
    }

    
}
