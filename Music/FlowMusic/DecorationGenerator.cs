using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationGenerator : MonoBehaviour
{
    public GameObject Player;
    public GameObject Item1;
    public int Item1Num = 4;
    public GameObject Item2;
    public int Item2Num = 5;

    private List<GameObject> Item1s = new List<GameObject>();
    private List<GameObject> Item2s = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Item1s.Count < Item1Num)
        {
            GameObject item1 = Instantiate(Item1, new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f) + Player.transform.position, Quaternion.identity);
            Item1s.Add(item1);
        }
        if (Item2s.Count < Item2Num)
        {
            GameObject item2 = Instantiate(Item2, new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0f) + Player.transform.position, Quaternion.identity);
            Item2s.Add(item2);
        }
        foreach (GameObject item in Item1s.ToArray())
        {
            if (item == null)
                Item1s.Remove(item);

            if (Vector3.Distance(item.transform.position, Player.transform.position) > 50f)
            {
                Item1s.Remove(item);
                Destroy(item);
            }
        }
        foreach (GameObject item in Item2s.ToArray())
        {
            if (item == null)
                Item2s.Remove(item);

            if (Vector3.Distance(item.transform.position, Player.transform.position) > 50f)
            {
                Item2s.Remove(item);
                Destroy(item);
            }
        }
    }
}