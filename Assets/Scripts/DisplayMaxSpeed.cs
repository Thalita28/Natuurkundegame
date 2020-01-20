using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayMaxSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI MaxSpeedAmount;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateMaxSpeed", 0.1f, 0.5f);
    }


    private void UpdateMaxSpeed()
    {
        MaxSpeedAmount.text = "Huidige maximale snelheid: " + (PlayerPrefs.GetInt("MaxSpeed" , 0) +75) + "  (per richting)";
    }
}
