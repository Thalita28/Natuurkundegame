using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedVectorScript : MonoBehaviour
{
    public GameObject PlayerShip;
    public GameObject PositiveSpeedZ;
    public GameObject NegativeSpeedZ;
    public GameObject PositiveSpeedX;
    public GameObject NegativeSpeedX;
    public int GageScale;

    private void Start()
    {
        PositiveSpeedZ.transform.localScale = new Vector3(0, 0, 0);
        NegativeSpeedZ.transform.localScale = new Vector3(0, 0, 0);
        PositiveSpeedX.transform.localScale = new Vector3(0, 0, 0);
        NegativeSpeedX.transform.localScale = new Vector3(0, 0, 0);
    }
    void Update()
    {
        Rigidbody playerShipRB = PlayerShip.GetComponent<Rigidbody>();
        float PositivePlayerSpeedZ = playerShipRB.velocity.z / GageScale;
        float NegativePlayerSpeedZ = playerShipRB.velocity.z / GageScale;
        float PositivePlayerSpeedX = playerShipRB.velocity.x / GageScale;
        float NegativePlayerSpeedX = playerShipRB.velocity.x / GageScale;

        if (PositivePlayerSpeedZ > 0.0f)
        {
            PositiveSpeedZ.transform.localScale = new Vector3(1, PositivePlayerSpeedZ,1);
        }
        else
        {         
            PositiveSpeedZ.transform.localScale = new Vector3(0, 0, 0);
        }
        if (NegativePlayerSpeedZ < 0.0f)
        {
            NegativeSpeedZ.transform.localScale = new Vector3(1, -NegativePlayerSpeedZ, 1);
        }
        else
        {
            NegativeSpeedZ.transform.localScale = new Vector3(0, 0, 0);
        }
        if (PositivePlayerSpeedX > 0.0f)
        {
            PositiveSpeedX.transform.localScale = new Vector3(PositivePlayerSpeedX, 1, 1);
        }
        else
        {
            PositiveSpeedX.transform.localScale = new Vector3(0, 0, 0);
        }
        if (NegativePlayerSpeedX < 0.0f)
        {
            NegativeSpeedX.transform.localScale = new Vector3(-NegativePlayerSpeedX, 1, 1);
        }
        else
        {
            NegativeSpeedX.transform.localScale = new Vector3(0, 0, 0);
        }



    }

}
