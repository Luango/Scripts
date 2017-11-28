using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SoundBar : MonoBehaviour {
	public AudioSource note;
    private Transform WhiteKey;
    private Vector3 scale;

    private void Awake()
    {
        WhiteKey = transform.GetChild(0);
        scale = WhiteKey.transform.localScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "MusicNote")
        {
            if (this.tag == "PlanetMusicBar"&&other.gameObject.GetComponent<MusicNote>().isActive==true) {
			}
			if (other.transform.gameObject.GetComponent<MusicNote> ().isActive)
            {
                WhiteKey.DOScale(scale * 1.3f, 0.3f).OnComplete(() => WhiteKey.DOScale(scale, 0.3f));
                note.Play ();
			}
		}
	}
}
