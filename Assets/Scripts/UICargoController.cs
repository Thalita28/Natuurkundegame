using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

public class UICargoController : MonoBehaviour
{
    
    private LevelManagerFunctions ManagerScript;
    public TextMeshProUGUI CargoNumbers;
    void Start()
    {    
        ManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManagerFunctions>();
    }


    void Update()
    {
        int massa = 100;
        massa += ManagerScript.CargoAmount[1] * 50;
        massa += ManagerScript.CargoAmount[2] * 100;
        massa += ManagerScript.CargoAmount[3] * 150;
        CargoNumbers.text = "" + ManagerScript.CargoAmount[1] + "        " + ManagerScript.CargoAmount[2] + "         " + ManagerScript.CargoAmount[3] +  "\n\nmassa = " + massa + " kg";
    }
}
