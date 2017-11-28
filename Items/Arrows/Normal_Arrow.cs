using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal_Arrow : MonoBehaviour {
    [HideInInspector]
    public Vector3 velocity;
    public GameObject shooter;
    public GameObject player;
     
    protected float G = 0.05f; // Gravity
    protected float LifeSpan = 8f;
    protected int arrow_damage = 1;

    // Trigger for ground
    public void OnTriggerEnter2D(Collider2D collision)
    {
        string _tag = collision.gameObject.tag;
        print("trigger tag: " + _tag);
        if (_tag == "ground" || _tag == "Ground")
        {
            GameObject children;
            children = this.gameObject.transform.GetChild(0).gameObject;
            Destroy(children);

            CircleCollider2D[] BoxCollider2Ds;
            BoxCollider2Ds = gameObject.GetComponents<CircleCollider2D>();
            foreach (CircleCollider2D collider in BoxCollider2Ds)
            {
                collider.enabled = false;
            }
            if (this != null)
            {
                Destroy(this);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (shooter != collision.gameObject) // Not itself
        {
            string _tag = collision.gameObject.tag;
            print("tag: " + tag);
            if (_tag != "Arrow")
            {
                if (_tag == "Mage")
                {
                    collision.gameObject.GetComponent<Mage>().getDamaged(arrow_damage);
                    gameObject.transform.parent = collision.gameObject.transform;
                    // Arrow insert into object.
                    gameObject.transform.parent = collision.gameObject.transform;
                    Destroy(this);
                }
                else if (_tag == "Player")
                {
                    collision.gameObject.GetComponent<YijoStatus>().Damaged(arrow_damage);
                    gameObject.transform.parent = collision.gameObject.transform;
                    // Arrow insert into object.
                    gameObject.transform.parent = collision.gameObject.transform;
                    Destroy(this);
                }
                else if (_tag == "Enemy")
                {
                    print("bleeding");
                    collision.gameObject.GetComponent<Furfly>().StartBleeding();
                    
                    Destroy(this.gameObject);
                } else if (_tag == "Apple")
                {
                    SpriteSlicer2D.ExplodeSprite(collision.rigidbody.gameObject, 5, 15.0f, true, ref SpriteSlicer2DDemoManager.m_SlicedSpriteInfo);
                    // If we've chosen to fade out fragments once an object is destroyed, add a fade and destroy component
                    for (int spriteIndex = 0; spriteIndex < SpriteSlicer2DDemoManager.m_SlicedSpriteInfo.Count; spriteIndex++)
                    {
                        for (int childSprite = 0; childSprite < SpriteSlicer2DDemoManager.m_SlicedSpriteInfo[spriteIndex].ChildObjects.Count; childSprite++)
                        {
                            if (!SpriteSlicer2DDemoManager.m_SlicedSpriteInfo[spriteIndex].ChildObjects[childSprite].GetComponent<Rigidbody2D>().isKinematic)
                            {
                                SpriteSlicer2DDemoManager.m_SlicedSpriteInfo[spriteIndex].ChildObjects[childSprite].layer = LayerMask.NameToLayer("Apple");
                                SpriteSlicer2DDemoManager.m_SlicedSpriteInfo[spriteIndex].ChildObjects[childSprite].AddComponent<FadeAndDestroy>();
                            }
                        }
                    }
                    SpriteSlicer2DDemoManager.m_SlicedSpriteInfo.Clear();
                    Destroy(this.gameObject);
                    /*
                    gameObject.transform.parent = collision.gameObject.transform;
                    gameObject.GetComponent<Rigidbody2D>().simulated = false;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (-800f));
                    */
                }

                CircleCollider2D[] BoxCollider2Ds;
                BoxCollider2Ds = gameObject.GetComponents<CircleCollider2D>();
                foreach (CircleCollider2D collider in BoxCollider2Ds)
                {
                    collider.enabled = false;
                }
                if (this != null)
                {
                    Destroy(this);
                }
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {

        // Destroy
        if (LifeSpan < 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            LifeSpan -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Arrow_movement();
    }

    // Arrow movement: direction --> click position
    public void Arrow_movement()
    {
        // Gravity
        velocity.y -= G*Time.fixedDeltaTime*60f;
        // Dot product of Velocity and (1, 0, 0) to calculate the angle
        float dot_Result = Vector3.Dot(velocity.normalized, new Vector3(1f, 0f, 0f));
        // Orientation is always largern than 0
        float orientation = Mathf.Acos(dot_Result);
        if (velocity.y < 0f) { orientation = -orientation; }
        //Debug.Log("velocity = " + velocity);

        // Update arrow's position and orientation
        transform.position += velocity * Time.fixedDeltaTime * 10f;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, orientation * 180f / Mathf.PI));
    }

    // Calculate the initial orientation to make arrow hit the clicked point.
    // delta_x = clicked_xPos - current_character_xPos
    // delta_y = clicked_yPos - current_character_yPos
    // v = speed
    // G = gravity
    public float Arrow_init_orientation(float delta_x, float delta_y, float v, float G)
    {
        float a = (delta_y * delta_y + delta_x * delta_x);
        float b = (delta_y * G * delta_x * delta_x) / (v * v) - delta_x * delta_x;
        float c = (Mathf.Pow(delta_x, 4) * Mathf.Pow(G, 2)) / (4 * Mathf.Pow(v, 4));

        // Calculate the roots (-b+sqrt(b^2-fac))/2a 
        float cos_sq_1 = (-b + Mathf.Sqrt(Mathf.Pow(b, 2) - 4 * a * c)) / (2 * a);

        float orientation = Mathf.Acos(Mathf.Sqrt(cos_sq_1));
        orientation = orientation * 180f / Mathf.PI;

        float y_threshold = -(delta_x * delta_x * G) / (v * v * 2);

        // if delta_y < 0 delta_y > delta_y'
        if (delta_x >= 0)
        {
            if (delta_y < 0 && delta_y < y_threshold)
            {
                orientation = -orientation;
            }
        }
        else
        {
            if (delta_y < 0 && delta_y < y_threshold)
            {
                orientation = -180f + orientation;
            }
            else
            {
                orientation = 180f - orientation;
            }
        }
        orientation = orientation * Mathf.PI / 180f;

        return orientation;
    }
}
