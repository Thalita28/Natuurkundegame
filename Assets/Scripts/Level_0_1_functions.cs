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
    public TextMeshProUGUI missionText;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevel(bool IsCompleted)
    {

        Debug.Log("levelProgress0" + " = " + PlayerPrefs.GetInt("levelProgress0"));
        if (IsCompleted)
        {
            if(PlayerPrefs.GetInt("levelProgress0") == 1) PlayerPrefs.SetInt("levelProgress0", 2);

            Debug.Log("levelProgress0" + " = " + PlayerPrefs.GetInt("levelProgress0"));
            SceneManager.LoadScene("MissionMenu");
        }
    }

    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0) EndLevel(true);
    }

    public void closePanel()
    {
        panel.SetActive(false);
    }

    public void halfWayUpdate()
    {
        missionText.text = "Vergeet niet optijd te remmen!";
    }

}
