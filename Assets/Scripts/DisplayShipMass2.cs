using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayShipMass2 : MonoBehaviour
{
    private TextMeshProUGUI massa;
    public GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
       // player = GameObject.FindGameObjectWithTag("Player1");
        rb = player.GetComponent<Rigidbody>();
        massa = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        massa.text = "Massa schip: " + rb.mass.ToString("f0") + "kg";
    }
}
