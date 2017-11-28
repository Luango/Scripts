using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatBottomLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 tarScale = transform.localScale + new Vector3(0.08f, 0.08f, 0.2f);
        transform.DOScale(tarScale, 3f).SetLoops(-1,LoopType.Yoyo);
	} 
}
