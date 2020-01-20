using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public GameObject Player;
    public int Boundary  = 30;
 public int speed = 100;
    public int zoomSpeed = 1000;
    public int max_distance = 100;
    private int StartX;
    private int StartZ;
    private int extraSpeed;
 
 private int theScreenWidth;
 private int theScreenHeight ;

    private bool FollowPlayer;
    private GameObject OtherCamera;
 
void Start()
    {
        OtherCamera = GameObject.FindGameObjectWithTag("MinimapCamera");
        FollowPlayer = false;
        StartX = (int)transform.position.x;
        StartZ = (int)transform.position.z;
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (FollowPlayer) FollowPlayer = false;
            else FollowPlayer = true;
        }

        if(FollowPlayer)transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);


        if (Input.GetMouseButton(0)) extraSpeed = 5;
        else extraSpeed = 1;
      

        if (Input.mousePosition.x > theScreenWidth - Boundary && transform.position.x < StartX + max_distance)
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * extraSpeed* Time.deltaTime, Space.World);
        //transform.position.x += speed * Time.deltaTime;
                FollowPlayer = false;
        }

        if (Input.mousePosition.x < 0 + Boundary && transform.position.x > StartX - max_distance)
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * extraSpeed * Time.deltaTime, Space.World);
        // transform.position.x -= speed * Time.deltaTime;
            FollowPlayer = false;
         }

        if (Input.mousePosition.y > theScreenHeight - Boundary && transform.position.z < StartZ + max_distance)
        {
            transform.Translate(new Vector3(0, 0, 1) * speed * extraSpeed * Time.deltaTime, Space.World);
        //transform.position.y += speed * Time.deltaTime;
         FollowPlayer = false;
         }

        if (Input.mousePosition.y < 0 + Boundary && transform.position.z > StartZ - max_distance)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * extraSpeed * Time.deltaTime, Space.World);
        // transform.position.y -= speed * Time.deltaTime;
                FollowPlayer = false;
         }



        if (Input.mousePosition.x < 683 && Input.mousePosition.y > 1508 )
        {
            OtherCamera.transform.Translate(new Vector3(0, 1, 0) * -Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, Space.World);
        }
        else transform.Translate(new Vector3(0, 1, 0) * -Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed, Space.World);
    }
}
