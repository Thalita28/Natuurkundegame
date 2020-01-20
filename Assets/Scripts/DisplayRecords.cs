using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRecords : MonoBehaviour
{

    public TextMeshProUGUI Records;
    // Start is called before the first frame update

    public void ViewRecordsOf(int i)
    {
        Records.text = "Records van Reddingsmissie " + i + "\n";
        Records.text = Records.text + "--------------------------------------------------\n";
        if (PlayerPrefs.GetFloat("Time_0_" + i, -1) < 0) Records.text = Records.text + "Snelste tijd: geen\n";
        else Records.text = Records.text + "Snelste tijd: " + PlayerPrefs.GetFloat("Time_0_" + i).ToString("f1") + "\n";
        Records.text = Records.text + "-----------------------------------------------------------\n";

        if (PlayerPrefs.GetInt("Score_0_" + i, -1) < 0)
        {
            Records.text = Records.text + "Beste score: geen";
        }
        else
        {
            Records.text = Records.text + "Beste score: " + PlayerPrefs.GetInt("Score_0_" + i) + " (lager = beter)\n";
            Records.text = Records.text + "Brandstof: " + PlayerPrefs.GetInt("Fuel_0_" + i) + "\n";
            Records.text = Records.text + "Tijd: " + ((float)((float)PlayerPrefs.GetInt("Score_0_" + i) / ((float)PlayerPrefs.GetInt("Fuel_0_" + i)/10))).ToString("f1");
        }


    }
    

  
}
