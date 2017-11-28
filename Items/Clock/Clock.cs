using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Clock : MonoBehaviour {
    public GameObject EducationBullet;
    private Transform MinutesPointer;
    private Transform HoursPointer;

    public List<Transform> candidates = new List<Transform>();

    private Transform HourTop;
    private Transform Center;
    private Transform HourBottom;
    private Vector3 PointingDirection;

    private void Awake()
    {
        if (MinutesPointer == null) MinutesPointer = this.transform.Find("MinutesPointer"); 
        if (HoursPointer == null) HoursPointer = this.transform.Find("HoursPointer");
        if (HourTop == null) HourTop = this.transform.Find("HoursPointer/HourTop");
        if (HourBottom == null) HourBottom = this.transform.Find("HoursPointer/HourBottom");
        if (Center == null) Center = this.transform.Find("Center");
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void RandomTime()
    {
        float RandomMin = Random.Range(-180f, 180f);
        float RandomHour = Random.Range(-180f, 180f);
        /*
        MinutesPointer.DORotate(new Vector3(0f, 0f, RandomMin), Random.Range(0.2f, 0.5f)).OnComplete(()=>
        {
            // Shoot dead line bullet.
            print("Rotate Completed");
            Transform MinTop = transform.Find("MinutesPointer/MinTop");

            if (EducationBullet != null) {
                GameObject bullet = Instantiate(EducationBullet, MinTop.position, Quaternion.Euler(0f, 0f, RandomMin));
            }
        });
        */

        HoursPointer.DORotate(new Vector3(0f, 0f, RandomHour), Random.Range(0.2f, 0.5f)).OnComplete(()=>
        {
            // Shoot dead line bullet.
            Transform HourTop = transform.Find("HoursPointer/HourTop");
            if (EducationBullet != null)
            {
                GameObject bullet = Instantiate(EducationBullet, HourTop.position, Quaternion.Euler(0f, 0f, RandomHour));
            }
        });
    }

    public void RandomHit()
    {
        if (candidates.Count>1) {
            int num = (int)Random.Range(0, candidates.Count);
            PointingDirection = Vector3.up;
            Vector3 TargetPosition = candidates[num].position;
            candidates.Remove(candidates[num]);
            Vector3 TargetDirection = Center.position - TargetPosition;
            float rotateAngle = Vector3.Angle(PointingDirection, TargetDirection) + 180f;
            print("Rotate angle = " + rotateAngle);
            if (TargetPosition.x < 0f)
            {
                rotateAngle = -rotateAngle;
            }
             
            Debug.DrawLine(Center.position, TargetPosition, Color.blue, 31f);

            HoursPointer.DORotate(new Vector3(0f, 0f, rotateAngle), Random.Range(0.2f, 0.5f)).OnComplete(() =>
            {
                Transform HourTop = transform.Find("HoursPointer/HourTop");
                  if (EducationBullet != null)
                  {
                      GameObject bullet = Instantiate(EducationBullet, HourTop.position, Quaternion.Euler(0f, 0f, rotateAngle));
                  }
            });
        }
    }
}
