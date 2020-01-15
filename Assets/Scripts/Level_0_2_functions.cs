using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level_0_2_functions : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    private Rigidbody rbPlayer;

    public TextMeshProUGUI panelText;
    

    public GameObject[] targets;
    private int path;

    private Animator PanelAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PanelAnimator = panel.GetComponent<Animator>();
        PanelAnimator.SetTrigger("Start");
        path = 0;


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
            if (PlayerPrefs.GetInt("levelProgress0") == 2) PlayerPrefs.SetInt("levelProgress0", 3);
            SceneManager.LoadScene("MissionMenu");
        }
        else if (IsCompleted == 2)
        {
            if (PlayerPrefs.GetInt("levelProgress0") == 2) PlayerPrefs.SetInt("levelProgress0", 3);
            SceneManager.LoadScene("Level_0_3");
        }
        else if(IsCompleted == 3)
        {
            SceneManager.LoadScene("Level_0_2");
        }

    }

    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0 && path == 1)
        {
            panelText.text = "Level Voltooid!";
        

            panel.SetActive(true);
            PanelAnimator.SetBool("IsHidden", false);
            PanelAnimator.SetTrigger("Succes");
        }
    }

    public void closePanel()
    {
        HidePanel();
    }

    public void HalfwayCheckpoint()
    {
        path = 1;
        targets[0].SetActive(false);
    }

    public void LevelFail()
    {
        panelText.text = "Helaas, het moet anders kunnen!";
   
        panel.SetActive(true);
        PanelAnimator.SetBool("IsHidden", false);
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
