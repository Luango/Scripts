using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHearts : MonoBehaviour {

    public GameObject boss;
    public GameObject heartUI;
    public Transform canvas;
    private int curr_health;
    public List<GameObject> heartList;

    public float height;
    public float width;
    public float gap;
    private bool ini = true;

    // Use this for initialization
    void Start()
    {
        curr_health = boss.GetComponent<FemaleSpider>().health;
        for (int i = 0; i < curr_health; i++)
        {
            //GameObject heart = Instantiate(heartUI, new Vector3(width, height, 0) + Vector3.right * i * gap, Quaternion.identity);
            //heart.transform.SetParent(canvas);
            //heartList.Add(heart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (boss != null)
        {
            if (ini)
            {
                ini = false;
                for (int i = 0; i < curr_health; i++)
                {
                    GameObject heart = Instantiate(heartUI, Vector3.zero, Quaternion.identity);
                    heart.transform.SetParent(canvas);
                    heart.transform.position = new Vector3(width, height, 0) + Vector3.right * i * gap;
                    heartList.Add(heart);
                }
            }
            else if (boss.GetComponent<FemaleSpider>().health > curr_health)
            {
                // 70f, 870f 
                GameObject heart = Instantiate(heartUI, Vector3.zero, Quaternion.identity);
                heart.transform.SetParent(canvas);
                heart.transform.position = heartList[curr_health-1].transform.position + Vector3.right * gap;
                heartList.Add(heart);

                curr_health = boss.GetComponent<FemaleSpider>().health;
            }
            else if (boss.GetComponent<FemaleSpider>().health < curr_health && boss.GetComponent<FemaleSpider>().health >= 0)
            {
                Destroy(heartList[heartList.Count - 1]);
                heartList.RemoveAt(heartList.Count - 1);

                curr_health = boss.GetComponent<FemaleSpider>().health;
            }
        }
        
    }
}
