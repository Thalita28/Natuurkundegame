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
        timer = 0;
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

                int Score = 1000000/(int)(UsedFuel * Mathf.Pow(FinishTime, 1.5f)/DifficultyFactor);
               

                if ( Score > PlayerPrefs.GetInt("Score_" + thisBlockLevel + "_" + thisIndex, -1) || PlayerPrefs.GetInt("Score_" + thisBlockLevel + "_" + thisIndex, -1) == -1)
                {
                    PlayerPrefs.SetInt("Score_" + thisBlockLevel + "_" + thisIndex,Score);
                    PlayerPrefs.SetInt("Fuel_" + thisBlockLevel + "_" + thisIndex, (int)(UsedFuel*10));
                    PlayerPrefs.SetFloat("ScoreTime_" + thisBlockLevel + "_" + thisIndex, FinishTime);
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

    IEnumerator trustRight(int TargetAcc, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterRight(TargetAcc);
    }
    IEnumerator trustUp(int TargetAcc, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterUp(TargetAcc);
    }
    IEnumerator trustDown(int TargetAcc, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterDown(TargetAcc);
    }
    IEnumerator trustLeft(int TargetAcc, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        player.GetComponent<Playermovement2>().trusterLeft(TargetAcc);
    }




    public void ExecuteAnswer(int answer)
    {
        closePanel();
        if (thisIndex == 5 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustRight(50, 1));
                    StartCoroutine(trustLeft(50, 14));
                    break;
                case 1:
                    StartCoroutine(trustRight(50, 1));
                    break;
                case 2:
                    StartCoroutine(trustLeft(50, 1));
                    StartCoroutine(trustRight(50, 8));
                    break;
                case 3:
                    StartCoroutine(trustRight(50,1));
                    break;
            }
        }

        else if (thisIndex == 6 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustRight(50, 1));
                    StartCoroutine(trustDown(50, 6));
                    StartCoroutine(trustUp(50, 9));
                    break;
                case 1:
                    StartCoroutine(trustRight(50, 1));
                    StartCoroutine(trustUp(20, 7));
                    StartCoroutine(trustDown(20, 14));
                    StartCoroutine(trustLeft(50, 14));
                    break;
                case 2:
                    StartCoroutine(trustRight(50, 1));
                    StartCoroutine(trustDown(20, 7));
                    StartCoroutine(trustUp(20, 14));
                    StartCoroutine(trustLeft(50, 14));
                    break;
            }
        }

        else if (thisIndex == 7 && thisBlockLevel == 0)
        {
            switch (answer)
            {
                case 0:
                    StartCoroutine(trustUp(50, 1));
                    StartCoroutine(trustLeft(50, 1));
                    StartCoroutine(trustRight(50, 4));
                    StartCoroutine(trustDown(50, 7));
                    StartCoroutine(trustRight(50, 9));
                    StartCoroutine(trustLeft(50, 12f));
                    StartCoroutine(trustLeft(50, 14.1f));
                    StartCoroutine(trustDown(50, 14.1f));
                    StartCoroutine(trustRight(50, 20));
                    StartCoroutine(trustUp(50, 20));
                    StartCoroutine(trustRight(50, 22.1f));
                    StartCoroutine(trustLeft(50, 28));
                    break;
                case 1:
                    StartCoroutine(trustUp(50, 1));
                    StartCoroutine(trustDown(50, 7));
                    StartCoroutine(trustLeft(50, 9));
                    StartCoroutine(trustDown(50, 9));
                    StartCoroutine(trustRight(50, 16));
                    StartCoroutine(trustUp(50, 16));
                    StartCoroutine(trustRight(50, 18));
                    StartCoroutine(trustLeft(50, 24));
                    break;
                case 2:
                    StartCoroutine(trustLeft(50, 1));
                    StartCoroutine(trustRight(50, 7));
                    StartCoroutine(trustUp(50, 7));
                    StartCoroutine(trustLeft(50, 12));
                    StartCoroutine(trustDown(50, 12));
                    StartCoroutine(trustDown(50, 14.1f));
                    StartCoroutine(trustUp(50, 18.5f));
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

        if (StationIndex != 0 && Vector3.Distance(player.transform.position,targets[index].transform.position) < 400)
        {
           targets[index].SetActive(false);
           CargoAmount[color]++;
           rbPlayer.mass += (500 * color);
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
            rbPlayer.mass -= (500 * color);
            PlayerOnTarget();
        }
    }


}
