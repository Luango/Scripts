using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour {
    public GameObject player;
    public GameObject heartUI;
    public Transform canvas;
    private int curr_health;
    public List<GameObject> heartList;

    public float height;
    public float width;

	// Use this for initialization
	void Start () {
        curr_health = player.GetComponent<YijoStatus>().curr_health;
        for (int i = 0; i < curr_health; i++) {
            GameObject heart = Instantiate(heartUI, new Vector3(width, height, 0) + Vector3.right * i * 50f, Quaternion.identity);
            heart.transform.SetParent(canvas);
            heartList.Add(heart);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (player.GetComponent<YijoStatus>().curr_health > curr_health)
            {
                // 70f, 870f
                GameObject heart = Instantiate(heartUI, new Vector3(width, height, 0) + Vector3.right * (heartList.Count) * 50f, Quaternion.identity);
                heart.transform.SetParent(canvas);
                heartList.Add(heart);

                curr_health = player.GetComponent<YijoStatus>().curr_health;
            }
            else if (player.GetComponent<YijoStatus>().curr_health < curr_health)
            {
                Destroy(heartList[heartList.Count - 1]);
                heartList.RemoveAt(heartList.Count-1);

                curr_health = player.GetComponent<YijoStatus>().curr_health;
            }
        }
	}
}
