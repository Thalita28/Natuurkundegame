using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuExecute : MonoBehaviour
{


    public void StartGame()
    {

        int i;
        for(i = 0; i < 3; i++)
        {
            if (PlayerPrefs.GetInt("levelProgress" + i) == 0) PlayerPrefs.SetInt("levelProgress" + i, 1);
        }

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
