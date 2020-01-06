using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public int Boundary  = 30;
 public int speed = 100;
    public int zoomSpeed = 1000;
    public int max_distance = 100;
    private int StartX;
    private int StartZ;

 
 private int theScreenWidth;
 private int theScreenHeight ;
 
void Start()
    {

        StartX = (int)transform.position.x;
        StartZ = (int)transform.position.z;
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    void Update()
    {
        if (Input.mousePosition.x > theScreenWidth - Boundary && transform.position.x < StartX+ max_distance)
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
            //transform.position.x += speed * Time.deltaTime;
        }

        if (Input.mousePosition.x < 0 + Boundary && transform.position.x > StartX - max_distance)
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime, Space.World);
           // transform.position.x -= speed * Time.deltaTime;
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary && transform.position.z < StartZ + max_distance)
        {
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime, Space.World);
            //transform.position.y += speed * Time.deltaTime;
        }

        if (Input.mousePosition.y < 0 + Boundary && transform.position.z > StartZ - max_distance)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime, Space.World);
           // transform.position.y -= speed * Time.deltaTime;
        }

        transform.Translate(new Vector3(0, 1, 0)* -Input.mouseScrollDelta.y*Time.deltaTime* zoomSpeed,Space.World);

    }
}
