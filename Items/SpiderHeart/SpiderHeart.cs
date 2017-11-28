using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderHeart : MonoBehaviour {
    private GameObject FemaleSpider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Find the FemaleSpider.
        // find
        FemaleSpider = GameObject.FindGameObjectWithTag("FemaleSpider");
        // Move the heart to the FemaleSpider.
        if (FemaleSpider != null)
        {
            gameObject.transform.Translate((FemaleSpider.transform.position - gameObject.transform.position).normalized * 50f * Time.deltaTime);
        }

        if (FemaleSpider != null)
        {
            if (Vector3.Distance(gameObject.transform.position, FemaleSpider.transform.position) < 8f)
            {
                if (FemaleSpider.GetComponent<FemaleSpider>().health < 63)
                {
                    FemaleSpider.GetComponent<FemaleSpider>().health += 1;
                }
                Destroy(this.gameObject);
            }
        }
	}
}
