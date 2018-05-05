using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour {

    
    public GameObject world;
    public GameObject cam;

    public GameObject Gate;

    Animator fenrirAnimator;
   
    
    // original Vec3 of camera
    Vector3 camOrigin;

    // the angle from player to the cam
    float anglePlayerToCam;

    // the initial angle from player to the cam
    float initialAnglePlayerToCam;

    public float moveSpeed;
    

    float worldTextureOffsetX;
    float worldTextureOffsetY;
    Renderer worldRend;

    bool movingUp;
    bool movingDown;
    bool movingLeft;
    bool movingRight;

    public float cameraRotationSpeed;

    // how far to rotate
    public float cameraRotationMin;
    public float cameraRotationMax;
    

    // Use this for initialization
    void Start () {

        if (world != null)
            worldRend = world.gameObject.GetComponent<Renderer>();

        //cam = Camera.main.gameObject;

        if (cam != null)
        {
            camOrigin = cam.transform.up;
            initialAnglePlayerToCam = Vector3.SignedAngle(camOrigin, cam.transform.up, transform.up);
        }



        fenrirAnimator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {

        // Go Up
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("moving up");
            movingUp = true;
        }

        // Go Down
        if (Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("moving down");
            movingDown = true;
        }


        //Go Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            //Debug.Log("moving left");
            movingLeft = true;
        }


        // Go Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            //Debug.Log("moving right");
            movingRight = true;
        }

        // Stop going Up
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log("stopped moving up");
            movingUp = false;
        }

        // Stop going down
        if (Input.GetKeyUp(KeyCode.S))
        {
            //Debug.Log("stopped moving down");
            movingDown = false;
        }


        // Stop going Left
        if (Input.GetKeyUp(KeyCode.A))
        {
            //Debug.Log("stopped moving left");
            movingLeft = false;
        }


        // Stop going Right
        if (Input.GetKeyUp(KeyCode.D))
        {
            //Debug.Log("stopped moving right");
            movingRight = false;
        }

        // recalculate the angle from player to cam
        anglePlayerToCam = Vector3.SignedAngle(camOrigin, cam.transform.up, transform.up);


        // moving texture offest and the gate to give illusion of movement 

        if (movingUp)
        {
            worldTextureOffsetX += -transform.right.x* moveSpeed * Time.deltaTime;
            worldTextureOffsetY += -transform.right.z *moveSpeed* Time.deltaTime;

            Gate.transform.position += new Vector3(-transform.right.x * moveSpeed*6 * Time.deltaTime, 0, -transform.right.z * moveSpeed * 6 * Time.deltaTime);

            //worldTextureOffsetX -= moveSpeed * 0.001f;
        }


        // rotate player and camera
        if (movingLeft)
        {
            //worldTextureOffsetY -= moveSpeed * 0.001f;
            //fenrirAnimator.SetBool("movingLeft", true);

            cam.transform.RotateAround(transform.position, transform.up, -cameraRotationSpeed);
            transform.Rotate(transform.up, -cameraRotationSpeed);
        }

        // rotate player and camera
        if (movingRight)
        {
        //    worldTextureOffsetY += moveSpeed * 0.001f;

        //    // only rotate camera if limit is not reached
        //    //if (anglePlayerToCam <= cameraRotationMax)
        //    //{
         cam.transform.RotateAround(transform.position, transform.up, +cameraRotationSpeed);
         transform.Rotate(transform.up, +cameraRotationSpeed);
           
            //    //}
        }

        //if (!movingLeft && !movingRight)
        //{
        //    if (anglePlayerToCam <= cameraRotationMax + 10 && anglePlayerToCam > initialAnglePlayerToCam)
        //    {
        //        cam.transform.RotateAround(transform.position, transform.up, -cameraRotationSpeed);
        //        transform.Rotate(transform.up, -cameraRotationSpeed);
        //    }

        //    if (anglePlayerToCam >= cameraRotationMin - 10 && anglePlayerToCam < initialAnglePlayerToCam)
        //    {
        //        cam.transform.RotateAround(transform.position, transform.up, +cameraRotationSpeed);
        //        transform.Rotate(transform.up, +cameraRotationSpeed);
        //    }

        //}


        // stop walk animation if not moving
        if (!movingUp && !movingDown && !movingLeft && !movingRight)
        {
            fenrirAnimator.speed = 0;
        }

        // resume walk animation if animation paused and  you started moving
        else
        {
            fenrirAnimator.speed = 1;
        }


        
        worldRend.material.SetTextureOffset("_MainTex", new Vector2(worldTextureOffsetX, worldTextureOffsetY));
        

    }
}
