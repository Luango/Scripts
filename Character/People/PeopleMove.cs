using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Movement();
	}

    private void Movement()
    {
        this.gameObject.transform.Translate(Vector2.right * Time.deltaTime);
    }
}
