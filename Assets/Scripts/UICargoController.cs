using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

public class UICargoController : MonoBehaviour
{

    public GameObject player;
    private Rigidbody rb;
    private LevelManagerFunctions ManagerScript;
    public TextMeshProUGUI CargoNumbers;
    void Start()
    {

      // player = GameObject.FindGameObjectWithTag("Player1");
        rb = player.GetComponent<Rigidbody>();
        ManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManagerFunctions>();
    }


    void Update()
    {
      
        CargoNumbers.text = "" + ManagerScript.CargoAmount[1] + "        " + ManagerScript.CargoAmount[2] + "         " + ManagerScript.CargoAmount[3] +  "\n\nmassa = " + (int)rb.mass + " kg";
    }
}
