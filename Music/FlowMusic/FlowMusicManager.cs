using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowMusicManager : MonoBehaviour { 
    public TextAsset sheetMusic;
    public float DeltaTime;
    private float deltaTime;
    private string[] linesInFile;
    public int lineNo = 0;
    private Vector3 prePos;
    private int stepsCount;
    private float stepSize = 0.25f;
    private static FlowMusicManager instance = null;
    private Vector2 FlowDirection;

    public static FlowMusicManager Instance
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

        if(sheetMusic != null)
        {
            linesInFile = sheetMusic.text.Split('\n');
        }
        else
        {
            print("No sheet music!");
        }
    }
     
    void Start () {
        prePos = FlowMusicPlayer.Instance.transform.position + new Vector3(Random.Range(0f, 3f), Random.Range(-30f, -40f), 0f);
        stepsCount = 0;
        FlowDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        FlowDirection.Normalize();
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
                    if (stepsCount < 80)
                    {
                        stepsCount++;
                    }
                    if (musicNote != null)
                    { 
                        Vector3 newPos = prePos + new Vector3(FlowDirection.x * stepSize * stepsCount* Random.Range(1f,1.2f)*2f, FlowDirection.y * stepSize * stepsCount * Random.Range(1f, 1.5f), 0f);
                        GameObject noteObj = (GameObject)Instantiate(musicNote, newPos, Quaternion.identity); 
                        noteObj.GetComponent<FlowMusicNote>().enabled = true;
                        prePos = newPos;
                        stepsCount = 0;

                        var turnPossible = Random.Range(0f, 1f);
                        if (turnPossible > 0.05f)
                        {
                            FlowDirection = new Vector2(FlowDirection.x + Random.Range(-0.5f, 0.5f), FlowDirection.y + Random.Range(-0.5f, 0.1f));
                            FlowDirection.Normalize();
                        }
                        else
                        { 
                            FlowDirection = new Vector2(FlowDirection.x + Random.Range(-0.6f, 0.6f), FlowDirection.y + Random.Range(-0.8f, 0.1f));
                            FlowDirection.Normalize();
                        }
                    }
                }
            }
            lineNo++;
        }
    }

    void ReadSheetCreateNotes()
    {
        string[] linesInFile = sheetMusic.text.Split('\n');
        int i = 0;
        foreach (string line in linesInFile)
        {
            string[] notesInLine = line.Split(new char[0]);       
            i++;
        }
    }
}
