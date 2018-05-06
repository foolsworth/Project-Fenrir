using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour {

    public GameObject player;
    Movement fenrirMoveScript;


	// Use this for initialization
	void Start () {
        fenrirMoveScript = player.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.name == "fenrir")
        {
            fenrirMoveScript.moveSpeed += 0.5f;
        }

        Destroy(gameObject);
    }
}
