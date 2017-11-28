using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {
    public List<GameObject> TutorialNotes;
    public GameObject MelodyManager;
    public GameObject ChordManager;
    private int i = 0;

	// Update is called once per frame
	void Update () {
        if (i < TutorialNotes.Count)
        {
            bool isChecked = TutorialNotes[i].GetComponent<TutorialNoteManager>().isChecked;
            if (isChecked)
            {
                i++;
                if (i < TutorialNotes.Count)
                {
                    TutorialNotes[i].SetActive(true);
                }
                else
                {
                    MelodyManager.SetActive(true);
                    ChordManager.SetActive(true);
                }
            }
        }
	}
}
