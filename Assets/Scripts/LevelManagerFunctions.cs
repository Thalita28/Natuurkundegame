using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class LevelManagerFunctions : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    private Rigidbody rbPlayer;
    public TextMeshProUGUI panelText;
    public TextMeshProUGUI CoinAmount;
    private Animator PanelAnimator;
    public GameObject[] targets;

    private int path;
    private string SceneName;
    [SerializeField] int thisIndex;
    [SerializeField] int thisBlockLevel;
    private string NextSceneName;
    [SerializeField] bool AILevel;

    // Start is called before the first frame update
    void Start()
    {

        PanelAnimator = panel.GetComponent<Animator>();
        if(AILevel)PanelAnimator.SetTrigger("Choice");
        else PanelAnimator.SetTrigger("Start");


        SceneName = SceneManager.GetActiveScene().name;

        path = 10 - targets.Length;

        
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndLevel(string TargetAction)
    {

        if (TargetAction == "FailMenu")
        {
            SceneManager.LoadScene("MissionMenu");
        }
        else if (TargetAction == "SuccesMenu")
        {
            if (PlayerPrefs.GetInt("levelProgress" + thisBlockLevel) == thisIndex) PlayerPrefs.SetInt("levelProgress" + thisBlockLevel, (thisIndex +1));
            
            SceneManager.LoadScene("MissionMenu");
        }
        else if (TargetAction == "NextLevel")
        {
            if (PlayerPrefs.GetInt("levelProgress" + thisBlockLevel) == thisIndex) PlayerPrefs.SetInt("levelProgress" + thisBlockLevel, (thisIndex+1));
            
            SceneManager.LoadScene("Level_"+thisBlockLevel+"_"+(thisIndex+1));
        }
        else if (TargetAction == "Retry")
        {
            SceneManager.LoadScene(SceneName);
        }

    }


    public void HalfwayCheckpoint(int index)
    {
        path++;
        targets[index].SetActive(false);
    }


    public void PlayerOnTarget()
    {
        if (rbPlayer.velocity.magnitude == 0 && path == 10)
        {
            path = 0;
            panelText.text = "Level Voltooid!";

            if (PlayerPrefs.GetInt("levelProgress" + thisBlockLevel) == thisIndex)
            {
                PlayerPrefs.SetInt("Coins", 100 + PlayerPrefs.GetInt("Coins", 0));
                CoinAmount.text = "100	    verdiend!";
            }
            else
            {
                PlayerPrefs.SetInt("Coins", 50 + PlayerPrefs.GetInt("Coins", 0));
                CoinAmount.text = "50	    verdiend!";
            }

            panel.SetActive(true);
            PanelAnimator.SetBool("IsHidden", false);
            PanelAnimator.SetTrigger("Succes");
        }
    }

    public void closePanel()
    {
            panel.SetActive(false);
    }



    public void LevelFail()
    {
        panelText.text = "Helaas, niet gehaald. Probeer het op een andere manier!";

        panel.SetActive(true);
        PanelAnimator.SetBool("IsHidden", false);
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
        if (thisIndex == 5 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustRight(2, 1));
                    StartCoroutine(trustLeft(1.98f, 12));
                    break;
                case 1:
                    StartCoroutine(trustRight(2, 1));
                    break;
                case 2:
                    StartCoroutine(trustLeft(2, 1));
                    StartCoroutine(trustRight(2, 8));
                    break;
                case 3:
                    StartCoroutine(trustRight(2.5f, 1));
                    break;
            }
        }

        else if (thisIndex == 6 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustRight(2, 1));
                    StartCoroutine(trustDown(2, 6));
                    StartCoroutine(trustUp(2, 9));
                    break;
                case 1:
                    StartCoroutine(trustRight(2, 1));
                    StartCoroutine(trustUp(2, 6));
                    StartCoroutine(trustLeft(2, 9));
                    StartCoroutine(trustDown(2, 9));
                    break;
                case 2:
                    StartCoroutine(trustRight(2, 1));
                    StartCoroutine(trustDown(0.8f, 6));
                    StartCoroutine(trustUp(0.77f, 12));
                    StartCoroutine(trustLeft(1.95f, 12));
                    break;
            }
        }

        else if (thisIndex == 7 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustUp(2, 1));
                    StartCoroutine(trustLeft(2, 1));
                    StartCoroutine(trustRight(2, 4));
                    StartCoroutine(trustDown(2, 7));
                    StartCoroutine(trustRight(2, 9));
                    StartCoroutine(trustLeft(2, 12));
                    StartCoroutine(trustLeft(2, 14.1f));
                    StartCoroutine(trustDown(2, 14.1f));
                    StartCoroutine(trustRight(1.97f, 19.5f));
                    StartCoroutine(trustUp(2, 19.5f));
                    StartCoroutine(trustRight(2, 21.5f));
                    StartCoroutine(trustLeft(1.97f, 26.5f));
                    break;
                case 1:
                    StartCoroutine(trustUp(2, 1));
                    StartCoroutine(trustDown(1.97f, 6));
                    StartCoroutine(trustLeft(2, 8));
                    StartCoroutine(trustDown(2, 8));
                    StartCoroutine(trustRight(1.97f, 13));
                    StartCoroutine(trustUp(2, 13));
                    StartCoroutine(trustRight(2, 15f));
                    StartCoroutine(trustLeft(1.97f, 19.5f));
                    break;
                case 2:
                    StartCoroutine(trustLeft(2, 1));
                    StartCoroutine(trustRight(2, 6));
                    StartCoroutine(trustUp(2, 6));
                    StartCoroutine(trustLeft(1.97f, 11));
                    StartCoroutine(trustDown(2, 11));
                    StartCoroutine(trustDown(2, 13.1f));
                    StartCoroutine(trustUp(1.97f, 17.5f));
                    break;
            }
        }
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
