using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatTrailMove : MonoBehaviour {
    public float waveTime = 0f;
	// Use this for initialization
	void Start () {
        transform.DOLocalMoveX(0.07f, waveTime+Random.Range(0f, 0.1f)).SetLoops(-1, LoopType.Yoyo);
	} 
}
