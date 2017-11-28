using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;

// Create music notes on the musical cylinder from sheet music
public class MusicManager : MonoBehaviour {
	public TextAsset sheetMusic;
	public GameObject musicNoteGameObject;
    public float deltaTheta;
    public float startPosition;
    public float rotationSpeed;
    public float buttonRotation;
    static public bool Pause = false;

    private bool Completed = false;
    private GameObject FirstNote;
    private GameObject LastNote;
    public List<int> Answers;
    public List<int> CurrAnswers;

    public List<GameObject> Groups;

    private static MusicManager instance = null;
    
    public static MusicManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
		ReadSheetCreateNotes ();
	}
    private void Update()
    {
        if (Completed==false)
        {
            if (CheckAnswer())
            {
                Completed = true;
            }
        }
    }

    private bool CheckAnswer ()
    {
        for (int i =0; i < Answers.Count; i++)
        {
            CurrAnswers[i] = (int)Groups[i].GetComponent<MusicGroup>().orientationOffset; 
            if ((Answers[i] - CurrAnswers[i]) % 360 != 0)
            { 
                return false;
            }
        }
        print("Solved");
        return true;
    }

	void ReadSheetCreateNotes() { 
		string[] linesInFile = sheetMusic.text.Split('\n');
		int lineNo = 0;
		foreach (string line in linesInFile)
		{
			string[] notesInLine = line.Split (new char[0]);

			foreach (string note in notesInLine) {
				CreateMusicNote (note, lineNo);
			}
			lineNo++;
		}
	}
    
	void CreateMusicNote(string noteName, int lineNo){
		GameObject musicNote = GameObject.Find(noteName);
        
        if (musicNote != null)
        {
            GameObject musicCylinder = musicNote.transform.Find ("musicCylinder").gameObject;
            float radius = musicCylinder.GetComponent<MusicBoxMain>().radius;
            float offset = musicCylinder.transform.parent.gameObject.transform.parent.gameObject.GetComponent<MusicGroup>().orientationOffset;
            
            Vector3 position = CalculateNotePosition(lineNo, radius, offset);
            if (musicCylinder != null) {
				Vector3 musicNotePosition = musicCylinder.transform.position + position;
				GameObject newNote = (GameObject)Instantiate (musicNoteGameObject, musicNotePosition, Quaternion.identity);
				newNote.transform.parent = musicCylinder.transform;
			}
		}
    }
    
	Vector3 CalculateNotePosition(int n, float radius, float offset){
		Vector3 position;
		float x = 0f;
		float y = 0f;
		float z = 0f;

		y = radius * Mathf.Cos (Mathf.PI * (startPosition + offset) / 180f - n * deltaTheta);
		x = radius * Mathf.Sin (Mathf.PI * (startPosition + offset) / 180f - n * deltaTheta);
		position = new Vector3 (x, y, z);

		return position;
	}
}
