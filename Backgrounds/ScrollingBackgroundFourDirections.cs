using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgroundFourDirections : MonoBehaviour {
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 70;

    private int topRowIndex;
    private int bottomRowIndex;
    private int leftRowIndex;
    private int rightRowIndex;

    public float backgroundSize;
    public float backgroundHeight;

    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);
        topRowIndex = 0; // 0, 1, 2
        bottomRowIndex = 2; //6, 7, 8
        leftRowIndex = 0; // 0, 3, 6
        rightRowIndex = 2; //2, 5, 8 
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cameraTransform.position.x < (layers[leftRowIndex].transform.position.x + viewZone))
            ScrollLeft();

        if (cameraTransform.position.x > (layers[rightRowIndex].transform.position.x - viewZone))
        {
            ScrollRight();
        }

        if (cameraTransform.position.y > (layers[topRowIndex].transform.position.y - viewZone/2f))
        {
            ScrollUp();
        }

        if (cameraTransform.position.y < (layers[bottomRowIndex*3].transform.position.y + viewZone/2f))
        {
            ScrollDown();
        }
    }

    private void ScrollLeft()
    {
        for (int i = 0; i < 3; i++)
        {
            //move right to left - size
            //right position <- leftposition - size
            layers[rightRowIndex + i * 3].position = Vector3.right * (layers[leftRowIndex + i * 3].position.x - backgroundSize) + Vector3.up * (layers[leftRowIndex + i * 3].position.y);
        }
        leftRowIndex = rightRowIndex;
        rightRowIndex--;
        if (rightRowIndex < 0)
            rightRowIndex = 3 - 1;
    }

    private void ScrollRight()
    {
        for (int i = 0; i < 3; i++)
        {
            //move right to left - size
            //right position <- leftposition - size
            layers[leftRowIndex + i * 3].position = Vector3.right * (layers[rightRowIndex + i * 3].position.x + backgroundSize) + Vector3.up * (layers[rightRowIndex + i * 3].position.y);
        }
        rightRowIndex = leftRowIndex;
        leftRowIndex++;
        if (leftRowIndex == 3)
            leftRowIndex = 0;

    }

    private void ScrollUp()
    {
        for (int i = 0; i < 3; i++)
        {
            //move right to left - size
            //right position <- leftposition - size
            layers[bottomRowIndex*3 + i].position = Vector3.up * (layers[topRowIndex*3 + i].position.y + backgroundHeight) + Vector3.right * (layers[topRowIndex*3 + i].position.x);
        }
        topRowIndex = bottomRowIndex;
        bottomRowIndex--;
        if (bottomRowIndex < 0)
            bottomRowIndex = 3 - 1;
    }

    private void ScrollDown()
    {
        for (int i = 0; i < 3; i++)
        {
            //move right to left - size
            //right position <- leftposition - size
            layers[topRowIndex * 3 + i].position = Vector3.up * (layers[bottomRowIndex * 3 + i].position.y - backgroundHeight) + Vector3.right * (layers[bottomRowIndex * 3 + i].position.x);
        }
        bottomRowIndex = topRowIndex;
        topRowIndex++;
        if (topRowIndex == 3)
            topRowIndex = 0;

    }
}
