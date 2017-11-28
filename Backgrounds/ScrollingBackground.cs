using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    private Transform cameraTransform;
    public Transform[] horizontal_layers;
    private float viewZone = 25;

    private int leftIndex;
    private int rightIndex;
    public float backgroundSize;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        horizontal_layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            horizontal_layers[i] = transform.GetChild(i);
        leftIndex = 0;
        rightIndex = horizontal_layers.Length - 1;
    }

    private void Update()
    {
        if (cameraTransform.position.x < (horizontal_layers[leftIndex].transform.position.x + viewZone))
            ScrollLeft();

        if (cameraTransform.position.x > (horizontal_layers[rightIndex].transform.position.x - viewZone))
            ScrollRight();
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        horizontal_layers[rightIndex].position = Vector3.right * (horizontal_layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = horizontal_layers.Length - 1;
    }

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        horizontal_layers[leftIndex].position = Vector3.right * (horizontal_layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == horizontal_layers.Length)
            leftIndex = 0;
    }
}
