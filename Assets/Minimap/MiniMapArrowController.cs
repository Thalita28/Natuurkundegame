using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapArrowController : MonoBehaviour
{

    public GameObject Player;
    private Rigidbody rb;
    private GameObject OtherCamera;

    private float angle = 0;
    private float StartingY;

    // Start is called before the first frame update
    void Start()
    {
        StartingY = transform.position.y;
        rb = Player.GetComponent<Rigidbody>();
        OtherCamera = GameObject.FindGameObjectWithTag("MinimapCamera");

    }

    // Update is called once per frame
    void Update()
    {
        float Scale = StartingY/Vector3.Distance(OtherCamera.transform.position, Player.transform.position);
        if (Scale < 1) Scale = 1;

        transform.localScale = new Vector3(Scale,Scale, Scale);
        // Vector3 targetDir = target.position - transform.position;
        // float angle = Vector3.Angle(targetDir, transform.forward);

        if (rb.velocity.z > 0) angle = Vector3.Angle(rb.velocity, Player.transform.forward) - 90;
        else angle = Vector3.Angle(rb.velocity, Player.transform.forward) * -1 + 270;

        transform.eulerAngles = new Vector3(
        transform.eulerAngles.x,
        transform.eulerAngles.y,
        angle
        );

    }
}
