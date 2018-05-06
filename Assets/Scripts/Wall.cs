using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    Vector3 initialPos;
    bool start = false;

	// Use this for initialization
	void Start () {
        initialPos = transform.localPosition;

        transform.localPosition = initialPos + new Vector3(0.0f, 500.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(start && transform.localPosition != initialPos)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialPos, 20f * Time.deltaTime);
        }
	}

    public void startGame()
    {
        start = true;
    }
}
