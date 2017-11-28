using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<YijoStatus>().curr_health += 1;
            Destroy(this.gameObject);
        }
    }
}
