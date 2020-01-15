using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class Level_0_4_functions : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    private Rigidbody rbPlayer;
  
    public TextMeshProUGUI panelText;

    private Animator PanelAnimator;


    private int path;


    // Start is called before the first frame update
    void Start()
    {
   
        path = 0;

        PanelAnimator = panel.GetComponent<Animator>();
        PanelAnimator.SetTrigger("Choice");
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
            if (PlayerPrefs.GetInt("levelProgress0") == 4) PlayerPrefs.SetInt("levelProgress0", 5);
            SceneManager.LoadScene("MissionMenu");
        }
        else if (IsCompleted == 2)
        {
            if (PlayerPrefs.GetInt("levelProgress0") == 4) PlayerPrefs.SetInt("levelProgress0", 5);
            SceneManager.LoadScene("Level_0_4");
        }
        else if (IsCompleted == 3)
        {
            SceneManager.LoadScene("Level_0_4");
        }

    }

    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0 )
        {
            panelText.text = "SUPER NICE MAN, LEKKER GEDAAN";
 

            panel.SetActive(true);
            PanelAnimator.SetTrigger("Succes");
        }
    }

    public void closePanel()
    {
        panel.SetActive(false);
        
    }

  

    public void LevelFail()
    {
        panelText.text = "JAMMER JOH NIET GEHAALD";

        panel.SetActive(true);
        PanelAnimator.SetTrigger("Fail");

    }

    IEnumerator trustRight(float UpTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        player.GetComponent<Playermovement2>().trusterRight(UpTime);
    }
    IEnumerator trustUp(float UpTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterUp(UpTime);
    }
    IEnumerator trustDown(float UpTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterDown(UpTime);
    }
    IEnumerator trustLeft(float UpTime, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterLeft(UpTime);
    }


   

    public void ExecuteAnswer(int answer)
    {
        closePanel();
        switch(answer)
        {
            case 0:
                StartCoroutine(trustRight(2, 0));
                StartCoroutine(trustLeft(1.99f, 10));
                break;
            case 1:
                StartCoroutine(trustRight(2, 0));
                break;
            case 2:
                StartCoroutine(trustLeft(2, 0));
                StartCoroutine(trustRight(2, 7));
                break;
            case 3:
                StartCoroutine(trustRight(2, 5));
                break;

        }
    }


    public void HidePanel()
    {
        PanelAnimator.SetBool("IsHidden", true);
   
    }

    public void ShowPanel()
    {
        PanelAnimator.SetBool("IsHidden",false);

    }
}
