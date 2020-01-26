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
    public GameObject[] MissionTargets;

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
    private float FuelUsed = 0.0f;
    private float OldFuelUsed = 0;
    public float FuelUsedTotal = 0;
    private float MaxSpeed;
    private int StartingFuel;
    [SerializeField] int LevelFuel;
    [SerializeField] float StoppingSpeed;
    [SerializeField] bool AI = false;
    [SerializeField] bool Parameters = false;
    private float ZAxisMovement;
    private float XAxisMovement;
    private bool isTanking;
    private float TargetXAxis, TargetZAxis = 0;
    private Vector3 TargetPosition;
    private bool AutoPilotOn = false;

    private GameObject SliderPanel;
    private TextMeshProUGUI SliderText;
    private Slider Sliders;
    private TextMeshProUGUI SliderText2;
    private Slider Sliders2;
    // private float startingMass;

    void Start()
    {

        MissionTargets = GameObject.FindGameObjectsWithTag("StationPlatform");
        if (AI) Arrows[4].SetActive(false);
        IsMoving = false;
        checkingForStop = false;
        FuelUsed = 0;
        FuelUsedTotal = 0;
        rb = GetComponent<Rigidbody>();
        StartingFuel = LevelFuel + PlayerPrefs.GetInt("StartingFuel", 0);
        MaxSpeed = 75 + PlayerPrefs.GetInt("MaxSpeed", 0);
        rb.mass += StartingFuel / 100;
        if (!AI) ThrusterPower = 500 + PlayerPrefs.GetInt("Power", 0);
        else ThrusterPower = 500;
        Movement();
        if (Parameters) GetSliderPanel();


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            AutoPilotOn = true;
            AI = true;
            FindNearestWayPoint(1);

        }

        if (Input.GetKeyDown(KeyCode.P) && !AI)
        {
            AutoPilotOn = true;
            AI = true;
            FindNearestWayPoint(0);
        }
    }


    private void FixedUpdate()
    {

       
        if (AutoPilotOn)
        {
            GetToPosition();
            CheckIfTargetReached();
        }
        if (AI) GetToVelocity();

        if (IsMoving) Movement();
        UpdateFuelBar();
        CheckForCompleteStop();
        var move_vec = rb.velocity;
        MotionFeedback.text = "Speed: " + move_vec.magnitude + "\nVector: " + move_vec + "\nFuel used: " + FuelUsed;
        Speed.text = "v = " + (int)move_vec.magnitude + " m/s";

        if (isTanking) AddFuel();

        if (OldFuelUsed != FuelUsed && !Parameters)
        {
            rb.mass += (OldFuelUsed - FuelUsed) / 100;

            OldFuelUsed = FuelUsed;
        }

        if (Parameters) FuelUsed = 0;

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
            rb.AddForce(Vector3.forward * ZAxisMovement * ThrusterPower, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(ZAxisMovement * ThrusterPower)) / 10;
            FuelUsedTotal += Mathf.Abs((int)(ZAxisMovement * ThrusterPower)) / 10;
            rb.AddForce(Vector3.right * XAxisMovement * ThrusterPower, ForceMode.Impulse);
            FuelUsed += Mathf.Abs((int)(XAxisMovement * ThrusterPower)) / 10;
            FuelUsedTotal += Mathf.Abs((int)(XAxisMovement * ThrusterPower)) / 10;
            MotorAnimator(ZAxisMovement, XAxisMovement);
            if (AI && !AutoPilotOn) ArrowVisualization(ZAxisMovement, XAxisMovement);
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

    public void allowMovement()
    {
        if (AI && !AutoPilotOn) Arrows[4].SetActive(true);
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
            Invoke("Failed", 5);
        }

        FuelBar.transform.localScale = new Vector3(1, FuelBarLeft, 1);

        var red = Mathf.Clamp(510 - (FuelBarLeft * 510), 0, 255);
        var green = Mathf.Clamp((FuelBarLeft) * 510, 0, 255);

        FuelBarSprite.color = new Color(red / 255, green / 255, 0);
        FuelText.text = (int)(StartingFuel - FuelUsed) / 100 + "/\n" + (int)StartingFuel / 100 + " kg";
    }

    private void Failed()
    {
        PlayerFail.Invoke();
    }


    public void trusterUp(int TargetAcc)
    {

        TargetZAxis += TargetAcc;
        //ZAxisMovement = 1;
        //Invoke("trusterStopVertical", UpTime);
    }

    public void trusterDown(int TargetAcc)
    {
        TargetZAxis -= TargetAcc;
        // ZAxisMovement = -1;
        //Invoke("trusterStopVertical", UpTime);
    }
    public void trusterRight(int TargetAcc)
    {
        TargetXAxis += TargetAcc;
        //XAxisMovement = 1;
        //Invoke("trusterStopHorizontal", UpTime);
    }
    public void trusterLeft(int TargetAcc)
    {
        TargetXAxis -= TargetAcc;
        // XAxisMovement = -1;
        // Invoke("trusterStopHorizontal", UpTime);
    }

    private void trusterStopVertical()
    {
        ZAxisMovement = 0;
    }

    private void trusterStopHorizontal()
    {
        XAxisMovement = 0;
    }

    private void ArrowVisualization(float xMove, float zMove)
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

    public void TankFuel()
    {
        if (rb.velocity.magnitude == 0) isTanking = true;
        else isTanking = false;

    }

    private void AddFuel()
    {
        if (FuelUsed > 0) FuelUsed -= 160;
        if (FuelUsed < 0) FuelUsed = 0;
    }


    public void FreezeMovement()
    {
        //goeie plek voor crash animatie
        denyMovement();
        rb.velocity = Vector3.zero;
    }

    public void GetToVelocity()
    {
        float accuracy = 1f;
        float corrector = (rb.mass / ThrusterPower) / 1.5f;

        //float diff = Mathf.Abs(rb.velocity.x - TargetXAxis);
        if (rb.velocity.x - TargetXAxis > accuracy) XAxisMovement = -1;
        else if (TargetXAxis - rb.velocity.x > accuracy) XAxisMovement = 1;
        else XAxisMovement = 0;

        //diff = Mathf.Abs(rb.velocity.z - TargetZAxis);
        if (rb.velocity.z - TargetZAxis > accuracy) ZAxisMovement = -1;
        else if (TargetZAxis - rb.velocity.z > accuracy) ZAxisMovement = 1;
        else ZAxisMovement = 0;
    }



    private void GetToPosition()
    {
        Vector3 diff = transform.position - TargetPosition;
        float factor, factor2;
        float targetSpeed = (MaxSpeed * 1.3f);
        float factor3 = ((StartingFuel - FuelUsed) * 7.5f) / rb.mass;
        if (targetSpeed > factor3) targetSpeed = factor3;

        float corrector = (rb.mass / ThrusterPower) * 1.7f;

        if (diff[0] > 0) diff[0] += 2.5f;
        if (diff[2] < 0) diff[2] -= 2.5f;
        if (diff[0] < 0) diff[0] -= 2.5f;
        if (diff[2] > 0) diff[2] += 2.5f;


        TargetXAxis = -diff[0] / corrector;
        TargetZAxis = -diff[2] / corrector;

        factor = Mathf.Abs(TargetZAxis) / (Mathf.Abs(TargetXAxis) + Mathf.Abs(TargetZAxis));
        factor2 = Mathf.Abs(TargetXAxis) / (Mathf.Abs(TargetXAxis) + Mathf.Abs(TargetZAxis));
        if (TargetXAxis > targetSpeed) TargetXAxis = targetSpeed;
        if (TargetXAxis < -targetSpeed) TargetXAxis = -targetSpeed;
        if (TargetZAxis > targetSpeed) TargetZAxis = targetSpeed;
        if (TargetZAxis < -targetSpeed) TargetZAxis = -targetSpeed;

        TargetZAxis *= factor;
        TargetXAxis *= factor2;


    }

    private void FindNearestWayPoint(int next)
    {
        float distance = 9999999;
        int NearestWayPoint = 0;
        int i;
        for (i = 0; i < MissionTargets.Length; i++)
        {
            if (Vector3.Distance(transform.position, MissionTargets[i].transform.position) < distance)
            {
                distance = Vector3.Distance(transform.position, MissionTargets[i].transform.position);
                NearestWayPoint = i;
            }
        }

        if (next == 1)
        {
            NearestWayPoint = (int)Random.Range(0, MissionTargets.Length);
        }

        TargetPosition = new Vector3(MissionTargets[NearestWayPoint].transform.position.x, transform.position.y, MissionTargets[NearestWayPoint].transform.position.z);
    }


    private void CheckIfTargetReached()
    {
        if (Vector3.Distance(TargetPosition, transform.position) < 60 && rb.velocity.magnitude < 1)
        {
            AI = false;
            AutoPilotOn = false;
        }
    }


    private void GetSliderPanel()
    {
        SliderPanel = GameObject.FindGameObjectWithTag("ParaPanel");
        SliderPanel.SetActive(true);
        SliderText = GameObject.FindGameObjectWithTag("ThrusterPowerText").GetComponent<TextMeshProUGUI>();
        SliderText2 = GameObject.FindGameObjectWithTag("MassText").GetComponent<TextMeshProUGUI>();
        Sliders = GameObject.FindGameObjectWithTag("ThrusterPowerSlider").GetComponent<Slider>();
        Sliders2 = GameObject.FindGameObjectWithTag("MassSlider").GetComponent<Slider>();

        Sliders.value = ThrusterPower;
        Sliders2.value = rb.mass;
        SliderText.text = "Motorkracht: " + ThrusterPower;
        SliderText2.text = "Massa: " + rb.mass;
    }

    public void OpenParameterPanel()
    {
        Sliders.value = ThrusterPower;
        Sliders2.value = rb.mass;
        SliderText.text = "Motorkracht: " + ThrusterPower;
        SliderText2.text = "Massa: " + rb.mass;
        SliderPanel.SetActive(true);
    }

    public void MassChange()
    {
        rb.mass = Sliders2.value;
        if (rb.mass < 1000) rb.mass = 1000;
        SliderText2.text = "Massa: " + rb.mass.ToString("f0");
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    public void ThrustPowerChange()
    {
        ThrusterPower = (int)Sliders.value;
        SliderText.text = "Motorkracht: " + ThrusterPower;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

    }
}
