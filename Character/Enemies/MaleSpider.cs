using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaleSpider : MonoBehaviour {
    // Male spider has hearts and obsorbed by the female spider.
    public GameObject heart;
    private float frequency = Constants.SpiderHeartSpawnTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        frequency -= Time.deltaTime;
        if (frequency<0f)
        {
            Instantiate(heart, gameObject.transform.position, Quaternion.identity);
            frequency = Constants.SpiderHeartSpawnTime;
        }
	}
}
