using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPageOpener : MonoBehaviour
{


    public GameObject HelpPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenHelpPage()
    {
        bool IsActive = HelpPage.activeSelf;
        HelpPage.SetActive(!IsActive);
    }
}
