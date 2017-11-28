using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class FlowMusicPlayer : MonoBehaviour {
    private float speed = 1500f;
    private Rigidbody2D body;

    public Transform StoryTransform;
    private static FlowMusicPlayer instance = null;
    public static FlowMusicPlayer Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance!= null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        StoryTransform = transform.Find("StoryPos");
    }

    // Use this for initialization
    void Start () { 
        body = GetComponent<Rigidbody2D>();
    } 

    // Update is called once per frame
    void FixedUpdate () {

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 v = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            v.Normalize();
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
            body.AddForce(v * speed, ForceMode2D.Force);
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        // use accleration control 
        Vector2 v2 = new Vector2(Input.acceleration.x, Input.acceleration.y);  

        float angle = Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        body.AddForce(v2 * speed * 4f, ForceMode2D.Force);
#endif
    }
}
