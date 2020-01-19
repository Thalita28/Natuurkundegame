﻿using System.Collections;
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
    public TextMeshProUGUI TargetProgress;
    private Animator PanelAnimator;
    public GameObject[] targets;
    public bool UseTimer;
    private int StationIndex;

    [HideInInspector] public int[] CargoAmount = new int[] { 0, 0, 0, 0, 0 };
    public int DifficultyFactor = 1;

    private float timer;
    private float FinishTime;
    private int path;
    private string SceneName;
    [SerializeField] int thisIndex;
    [SerializeField] int thisBlockLevel;
    private string NextSceneName;
    [SerializeField] bool AILevel;

    // Start is called before the first frame update
    void Start()
    {
        CargoAmount = new int[] { 0, 0, 0, 0};
        Debug.Log(CargoAmount.Length);
        StationIndex = 0;
        PanelAnimator = panel.GetComponent<Animator>();
        if(AILevel)PanelAnimator.SetTrigger("Choice");
        else PanelAnimator.SetTrigger("Start");


        SceneName = SceneManager.GetActiveScene().name;

        path = 0;
        CargoAmount[1] = 0;
        CargoAmount[2] = 0;
        CargoAmount[3] = 0;
        rbPlayer = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (path != -1)
        {
            if (thisBlockLevel == 0) TargetProgress.text = "Astronauten gered: " + path + "/" + targets.Length;
            else if (thisBlockLevel == 1) TargetProgress.text = "Vracht afgeleverd: " + path + "/" + targets.Length;
        }
        else
        {
            if (thisBlockLevel == 0) TargetProgress.text = "Astronauten gered: " + targets.Length + "/" + targets.Length;
            else if (thisBlockLevel == 1) TargetProgress.text = "Vracht afgeleverd: " + targets.Length + "/" + targets.Length;
        }
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
            if(thisIndex == 8) SceneManager.LoadScene("MissionMenu");
            else SceneManager.LoadScene("Level_"+thisBlockLevel+"_"+(thisIndex+1));
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
        if (rbPlayer.velocity.magnitude == 0 && path == targets.Length)
        {
            path = -1;

            Playermovement2 PlayerScript = player.GetComponent<Playermovement2>();
            var UsedFuel = PlayerScript.FuelUsedTotal;
            if (UseTimer)
            {
                FinishTime = Time.time - timer;

                if (FinishTime < PlayerPrefs.GetFloat("Time_" + thisBlockLevel + "_" + thisIndex, -1) || PlayerPrefs.GetFloat("Time_" + thisBlockLevel + "_" + thisIndex, -1) == -1)
                {
                    PlayerPrefs.SetFloat("Time_" + thisBlockLevel + "_" + thisIndex, FinishTime);
                    panelText.text = "Level Voltooid!\nFinish tijd: " + FinishTime.ToString("f1") + "  (Nieuw Record!)";
                }
                else panelText.text = "Level Voltooid!\nFinish tijd: " + FinishTime.ToString("f1");

                panelText.text = panelText.text + "\nBrandstofverbruik: " + (int)(UsedFuel / 100);

                UsedFuel /= 1000;

                int Score =(int)(UsedFuel * Mathf.Pow(FinishTime, 1.5f)/DifficultyFactor);
               

                if ( Score < PlayerPrefs.GetInt("Score_" + thisBlockLevel + "_" + thisIndex, -1) || PlayerPrefs.GetInt("Score_" + thisBlockLevel + "_" + thisIndex, -1) == -1)
                {
                    PlayerPrefs.SetInt("Score_" + thisBlockLevel + "_" + thisIndex,Score);
                    PlayerPrefs.SetInt("Fuel_" + thisBlockLevel + "_" + thisIndex, (int)(UsedFuel*10));
                    panelText.text = panelText.text + "\nScore: " + Score + "  (Nieuw Record!)";
                }
                else panelText.text = panelText.text + "\nScore: " + Score;

            }
            else
            {
                panelText.text = "Level Voltooid!";
                panelText.text = panelText.text + "\nBrandstofverbruik: " + (int)(UsedFuel / 100);
            }

            
 
            if (PlayerPrefs.GetInt("levelProgress" + thisBlockLevel) == thisIndex)
            {
                PlayerPrefs.SetInt("Coins", 100 + (DifficultyFactor) + PlayerPrefs.GetInt("Coins", 0));
                CoinAmount.text = ""+ (100 + (DifficultyFactor)) +    "	    verdiend!";
            }
            else
            {
                PlayerPrefs.SetInt("Coins", (100 + (DifficultyFactor))/2 + PlayerPrefs.GetInt("Coins", 0));
                CoinAmount.text = "" + (100 + (DifficultyFactor))/2 + "	    verdiend!";
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
                    StartCoroutine(trustLeft(1.95f, 14));
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
                    StartCoroutine(trustUp(0.8f, 7));
                    StartCoroutine(trustDown(0.77f, 14));
                    StartCoroutine(trustLeft(1.96f, 14));
                    break;
                case 2:
                    StartCoroutine(trustRight(2, 1));
                    StartCoroutine(trustDown(0.8f, 7));
                    StartCoroutine(trustUp(0.77f, 14));
                    StartCoroutine(trustLeft(1.95f, 14));
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
                    StartCoroutine(trustLeft(2, 12f));
                    StartCoroutine(trustLeft(2, 14.1f));
                    StartCoroutine(trustDown(2, 14.1f));
                    StartCoroutine(trustRight(1.97f, 20));
                    StartCoroutine(trustUp(2, 20));
                    StartCoroutine(trustRight(2, 22.1f));
                    StartCoroutine(trustLeft(1.97f, 28));
                    break;
                case 1:
                    StartCoroutine(trustUp(2, 1));
                    StartCoroutine(trustDown(1.97f, 7));
                    StartCoroutine(trustLeft(2, 9));
                    StartCoroutine(trustDown(2, 9));
                    StartCoroutine(trustRight(1.97f, 15));
                    StartCoroutine(trustUp(2, 15));
                    StartCoroutine(trustRight(2, 17));
                    StartCoroutine(trustLeft(1.97f, 23));
                    break;
                case 2:
                    StartCoroutine(trustLeft(2, 1));
                    StartCoroutine(trustRight(2, 7));
                    StartCoroutine(trustUp(2, 7));
                    StartCoroutine(trustLeft(1.97f, 12));
                    StartCoroutine(trustDown(2, 12));
                    StartCoroutine(trustDown(2, 14.1f));
                    StartCoroutine(trustUp(1.97f, 18.5f));
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

    public void StartTimer()
    {
        timer = Time.time;
    }

    public void LoadCargo(string indexstring)
    {
        int index = indexstring[1] - 48;
        int color = indexstring[0] - 48;

        if (StationIndex != 0)
        {
           targets[index].SetActive(false);
           CargoAmount[color]++;
           rbPlayer.mass += (50 * color);
        }

    }

    public void ShipAtStation(int index)
    {
        if (rbPlayer.velocity.magnitude == 0) StationIndex = index;
        else StationIndex = 0;
    }

    public void UnLoadCargo(int color)
    {
        if(StationIndex == color && CargoAmount[color] > 0)
        {
            CargoAmount[color] -= 1;
            path += 1;
            PlayerOnTarget();
        }
    }


}
