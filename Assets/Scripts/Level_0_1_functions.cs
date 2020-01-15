using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level_0_1_functions : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    private Rigidbody rbPlayer;

    public TextMeshProUGUI panelText;
 

    private Animator PanelAnimator;


    // Start is called before the first frame update
    void Start()
    {
        PanelAnimator = panel.GetComponent<Animator>();
        PanelAnimator.SetTrigger("Start");
     
        rbPlayer = player.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevel(int IsCompleted)
    {

        if (IsCompleted == 0)
        {


            SceneManager.LoadScene("MissionMenu");
        }
        else if (IsCompleted == 1)
        {
            if (PlayerPrefs.GetInt("levelProgress0") == 1) PlayerPrefs.SetInt("levelProgress0", 2);
            SceneManager.LoadScene("MissionMenu");
        }
        else if (IsCompleted == 2)
        {
            if (PlayerPrefs.GetInt("levelProgress0") == 1) PlayerPrefs.SetInt("levelProgress0", 2);
            SceneManager.LoadScene("Level_0_2");
        }
        else if (IsCompleted == 3)
        {
            SceneManager.LoadScene("Level_0_1");
        }
    }

    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0)
        {
            panelText.text = "SUPER NICE MAN, LEKKER GEDAAN";
  
            panel.SetActive(true);
            PanelAnimator.SetBool("IsHidden", false);
            PanelAnimator.SetTrigger("Succes");
        }
    }

    public void closePanel()
    {
        HidePanel();
    }

 

    public void LevelFail()
    {
        panelText.text = "JAMMER JOH NIET GEHAALD";
  
        panel.SetActive(true);
        PanelAnimator.SetBool("IsHidden",false);
        PanelAnimator.SetTrigger("Fail");

    }

    public void HidePanel()
    {
        PanelAnimator.SetBool("IsHidden", true);

    }

    public void ShowPanel()
    {
        PanelAnimator.SetBool("IsHidden", false);

    }

}
