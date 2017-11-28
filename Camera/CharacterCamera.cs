using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterCamera : MonoBehaviour {
    public Transform player;
    public Vector3 Mouse_Position;
    public Vector3 mouse_world_position;
    public Vector3 difference;
    private float tracking_time;
    private GameObject theSoulBall;

    // Update is called once per frame
    void Update () {
        Mouse_Position = Input.mousePosition;
        Mouse_Position.z = 0f;
        mouse_world_position = Camera.main.ScreenToWorldPoint(Mouse_Position);
        
        theSoulBall = GameObject.FindGameObjectWithTag("Soul_Ball");

        // Yi still alive.
        if (player != null)
        {
            if (theSoulBall != null)
            {
                print(theSoulBall.transform.position.x);
                mouse_world_position = new Vector3(Mathf.Clamp(mouse_world_position.x - player.position.x, -25f, 25f) / 2.5f, Mathf.Clamp(mouse_world_position.y - player.position.y, -15f, 15f) / 2.5f, 0f);
                Vector3 target = new Vector3((theSoulBall.transform.position.x + player.position.x)/2 + mouse_world_position.x, (theSoulBall.transform.position.y + player.position.y)/2 + mouse_world_position.y, -10f);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 600f);
            }
            else
            {
                mouse_world_position = new Vector3(Mathf.Clamp(mouse_world_position.x - player.position.x, -30f, 30f) / 2.5f, Mathf.Clamp(mouse_world_position.y - player.position.y, -15f, 15f) / 2.5f + 18f, 0f);
                Vector3 target = new Vector3(player.position.x + mouse_world_position.x, player.position.y + mouse_world_position.y, -10f);
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 300f);
            }
        }
    }
}
