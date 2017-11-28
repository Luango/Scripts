using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;


public class PlanetMusicManager : MonoBehaviour {
	public TextAsset sheetMusic; 
	public GameObject musicNoteGameObject;
	public List<GameObject> musicNotesList;
	public GameObject theGirl;

	public Text musicNoteText;

	// Use this for initialization
	void Start () {
		readSheetCreateNotes ();

		GameObject[] allBars = GameObject.FindGameObjectsWithTag ("PlanetMusicBar");
		foreach (GameObject theMusicBar in allBars) {
			theMusicBar.GetComponent<AudioSource> ().volume = 0.1f;
		}
	}

	void Update(){
		float percentage = (1032f-musicNotesList.Count) / 1032f*100;
		musicNoteText.text = "Music complete: " +  percentage.ToString("F1") + "%";
		if (theGirl.activeSelf == false && percentage > 95f) {
			theGirl.SetActive (true);
		}
	}

	void readSheetCreateNotes() { 
		string[] linesInFile = sheetMusic.text.Split('\n');
		int lineNo = 0;
		foreach (string line in linesInFile)
		{
			Vector3 position = calculateNotePosition(lineNo);

			string[] notesInLine = line.Split (new char[0]);
			foreach (string note in notesInLine) {
				createMusicNote (note, position);
			}
			lineNo++;
		}
	}

	void createMusicNote(string noteName, Vector3 position){
		// find the gameobj with the note name
		// find note first;
		GameObject musicNote = GameObject.Find(noteName);
		if (musicNote != null&&musicNote.tag == "MusicPlanet") {
			GameObject musicCylinder = musicNote.transform.Find ("musicCylinder").gameObject;
			// create a note on that position
			if (musicCylinder != null) {
				Vector3 musicNotePosition = musicCylinder.transform.position + position;
				GameObject newNote = (GameObject)Instantiate (musicNoteGameObject, musicNotePosition, Quaternion.identity);
				newNote.transform.parent = musicCylinder.transform;

				// Add this newNote to note list
				musicNotesList.Add(newNote);
			}
		}
	}

	Vector3 calculateNotePosition(int n){
		Vector3 position;
		float radius = 125f;
		float x = 0f;
		float y = 0f;
		float z = 0f;
		//float deltaTheta = 1.68f / 360f;
		float deltaTheta = 3.0f / 360f;
		float startPosition;
		//startPosition = 1090;
		startPosition = 2100;
		y = radius * Mathf.Sin (startPosition / 360f - n * deltaTheta);
		z = radius * Mathf.Cos (startPosition / 360f - n * deltaTheta);
		position = new Vector3 (x, y, z);

		return position;
	}
}
