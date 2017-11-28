using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furfly : MonoBehaviour {
    public Transform startTransform;
    public Transform endTransform; 
    public float speed;
    private bool isAtEnd;
    private Vector3 startPos;
    private Vector3 endPos;

    public GameObject bleeding;

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.gameObject.tag == "Player")
        {
            print("Hitted Player!");
        }
        if(collision.gameObject.tag == "Arrow")
        {
            print("Hitted by arrow");
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    private void Awake()
    {
        speed = 13f;
        isAtEnd = false;
        startPos = startTransform.position;
        endPos = endTransform.position;
    }

    public void StartBleeding()
    {
        print("start bleeding");
        GameObject tempBleeding = Instantiate(bleeding, transform);
        tempBleeding.transform.SetParent(null);
        print(tempBleeding);

    }

    // Update is called once per frame
    void Update () {
        if(isAtEnd == false)
        {
            // Move to end
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPos, step);
            // Claculate distance to end
            if(Vector3.Distance(this.gameObject.transform.position, endPos) < 0.05f)
            {
                isAtEnd = true;
            }
        } else
        {
            // Move to start
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPos, step);
            if (Vector3.Distance(this.gameObject.transform.position, startPos) < 0.05f)
            {
                isAtEnd = false;
            }
        }
	}
}
