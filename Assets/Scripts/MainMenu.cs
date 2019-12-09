using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // speel het spel
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // ga naar de volgende scene waarin het spel kan worden gespeeld
    }
    // Sluit spel af indien er op quit gedrukt wordt
    public void QuitGame()
    {
        Debug.Log("Quit"); // debug omdat in de unity game engine het spel niet sluit op deze manier
        Application.Quit(); // sluit spel af
    }
}
