using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuitGameOnClick : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quit"); // debug omdat in de unity game engine het spel niet sluit op deze manier
        Application.Quit(); // sluit spel af
    }
}