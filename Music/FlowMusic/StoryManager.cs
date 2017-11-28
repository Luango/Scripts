using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour {
    private List<Transform> StoryNotes = new List<Transform>();
    private int Index = 0;
    private float deltaTime = 0.3f;
    private IEnumerator TextCoroutine;

    private static StoryManager instance = null;
    public static StoryManager Instance
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

        foreach (Transform child in transform)
        {
            print(child);
            StoryNotes.Add(child);
            child.gameObject.SetActive(false);
        }
    }
    
	// Update is called once per frame
	void Update () {
        deltaTime -= Time.deltaTime;
        if (FlowMusicManager.Instance != null && deltaTime<0f)
        {
            if (Index < StoryNotes.Count)
            {
                StoryNotes[Index].gameObject.SetActive(true);
                StoryNotes[Index].position = FlowMusicPlayer.Instance.StoryTransform.position;
                TextCoroutine = ShowStory(StoryNotes[Index].GetComponent<TextMeshPro>());
                StartCoroutine(TextCoroutine);
                Index++;
            }
            deltaTime = 5f;
        }
	}

    IEnumerator ShowStory(TextMeshPro text)
    {
        int totalVisibleCharacters = text.textInfo.characterCount;
        int counter = 0;
        bool isCompleted = false;
        while (!isCompleted)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            text.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                isCompleted = true;
            }

            counter++;

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(5f);
        Destroy(text.gameObject);
    }
}
