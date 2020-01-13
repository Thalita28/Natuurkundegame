using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level_0_3_functions : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    private Rigidbody rbPlayer;
    public TextMeshProUGUI missionText;
    public TextMeshProUGUI panelText;

    public GameObject[] Buttons;
    public GameObject[] targets;
   
    private int path;
  

    // Start is called before the first frame update
    void Start()
    {
        Buttons[0].SetActive(true);
        Buttons[1].SetActive(false);
        Buttons[2].SetActive(false);
        Buttons[3].SetActive(false);
        Buttons[4].SetActive(false);
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
            if (PlayerPrefs.GetInt("levelProgress0") == 3) PlayerPrefs.SetInt("levelProgress0", 4);
            SceneManager.LoadScene("MissionMenu");
        }
        else if (IsCompleted == 2)
        {
            if (PlayerPrefs.GetInt("levelProgress0") == 3) PlayerPrefs.SetInt("levelProgress0", 4);
            SceneManager.LoadScene("Level_0_3");
        }
        else if (IsCompleted == 3)
        {
            SceneManager.LoadScene("Level_0_3");
        }

    }

    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0 && path == 4)
        {
            panelText.text = "SUPER NICE MAN, LEKKER GEDAAN";
            Buttons[0].SetActive(false);
            Buttons[1].SetActive(true);
            Buttons[2].SetActive(true);
            Buttons[3].SetActive(false);
            Buttons[4].SetActive(false);
            panel.SetActive(true);
        }
    }

    public void closePanel()
    {
        panel.SetActive(false);
    }

    public void HalfwayCheckpoint(int index)
    {
        path++;
        targets[index].SetActive(false);
    }

    public void HalfWayStopPoint(int index)
    {
        if (rbPlayer.velocity.magnitude == 0)
        {
            path++;
            targets[index].SetActive(false);
        }
    }

    public void LevelFail()
    {
        panelText.text = "JAMMER JOH NIET GEHAALD";
        Buttons[0].SetActive(false);
        Buttons[1].SetActive(false);
        Buttons[2].SetActive(false);
        Buttons[3].SetActive(true);
        Buttons[4].SetActive(true);
        panel.SetActive(true);

    }

}