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

    public GameObject[] ColorButtons;
    public GameObject[] FuelStuff;

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

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void TryOnTrusters()
    {
        Trusters.material.color = Picker.CurrentColor;
       // GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void TryOnCockPit()
    {
        Cockpit.material.color = Picker.CurrentColor;
        //GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    
    public void TryOnBody()
    {
        Body.material.color = Picker.CurrentColor;
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
   



}
