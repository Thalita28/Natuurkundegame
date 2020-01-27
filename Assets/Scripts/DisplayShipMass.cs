using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayShipMass : MonoBehaviour
{
    private TextMeshProUGUI massa;
    private GameObject player;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        massa = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        massa.text = "Massa schip: " + rb.mass.ToString("f0") + "kg";
    }
}
