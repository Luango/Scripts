using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicStar : MonoBehaviour {
    private float LifeSpan = 5f;
    private float RotSpan = 0.05f;

	// Use this for initialization
	void Start () {
        Vector3 size = transform.localScale;
        transform.localScale = new Vector3(0f, 0f, 0f);
        transform.DOScale(size, 1f);
	}
    
    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 100f); 
        LifeSpan -= Time.deltaTime;
        if (LifeSpan < 2 && LifeSpan>1)
        {
            transform.DOScale(new Vector3(0f, 0f, 0f), 1f);
        }else if (LifeSpan < 1)
        {
            
            Destroy(this.gameObject);
        } 
    }
}
