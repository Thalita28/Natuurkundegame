using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MissionMenuExecute : MonoBehaviour
{
    [SerializeField] int index;
    private GameObject inputField;
    public GameObject levelMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonPressed()
    {
       
        switch(index)
        {
            case 0:
                foreach (Transform child in transform)
                {
                    if (child.tag == "input")
                    {
                        inputField = child.gameObject;
                        inputField.SetActive(true);
                        EventSystem.current.SetSelectedGameObject(inputField);

                    }

                }
                break;
            case 1: case 2: case 3:
                levelMenu.SetActive(true); break;
            case 4:
                SceneManager.LoadScene("HangerShop"); break;
            case 5:
                SceneManager.LoadScene("MainMenu"); break;
        }
     
    }

    public void NameEditExit()
    {
        inputField.SetActive(false);
    }


}
