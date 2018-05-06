using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatingFirstMaze : MonoBehaviour {

    Fading fader;

    // Use this for initialization
    void Start () {
        fader = GetComponent<Fading>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GateActivate")
        {
            Movement.ConsumablePositions.Clear();
            Movement.EverythingMoving.Clear();

            fader.LoadSceneAsync("Maze2", 4.0f);

        }
    }
}
