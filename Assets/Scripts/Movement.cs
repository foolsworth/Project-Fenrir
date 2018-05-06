using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Movement : MonoBehaviour {

    
    public GameObject world;
    public GameObject cam;

    public static List<GameObject> EverythingMoving= new List<GameObject>();

    Animator fenrirAnimator;
   
    
    // original Vec3 of camera
    Vector3 camOrigin;

    // the angle from player to the cam
    float anglePlayerToCam;

    // the initial angle from player to the cam
    float initialAnglePlayerToCam;

    public float moveSpeed;
    public float slowingRate;
    

    float worldTextureOffsetX;
    float worldTextureOffsetY;
    Renderer worldRend;

    bool movingUp;
    bool movingDown;
    bool movingLeft;
    bool movingRight;
    public bool inwall;

    public float cameraRotationSpeed;

    // how far to rotate
    public float cameraRotationMin;
    public float cameraRotationMax;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "wall")
        {
            inwall = true;
            movingUp=false;
            movingDown= true;
            movingLeft = false;
            movingRight = false;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            inwall = false;
            movingUp = false;
            movingDown = false;

            movingLeft = false;
            movingRight = false;
        }
    }

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

        slowingRate = 0.0005f;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (moveSpeed <= 0.0f)
        {
            StartCoroutine(Restart());
        }

        if (moveSpeed > 0.0f)
        {
            moveSpeed -= slowingRate;
        }


        // Go Up
        if (Input.GetKeyDown(KeyCode.W) && !inwall)
        {
            //Debug.Log("moving up");&& !inwall
            movingUp = true;
        }

        // Go Down
        //if (Input.GetKeyDown(KeyCode.S) && !inwall)
        //{
        //    //Debug.Log("moving down");
        //    movingDown = true;
        //}


        //Go Left
        if (Input.GetKeyDown(KeyCode.A) && !inwall)
        {
            //Debug.Log("moving left");
            movingLeft = true;
        }


        // Go Right
        if (Input.GetKeyDown(KeyCode.D) && !inwall)
        {
            //Debug.Log("moving right");
            movingRight = true;
        }

        // Stop going Up
        if (Input.GetKeyUp(KeyCode.W) && !inwall)
        {
            //Debug.Log("stopped moving up");
            movingUp = false;
        }

        //// Stop going down
        //if (Input.GetKeyUp(KeyCode.S) && !inwall)
        //{
        //    //Debug.Log("stopped moving down");
        //    movingDown = false;
        //}


        // Stop going Left
        if (Input.GetKeyUp(KeyCode.A) && !inwall)
        {
            //Debug.Log("stopped moving left");
            movingLeft = false;
        }


        // Stop going Right
        if (Input.GetKeyUp(KeyCode.D) && !inwall)
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

            foreach (GameObject thing in EverythingMoving) {

                thing.transform.position += new Vector3(-transform.right.x * moveSpeed * 6 * Time.deltaTime, 0, -transform.right.z * moveSpeed * 6 * Time.deltaTime);
            }

            //worldTextureOffsetX -= moveSpeed * 0.001f;
        }

        if (inwall)
        {
            worldTextureOffsetX -= -transform.right.x * moveSpeed * Time.deltaTime;
            worldTextureOffsetY -= -transform.right.z * moveSpeed * Time.deltaTime;

            foreach (GameObject thing in EverythingMoving)
            {

                thing.transform.position -= new Vector3(-transform.right.x * moveSpeed * 6 * Time.deltaTime, 0, -transform.right.z * moveSpeed * 6 * Time.deltaTime);
            }

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
        if ((!movingUp && !movingDown && !movingLeft && !movingRight) || moveSpeed <= 0.0f)
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

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(4.0f);
        EverythingMoving.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
