using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlowMusicChordNote : MonoBehaviour {
    private Vector3 iniPosition;

    private float lifeSpan = 0.1f;
    private string noteName;
    private float stepSize;
    private float threshold = 10f;
    
    public AudioClip keySound;
    private bool hasPlayed = false;
    private bool isShrinking = false;
    private int FileID;
    private int SoundID;

    private void Awake()
    {
        FileID = AndroidNativeAudio.load("Piano/" + keySound.name + ".wav");
        //FileID = AndroidNativeAudio.load("Piano/" + keySound.name + ".mp3");

    }
    // Use this for initialization
    void Start () {
        
        this.transform.localScale = new Vector3(0f, 0f, 1f);
    }
	 

    void OnApplicationQuit()
    {
        // Clean up when done
        AndroidNativeAudio.unload(FileID);
        AndroidNativeAudio.releasePool();
    }

    private void Update()
    {
        if (!isShrinking)
        {
            this.transform.DOScale(new Vector3(0.51f, 0.51f, 0.51f), lifeSpan);
        }

        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0f && hasPlayed == false)
        {
            hasPlayed = true;
            SoundID = AndroidNativeAudio.play(FileID); 
        }
        else if (lifeSpan <= 0f && lifeSpan > -1f)
        {
            if (!isShrinking)
            {
                isShrinking = true;
                this.transform.DOScale(new Vector3(0.0f, 0.0f, 1f), 0.3f);
            }
        }
        else if(lifeSpan <= -1f)
        {
            Destroy(this.gameObject);
        }
    }

    private float CalculateDistanceFromPlayer()
    {
        float dis = Vector3.Distance(this.gameObject.transform.position, FlowMusicPlayer.Instance.transform.position);
        return dis;
    }
}
