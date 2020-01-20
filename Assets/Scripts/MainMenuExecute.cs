using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuExecute : MonoBehaviour
{

    public GameObject[] Opties;

    public void StartGame()
    {

        int i;
        for(i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("levelProgress" + i) == 0) PlayerPrefs.SetInt("levelProgress" + i, 1);
        }

        //PlayerPrefs.SetInt("StartingFuel", 0);

        SceneManager.LoadScene("MissionMenu");

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void Options()
    {
       
            bool menuState = Opties[0].activeSelf;
            int i;

            for (i = 0; i < Opties.Length; i++)
            {
                Opties[i].SetActive(!menuState);
            }

            GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void CompleteReset()
    {

        PlayerPrefs.SetInt("Coins", 5000);

        PlayerPrefs.SetInt("Power", 0);
        PlayerPrefs.SetInt("MaxSpeed", 0);
        PlayerPrefs.SetInt("StartingFuel", 0);

        PlayerPrefs.SetInt("levelProgress0", 1);
        PlayerPrefs.SetInt("levelProgress1", 1);


        PlayerPrefs.SetString("TrusterColor", "RGBA(1.000, 0.000, 0.000, 1.000)");
        PlayerPrefs.SetString("BodyColor", "RGBA(0.000, 1.000, 0.500, 1.000)");
        PlayerPrefs.SetString("CockpitColor", "RGBA(0.000, 1.000, 1.000, 1.000)");

        int i;

        for(i=1; i < 9; i++)
        {
            PlayerPrefs.SetInt("Score_0_"+i, -1);
            PlayerPrefs.SetInt("Fuel_0_" + i, -1);
            PlayerPrefs.SetFloat("Time_0_" + i, -1);
            PlayerPrefs.SetFloat("ScoreTime_0_" + i,-1);
        }

        for (i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("Score_1_" + i, -1);
            PlayerPrefs.SetInt("Fuel_1_" + i, -1);
            PlayerPrefs.SetFloat("Time_1_" + i, -1);
            PlayerPrefs.SetFloat("ScoreTime_1_" + i, -1);
        }

        PlayerPrefs.SetString("PlayerName", "Name...");


    }

    public void QuitGame()
    {
        Application.Quit();
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

  
}
