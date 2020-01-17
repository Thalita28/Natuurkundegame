using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayCoins : MonoBehaviour
{

    public TextMeshProUGUI CoinAmount;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateCoins", 0.1f, 0.5f);
    }

   private void UpdateCoins()
    {
        CoinAmount.text = "" + PlayerPrefs.GetInt("Coins", 0);
    }
 
}
