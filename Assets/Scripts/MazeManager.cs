using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {


    public GameObject Gate;

	// Use this for initialization
	void Start () {
        startGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void startGame()
    {
        transform.position = Gate.transform.position; 
    }

    IEnumerator CallChildren(GameObject child)
    {
        child.GetComponent<Wall>().startGame();

        yield return new WaitForSeconds(1.0f);
    }
}
