using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTrustPower : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI ThrusterAmount;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateThrustPower", 0.1f, 0.5f);
    }


    private void UpdateThrustPower()
    {
        ThrusterAmount.text = "Huidige motorkracht: " + (50 + (PlayerPrefs.GetInt("Power", 0) ));
    }
}
