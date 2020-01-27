using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MissionMenuExecute : MonoBehaviour
{
    private GameObject inputField;
    public GameObject canvas;
    public GameObject[] levelMenu;
    public GameObject InputField;
    public GameObject LevelRecords;
    private GameObject ExplainButton;

    private void Start()
    {
        ExplainButton = GameObject.FindGameObjectWithTag("ExplainButton");
        ExplainButton.SetActive(false);
    }

    public void EditPlayerName()
    {
       
        InputField.SetActive(true);
        EventSystem.current.SetSelectedGameObject(InputField);
 
    }

    public void OpenLevelMenu0()
    {
        bool menuState = levelMenu[0].activeSelf;
        levelMenu[0].SetActive(!menuState);
        ExplainButton.SetActive(true);

        LevelRecords.SetActive(false);

        if (levelMenu[1].activeSelf) levelMenu[1].SetActive(false);
        if (levelMenu[2].activeSelf) levelMenu[2].SetActive(false);

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void OpenLevelMenu1()
    {
        bool menuState = levelMenu[1].activeSelf;
        levelMenu[1].SetActive(!menuState);
        ExplainButton.SetActive(true);


        LevelRecords.SetActive(false);

        if (levelMenu[0].activeSelf) levelMenu[0].SetActive(false);
        if (levelMenu[2].activeSelf) levelMenu[2].SetActive(false);

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

    }

    public void OpenLevelMenu2()
    {
        bool menuState = levelMenu[2].activeSelf;
        levelMenu[2].SetActive(!menuState);

        if (levelMenu[1].activeSelf) levelMenu[1].SetActive(false);
        if (levelMenu[0].activeSelf) levelMenu[0].SetActive(false);

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

    }

    public void ToShipHanger()
    {
        SceneManager.LoadScene("HangerShop");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }


    public void NameEditExit()
    {
        InputField.SetActive(false);
    }

    public void OpenRecords()
    {
        bool menuState = LevelRecords.activeSelf;
        LevelRecords.SetActive(!menuState);

        levelMenu[0].SetActive(false);
        levelMenu[1].SetActive(false);
        levelMenu[2].SetActive(false);
        ExplainButton.SetActive(false);
    }

    public void OpenExplains()
    {
        SceneManager.LoadScene("Explains");
    }




}
