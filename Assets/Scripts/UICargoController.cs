using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using UnityEngine;

public class UICargoController : MonoBehaviour
{
    
    private LevelManagerFunctions ManagerScript;
    public TextMeshProUGUI CargoNumbers;
    // Start is called before the first frame update
    void Start()
    {

        
        ManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelManagerFunctions>();
    }


    // Update is called once per frame
    void Update()
    {
        int massa = 100;
        massa += ManagerScript.CargoAmount[1] * 50;
        massa += ManagerScript.CargoAmount[2] * 100;
        massa += ManagerScript.CargoAmount[3] * 150;
        CargoNumbers.text = "" + ManagerScript.CargoAmount[1] + "        " + ManagerScript.CargoAmount[2] + "         " + ManagerScript.CargoAmount[3] +  "\n\nMassa schip: " + massa;
    }
}
