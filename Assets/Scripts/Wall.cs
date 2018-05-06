using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;

        transform.position = initialPos + new Vector3(0.0f, 1000.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        transform.position = Vector3.Lerp(transform.position, initialPos, 5.0f * Time.deltaTime);
    }
}
