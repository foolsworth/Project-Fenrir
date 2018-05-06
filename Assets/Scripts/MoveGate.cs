using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveGate : MonoBehaviour {


   // player
   public GameObject fenrir;
   public bool Activatefog = false;
    Fading fader;
    public GameObject mazeManager;
    bool wentThrough = false;

    // Use this for initialization
    void Start () {
        Movement.EverythingMoving.Add(gameObject);
        fader = GetComponent<Fading>();
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "fenrir" )
        {
            if (!wentThrough)
            {
                Activatefog = false;
                gameObject.transform.Find("Plane").transform.localScale = new Vector3(0, 0, 0);
                gameObject.transform.position = fenrir.transform.position + (fenrir.transform.right.normalized * 20);
            }
            else
            {
                gameObject.tag = "wall";
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "fenrir")
        {
            Activatefog = true;
        }

        if (other.gameObject.name == "GateActivate")
        {
            wentThrough = true;
            mazeManager.GetComponent<MazeManager>().startGame();
            fenrir.GetComponent<SphereCollider>().enabled = false;
            fenrir.GetComponent<CapsuleCollider>().enabled = true;
        }
    }
    // Update is called once per frame
    void Update () {
        if (Activatefog && gameObject.transform.Find("Plane").transform.localScale!= new Vector3(6.57956F, 6.57956F, 6.57956F))
        {
            gameObject.transform.Find("Plane").transform.localScale = Vector3.Lerp(gameObject.transform.Find("Plane").transform.localScale, new Vector3(6.57956F, 6.57956F, 6.57956F), Time.deltaTime);
        }
	}
}

