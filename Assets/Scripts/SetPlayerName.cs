using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    public TextMeshProUGUI playername;
    private TextMeshProUGUI myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<TextMeshProUGUI>();
        myText.text = PlayerPrefs.GetString("PlayerName", "Player Name");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubmitName()
    {
        string decent_name = playername.text.Substring(0, 10);
        PlayerPrefs.SetString("PlayerName", decent_name);
        //myText.text = playername.text; //alleen voor testen in editor
        myText.text = PlayerPrefs.GetString("PlayerName", "Player Name");
        Debug.Log(decent_name);


    }
}
