using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour {
    private float lifeSpan = 15f;
    private float speed = 0.3f;
	// Use this for initialization
	void Start () {
        speed = speed * Random.Range(0.3f, 1.3f);
	}
	
	// Update is called once per frame
	void Update () {
        lifeSpan -= Time.deltaTime;
        transform.Translate(Vector3.down * speed+Vector3.left*0.5f*speed, Space.World);
        //transform.Rotate(new Vector3(Random.Range(-11f, 11f), Random.Range(-11f, 11f), 0f));
        if (lifeSpan < 0f)
        {
            Destroy(this.gameObject);
        }
	}
}
