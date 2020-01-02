using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement2 : MonoBehaviour
{
    public int Boundary  = 50;
 public int speed = 5;
    public int zoomSpeed = 400;


 
 private int theScreenWidth;
 private int theScreenHeight ;
 
void Start()
    {
        theScreenWidth = Screen.width;
        theScreenHeight = Screen.height;
    }

    void Update()
    {
        if (Input.mousePosition.x > theScreenWidth - Boundary)
        {
            transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
            //transform.position.x += speed * Time.deltaTime;
        }

        if (Input.mousePosition.x < 0 + Boundary)
        {
            transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime, Space.World);
           // transform.position.x -= speed * Time.deltaTime;
        }

        if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            transform.Translate(new Vector3(0, 0, 1) * speed * Time.deltaTime, Space.World);
            //transform.position.y += speed * Time.deltaTime;
        }

        if (Input.mousePosition.y < 0 + Boundary)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime, Space.World);
           // transform.position.y -= speed * Time.deltaTime;
        }

        transform.Translate(new Vector3(0, 1, 0)* Input.mouseScrollDelta.y*Time.deltaTime* zoomSpeed,Space.World);

    }
}
