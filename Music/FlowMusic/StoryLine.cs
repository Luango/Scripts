using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryLine : MonoBehaviour {
    private IEnumerator TextCoroutine; 
	
	// Update is called once per frame
	void Start () {
        TextCoroutine = ShowStory(transform.GetComponent<TextMeshPro>());
        StartCoroutine(TextCoroutine);
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
        yield return new WaitForSeconds(15f);
        Destroy(text.gameObject);
    }
}
