using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow S;
    public GameObject player;
    public PixelPerfectCamera cam;
    public BoxCollider2D b;

    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;
    public bool moveH; //allows the camera to move horizontally
    public bool moveV;
    public bool moving;
    private float size; 
    private float newX, newY, refX, refY, refSize;


    private void Start()
    {
        S = this;
        moving = false;
        moveH = false;
        moveV = false;
        transform.position = new Vector3(-2.78f, -1.04f, -10.0f);
        cam.assetsPPU = 24;


        newX = 0.0f;
        newY = 0.0f;
        refX = 0.0f;
        refY = 0.0f;
        refSize = 0.0f;
        size = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 cameraposition = transform.position;
        float camSize = cam.assetsPPU;

        if (moving)
        {
            cameraposition.x = Mathf.SmoothDamp(cameraposition.x, newX, ref refX, 0.5f);
            cameraposition.y = Mathf.SmoothDamp(cameraposition.y, newY, ref refY, 0.5f);
            camSize = Mathf.SmoothDamp(camSize, size, ref refSize, 0.5f);

            moving = !(moveH || moveV);
        } else
        {
            if (moveH)
            {
                b.enabled = true;
                cameraposition.x = Mathf.SmoothDamp(cameraposition.x, playerposition.x, ref xVelocity, 0.5f);
            }
            if (moveV)
            {
                b.enabled = false;
                cameraposition.y = Mathf.SmoothDamp(cameraposition.y, playerposition.y, ref yVelocity, 0.5f);
            }
       }

        transform.position = cameraposition;
        cam.assetsPPU = (int)camSize;
    }

    public void MoveCamera(float x, float y, float s)
    {
        newX = x;
        newY = y;
        size = s;
        moving = true;
        moveH = false;
        moveV = false;
    }




}
