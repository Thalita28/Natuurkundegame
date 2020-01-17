using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayExtraFuel : MonoBehaviour
{

    public TextMeshProUGUI ExtraFuelAmount;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateExtraFuel", 0.1f, 0.5f);
    }


    private void UpdateExtraFuel()
    {
        ExtraFuelAmount.text = "Huidige extra tank: " + (PlayerPrefs.GetInt("StartingFuel", 0)/100);
    }
  
}
