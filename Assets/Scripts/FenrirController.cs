using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenrirController : MonoBehaviour
{

    public float fenrirMoveSpeed;
    public float maxFenrirSpeed;
    public float fenrirRotateSpeed;
    public float fenrirJump;

    public GameObject cam;
    public Transform cameratransform;
    public float cameraRotationSpeed;

    // how far to rotate
    public float cameraRotationMin;
    public float cameraRotationMax;

    float Pitch;
    private Rigidbody fenrirRB;

    bool isGrounded;

    Animator fenrirAnim;

    private void Start()
    {
        fenrirRB = GetComponent<Rigidbody>();
        fenrirAnim = GetComponent<Animator>();
        cameratransform = cam.transform;
    }

    void Update()
    {

        if (Input.GetAxis("Mouse X") > 0)
            transform.Rotate(Vector3.up, fenrirRotateSpeed);

        if (Input.GetAxis("Mouse X") < 0)
            transform.Rotate(Vector3.up, -fenrirRotateSpeed);


        //if (Input.GetAxis("Mouse Y") > 0 && cam.transform.rotation.x < 23)
        //{
           
        //   cam.transform.Rotate(Vector3.right, +cameraRotationSpeed);
            
        //}

        //if (Input.GetAxis("Mouse Y") < 0 && cam.transform.rotation.x > -13 )
        //{
            
        //   cam.transform.Rotate(Vector3.right, -cameraRotationSpeed);
            
        //}
      

        //Pitch -= Input.GetAxis("Mouse Y") * cameraRotationSpeed;
        //Pitch = Mathf.Clamp(Pitch, cameraRotationMin, cameraRotationMax);
        //Vector3 ObjectRotation = new Vector3(Pitch, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
        //cam.transform.eulerAngles = ObjectRotation;
        //cam.transform.localEulerAngles = new Vector3(Mathf.Clamp(cam.transform.localEulerAngles.x, cameraRotationMin, cameraRotationMax), 
        //   cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);

        /*if (fenrirRB.velocity == new Vector3(0,0,0) && !fenrirAnim.GetBool("Jumping"))
        {
            fenrirAnim.speed = 0;
        }

        else
        {
            fenrirAnim.speed = 1;
        }*/

        if (Input.GetAxis("Vertical") > 0)
            fenrirRB.AddForce(transform.right * fenrirMoveSpeed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                fenrirRB.AddForce(transform.up * fenrirJump, ForceMode.Impulse);
                isGrounded = false;
                // fenrirAnim.SetBool("Jumping", true);
            }
        }



        fenrirRB.velocity = Vector3.ClampMagnitude(fenrirRB.velocity, maxFenrirSpeed * 2.0f);

        //RaycastHit raycast;
        //if (Physics.Raycast(transform.position, Vector3.Normalize(transform.position- cam.transform.position), out raycast, Vector3.Magnitude( transform.position- cameratransform.position)))
        //{
        //    if (raycast.collider.tag != "MainCamera")
        //    {
        //        Vector3.Lerp(cam.transform.position, raycast.point, Time.deltaTime*20);
        //    }
        //}


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.name == "TempFloor")
        {
            isGrounded = true;
            //fenrirAnim.SetBool("Jumping", false);
        }
    }

    //void FixedUpdate()
    //{

    //    if (Input.GetAxis("Vertical") > 0)
    //        fenrirRB.AddForce(transform.right * fenrirMoveSpeed);

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (isGrounded)
    //        {
    //            fenrirRB.AddForce(transform.up * fenrirJump, ForceMode.Impulse);
    //            isGrounded = false;
    //           // fenrirAnim.SetBool("Jumping", true);
    //        }
    //    }



    //    //fenrirRB.velocity = Vector3.ClampMagnitude(fenrirRB.velocity, maxFenrirSpeed * 2.0f);
    //}
}