using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuExecute : MonoBehaviour
{


    public void StartGame()
    {
        SceneManager.LoadScene("MissionMenu");

        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void Options()
    {
        Debug.Log("Open options");
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        Application.Quit();
        GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
