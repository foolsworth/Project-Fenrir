﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGate : MonoBehaviour {

    public GameObject fenrir;
   public bool Activatefog = false;

    // Use this for initialization
    void Start () {
        fenrir.GetComponent<Movement>().Gate = gameObject;
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "fenrir")
        {
            Activatefog = false;
            gameObject.transform.Find("Plane").transform.localScale = new Vector3(0, 0, 0);
            gameObject.transform.position = fenrir.transform.position+(fenrir.transform.right.normalized *20);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "fenrir")
        {
            Activatefog = true;
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