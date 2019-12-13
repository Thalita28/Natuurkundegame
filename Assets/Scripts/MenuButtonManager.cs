using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{

    public int index;
    public bool AllowScrol;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        AllowScrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        //codes for openingup levels
       /* if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerPrefs.SetInt("levelProgress1", 6);
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.SetInt("levelProgress1", 1);
        }*/


        if(Input.GetAxis("Vertical") != 0 && AllowScrol)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else index = 0;

                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else index = maxIndex;
                }
                keyDown = true;
            }
            
        }
        else keyDown = false;
    }

  
}
