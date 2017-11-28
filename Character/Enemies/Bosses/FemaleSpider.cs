using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FemaleSpider : MonoBehaviour {
    public GameObject SpiderEmit;
    public bool damaged;
    public float damagedTime = 0;

    private float SpiderShootingTime = Constants.FemaleSpiderShootingTime;
    public int health = 40;

	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Arrow")
        {
            print("arrow");
            HittedByArrow();
            damagedColor();
            damaged = true;
            damagedTime = Constants.PlayerDamagedTime;
            Destroy(collision.gameObject);
        }
    }

    public void HittedByArrow()
    {
        health--;
    }

    private void damagedColor()
    {
        SpriteRenderer Renderer = GetComponent<SpriteRenderer>();
        Renderer.color = Color.red;
    }

    // Update is called once per frame
    void Update () {
        SpiderShootingTime -= Time.deltaTime;
        if (SpiderShootingTime < 0f)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(SpiderEmit, transform.position, Quaternion.identity);
                SpiderShootingTime = Constants.FemaleSpiderShootingTime;
            }
        }

        if (damagedTime > 0f)
        {
            damagedColor();
            damagedTime -= Time.deltaTime;
        }
        else
        {
            healthyColor();
        }

        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void healthyColor()
    {
        Component[] SpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer Sprite in SpriteRenderers)
        {
            Sprite.color = Color.white;
        }
    }
}
