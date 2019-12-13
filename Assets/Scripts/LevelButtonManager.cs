using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelButtonManager : MonoBehaviour
{

    public int index;

    public int MenuIndex;
    [SerializeField] bool keyDown;
    private int maxIndex;

    [SerializeField] MenuButton Button;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    private void OnEnable()
    {
        maxIndex = PlayerPrefs.GetInt("levelProgress" + MenuIndex, 1);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else index = maxIndex;

                }
                else if (Input.GetAxis("Horizontal") < 0)
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

        if (index == 0)
        {
            index = 1;
            Button.ReturnToMenu();
        }
    }

    


}
