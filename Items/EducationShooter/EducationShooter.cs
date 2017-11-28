using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EducationShooter : MonoBehaviour {
    public GameObject EducationBullet;
    private float waitToGenerate = 3f;
    public Transform iniTransform;

	// Use this for initialization
	void Start () {
        // Find prefab from the assets.
	}
	
	// Update is called once per frame
	void Update () {
        if (waitToGenerate < 0f)
        {
            Instantiate(EducationBullet, iniTransform.position, Quaternion.identity);
            waitToGenerate = 3f;
        }
        waitToGenerate -= Time.deltaTime;
	}
}
