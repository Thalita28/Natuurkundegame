using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRecords : MonoBehaviour
{

    public TextMeshProUGUI Records;
    // Start is called before the first frame update

    public void ViewRecordsOf(string i)
    {

        int index = i[1] - 48;
        int block = i[0] - 48;

        if(block == 0) Records.text = "Records van Reddingsmissie " + index + "\n";
        else if(block == 1) Records.text = "Records van Transportmissie " + index + "\n";

        Records.text = Records.text + "--------------------------------------------------\n";
        if (PlayerPrefs.GetFloat("Time_" + block + "_" + index, -1) < 0) Records.text = Records.text + "Snelste tijd: geen\n";
        else Records.text = Records.text + "Snelste tijd: " + PlayerPrefs.GetFloat("Time_" + block + "_" + index).ToString("f1") + "\n";
        Records.text = Records.text + "-----------------------------------------------------------\n";

        if (PlayerPrefs.GetInt("Score_" + block + "_" + index, -1) < 0)
        {
            Records.text = Records.text + "Beste score: geen";
        }
        else
        {
            Records.text = Records.text + "Beste score: " + PlayerPrefs.GetInt("Score_" + block + "_" + index) + " (lager = beter)\n";
            Records.text = Records.text + "Brandstofverbruik: " + PlayerPrefs.GetInt("Fuel_" + block + "_" + index) + "\n";
            Records.text = Records.text + "Tijd: " + PlayerPrefs.GetFloat("ScoreTime_" + block + "_" + index).ToString("f1");
        }


    }
    

  
}
