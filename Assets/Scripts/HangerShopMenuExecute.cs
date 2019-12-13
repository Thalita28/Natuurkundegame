using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HangerShopMenuExecute : MonoBehaviour
{

    [SerializeField] int index;
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

        if (index == 4)
        {
            SceneManager.LoadScene("MissionMenu");
        }

    }
}
