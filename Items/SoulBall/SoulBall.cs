using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBall : MonoBehaviour {
    private float lifeTime = 15f;

    public GameObject Yijo;

    // Use this for initialization
    void Start () {
        Yijo = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            YijoShots.hasShootSoulBall = false;

            Destroy(this.gameObject);
        }

        // Ignore player, enemies
        int p_layer = LayerMask.NameToLayer("Player");
        int soul_ball = LayerMask.NameToLayer("Soul_Ball");
        int enemies = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(p_layer, soul_ball);
        Physics2D.IgnoreLayerCollision(enemies, soul_ball);

        if (Input.GetKeyDown("space"))
        {
            YijoShots.hasShootSoulBall = false;

            // Send Yijo to current space_arrow position.
            TransportYijo();
            Destroy(this.gameObject);
        }
    }


    // Transport Yijo
    private void TransportYijo()
    {
        Yijo.transform.position = transform.position;
    }

}
