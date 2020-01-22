using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

public class UICargoController : MonoBehaviour
{

    private GameObject player;
    private Rigidbody rb;
    private LevelManagerFunctions ManagerScript;
    public TextMeshProUGUI CargoNumbers;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        ManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManagerFunctions>();
    }


    void Update()
    {
      
        CargoNumbers.text = "" + ManagerScript.CargoAmount[1] + "        " + ManagerScript.CargoAmount[2] + "         " + ManagerScript.CargoAmount[3] +  "\n\nmassa = " + (int)rb.mass + " kg";
    }
}
