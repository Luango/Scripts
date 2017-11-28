using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNoteManager : MonoBehaviour {
    public bool isChecked = false;
    public AudioClip keySound;
    private float checkLifeSpan = 0.0f;
    public GameObject note;

    // Use this for initialization
    void Awake () {
    }
	
	// Update is called once per frame
	void Update () {
        checkLifeSpan -= Time.deltaTime;
		if(isChecked == false && checkLifeSpan < 0f)
        {
            GameObject noteObj = Instantiate(note, transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0f), Quaternion.identity);
            noteObj.transform.parent = transform;
            //noteObj.GetComponent<SpriteRenderer>().enabled = true;
            noteObj.GetComponent<FlowMusicNote>().enabled = true;
            noteObj.GetComponent<FlowMusicNote>().keySound = keySound;

            checkLifeSpan = 3.5f;
        }
	}
}
