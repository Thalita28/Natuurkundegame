using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetPlayerName : MonoBehaviour
{
    public TextMeshProUGUI playername;
    private TextMeshProUGUI myText;
    void Start()
    {
        myText = gameObject.GetComponent<TextMeshProUGUI>();
        myText.text = PlayerPrefs.GetString("PlayerName", "Player Name");
    }

    public void SubmitName()
    {
        int length = playername.text.Length;
        if (length > 10) length = 10;
        string decent_name = playername.text.Substring(0, length);
        PlayerPrefs.SetString("PlayerName", decent_name);
        //myText.text = playername.text; //alleen voor testen in editor
        myText.text = PlayerPrefs.GetString("PlayerName", "Player Name");
        Debug.Log(decent_name);


    }
}
