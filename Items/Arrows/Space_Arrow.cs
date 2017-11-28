using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Arrow : Normal_Arrow {
    public GameObject Yijo;

    private float time_slowed_period = 0f;

    void Start()
    {
        G = 0.05f;
        LifeSpan = 15f;
        arrow_damage = 1;
        Yijo = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if (LifeSpan < 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            LifeSpan -= Time.deltaTime;
        }
        if (Input.GetKeyDown("space"))
        {
            // Send Yijo to current space_arrow position.
            TransportYijo();
            // Slow time for 2 seconds.
            time_slowed_period = 1.5f;
            Destroy(this.gameObject.GetComponent<Renderer>());
        }
        if (time_slowed_period > 0f)
        {
            Time.timeScale = 0.1f;
            time_slowed_period -= Time.deltaTime*10f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void FixedUpdate()
    {

        Arrow_movement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (shooter != collision.gameObject) // Not itself
        {
            string _tag = collision.gameObject.tag;
            if (_tag != "Arrow")
            {
                if (_tag == "Mage")
                {
                    collision.gameObject.GetComponent<Mage>().getDamaged(arrow_damage);
                    gameObject.transform.parent = collision.gameObject.transform;
                    // Arrow insert into object.
                    gameObject.transform.parent = collision.gameObject.transform;
                }
                else if (_tag == "Player")
                {
                    collision.gameObject.GetComponent<YijoStatus>().Damaged(arrow_damage);
                    gameObject.transform.parent = collision.gameObject.transform;
                    // Arrow insert into object.
                    gameObject.transform.parent = collision.gameObject.transform;
                }

                CircleCollider2D[] BoxCollider2Ds;
                BoxCollider2Ds = gameObject.GetComponents<CircleCollider2D>();
                foreach (CircleCollider2D collider in BoxCollider2Ds)
                {
                    collider.enabled = false;
                }
                // Instantiate an animation
                Destroy(this.gameObject);
            }
        }
    }

    // Transport Yijo
    private void TransportYijo()
    {
        Yijo.transform.position = transform.position;
    }
}
