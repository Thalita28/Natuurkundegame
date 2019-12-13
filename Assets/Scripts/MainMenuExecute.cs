using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuExecute : MonoBehaviour
{

    [SerializeField] int index;
    // Start is called before the first frame update

    public void ButtonPressed()
    {
        if (index == 0) SceneManager.LoadScene("MissionMenu");
        else if (index == 2) Application.Quit();

    }
}
