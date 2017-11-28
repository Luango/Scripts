using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCamera : MonoBehaviour {
    public Transform target;
    public float smoothSpeed = 10f;
    public Vector3 offset;
    // Aming target or boat.
    public bool aimingTarget = true;

	// Use this for initialization
	void Start () {
		
	}

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
