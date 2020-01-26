using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class HangerShopMenuExecute : MonoBehaviour
{
    public ColorPicker Picker;
    public Renderer Trusters;
    public Renderer Cockpit;
    public Renderer Body;
    public TextMeshProUGUI FuelCost;
    public TextMeshProUGUI MaxSpeedCost;
    public TextMeshProUGUI PowerCost;

    public GameObject[] ColorButtons;
    public GameObject[] FuelStuff;
    public GameObject[] EngineStuff;
    private int index = 1;



    private void Start()
    {
        Picker.onValueChanged.AddListener(color =>
        {
            if (index == 0) Trusters.material.color = color;
            else if (index == 1) Body.material.color = color;
            else if (index == 2) Cockpit.material.color = color;


        });
    }
    public void BackToMainMenu()
    {  
         SceneManager.LoadScene("MissionMenu");
    }

    public void OpenColorPicker()
    {
        bool menuState = ColorButtons[0].activeSelf;
        int i;

        for(i=0; i<ColorButtons.Length;i++)
        {
            ColorButtons[i].SetActive(!menuState);
        }

        for (i = 0; i < FuelStuff.Length; i++)
        {
            FuelStuff[i].SetActive(false);
        }

        for (i = 0; i < EngineStuff.Length; i++)
        {
            EngineStuff[i].SetActive(false);
        }

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void OpenFuelShop()
    {
        FuelCost.text = "" + (100 + PlayerPrefs.GetInt("StartingFuel") / 100);

        bool menuState = FuelStuff[0].activeSelf;
        int i;

        for (i = 0; i < FuelStuff.Length; i++)
        {
            FuelStuff[i].SetActive(!menuState);
        }
        for (i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].SetActive(false);
        }

        for (i = 0; i < EngineStuff.Length; i++)
        {
            EngineStuff[i].SetActive(false);
        }

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void OpenEngineShop()
    {
        MaxSpeedCost.text = "" + (100 + (PlayerPrefs.GetInt("MaxSpeed",0)));
        PowerCost.text = "" + (100 + (PlayerPrefs.GetInt("Power",0) ));

        bool menuState = EngineStuff[0].activeSelf;
        int i;

        for (i = 0; i < EngineStuff.Length; i++)
        {
            EngineStuff[i].SetActive(!menuState);
        }
        for (i = 0; i < ColorButtons.Length; i++)
        {
            ColorButtons[i].SetActive(false);
        }

        for (i = 0; i < FuelStuff.Length; i++)
        {
            FuelStuff[i].SetActive(false);
        }

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void TryOnTrusters()
    {
        index = 0;
       // GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void TryOnCockPit()
    {
        index = 2;
   
        //GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    
    public void TryOnBody()
    {
        index = 1;
       
        //GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void ConfirmColors()
    {
        if (PlayerPrefs.GetInt("Coins") > 99)
        {

            Debug.Log(Trusters.material.color.ToString());
            Debug.Log(Body.material.color.ToString());
            Debug.Log(Cockpit.material.color.ToString());


            PlayerPrefs.SetString("TrusterColor", Trusters.material.color.ToString());
            PlayerPrefs.SetString("BodyColor", Body.material.color.ToString());
            PlayerPrefs.SetString("CockpitColor", Cockpit.material.color.ToString());

            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - 100);
        }

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }


    public void UpgradeFuel()
    {
        if (PlayerPrefs.GetInt("Coins") > (100 + PlayerPrefs.GetInt("StartingFuel") / 100)-1)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - (100 + PlayerPrefs.GetInt("StartingFuel") / 100));
            PlayerPrefs.SetInt("StartingFuel", PlayerPrefs.GetInt("StartingFuel") + 5000);
        }

        FuelCost.text = "" + (100 + PlayerPrefs.GetInt("StartingFuel") / 100);
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void UpgradePower()
    {
        if (PlayerPrefs.GetInt("Coins") > (100 + (PlayerPrefs.GetInt("Power",0))) - 1)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - (100 + (PlayerPrefs.GetInt("Power", 0) )));
            PlayerPrefs.SetInt("Power", PlayerPrefs.GetInt("Power") + 50);
        }

        PowerCost.text = "" + (100 + (PlayerPrefs.GetInt("Power", 0)));
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }


    public void UpgradeMaxSpeed()
    {
        if (PlayerPrefs.GetInt("Coins") > (100 + (PlayerPrefs.GetInt("MaxSpeed", 0))) - 1)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - (100 + (PlayerPrefs.GetInt("MaxSpeed", 0))));
            PlayerPrefs.SetInt("MaxSpeed", PlayerPrefs.GetInt("MaxSpeed") + 100);
        }

        MaxSpeedCost.text = "" + (100 + (PlayerPrefs.GetInt("MaxSpeed", 0) ));
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

}
