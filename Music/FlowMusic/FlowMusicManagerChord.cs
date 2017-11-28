using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowMusicManagerChord : MonoBehaviour { 
    public TextAsset sheetMusic;
    public float DeltaTime;
    private float deltaTime;
    private string[] linesInFile;
    private int lineNo = 0;
    private static FlowMusicManagerChord instance = null;

    public static FlowMusicManagerChord Instance
    {
        get
        {
            return instance;
        }
    }
     
    private void Awake()
    {
        deltaTime = 2.9f;
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(sheetMusic != null)
        {
            linesInFile = sheetMusic.text.Split('\n');
        }
        else
        {
            print("No sheet music!");
        }
    }

    // Use this for initialization
    void Start () { 
    }

    private void FixedUpdate()
    {
        deltaTime -= Time.deltaTime;  
        if(deltaTime < 0f)
        {
            if (lineNo < linesInFile.Length)
            {
                
                deltaTime = DeltaTime;
                string[] notesInLine = linesInFile[lineNo].Split(new char[0]);

                foreach (string note in notesInLine)
                {
                    GameObject musicNote = GameObject.Find(note);
                    if (musicNote != null)
                    {
                        GameObject noteObj = (GameObject)Instantiate(musicNote, FlowMusicPlayer.Instance.transform.position + new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f), Quaternion.identity);
                        //noteObj.GetComponent<AudioSource>().enabled = true;
                        //noteObj.GetComponent<SpriteRenderer>().enabled = true;
                        noteObj.GetComponent<FlowMusicChordNote>().enabled = true;  
                    }
                }
            }
            lineNo++;
        }
    }

    void ReadSheetCreateNotes()
    {
        string[] linesInFile = sheetMusic.text.Split('\n');
        int lineNo = 0;
        foreach (string line in linesInFile)
        {
            string[] notesInLine = line.Split(new char[0]);       
            lineNo++;
        }
    }
}
