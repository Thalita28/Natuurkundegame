using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level11: MonoBehaviour
{
    public TextMeshProUGUI MissionText;
    public GameObject TextScreenHandeler;
    public GameObject MissionTextHandeler;
    public static bool FirstTrigger = false;
    public static bool LastTrigger = false;

    void Start()
    {
        TextScreenHandeler.SetActive(true);
        MissionTextHandeler.SetActive(true);
        TextScreenHandeler.transform.localScale = new Vector3(1, 0.5f, 1);
        MissionText.text = "Laten we beginnen.";
    }
    void Update()
    {
        if (FirstTrigger == true)
        {
            MissionText.text = "Probeer op tijd tot stilstand te komen";
            TextScreenHandeler.transform.localScale = new Vector3(1.0f, 1.0f, 1);
        }

        if (LastTrigger == true)
        {
            MissionText.text = "Je hebt gewonnen";
            TextScreenHandeler.transform.localScale = new Vector3(1, 0.5f, 1);
        }
    }
}
