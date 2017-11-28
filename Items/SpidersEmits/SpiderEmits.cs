using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEmits : MonoBehaviour {
    public Vector3 playerPos;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        string _tag = collision.gameObject.tag;

        print("is this yijo ?");
        print(_tag);

        if (_tag == "Player")
        {
            print("hello yijo spider (trigger) hurts you");
            collision.transform.gameObject.GetComponent<YijoStatus>().Damaged(1);
            print(collision.transform.gameObject.GetComponent<YijoStatus>().curr_health);
            if (this != null)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            playerPos += new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f);
        }	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, playerPos, 70f * Time.deltaTime);
	}
}
