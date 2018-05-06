using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour {


    public GameObject Gate;

	// Use this for initialization
	void Start () {
        Movement.EverythingMoving.Add(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public  void startGame()
    {
        //transform.position = Gate.transform.position;
        Debug.Log("start game");

        StartCoroutine(CallChildren());

    }

    IEnumerator CallChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.GetComponent<Wall>().startGame();
            yield return new WaitForSeconds(0.02f);
        }
        

        
    }
}
