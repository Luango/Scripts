using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Keyboard : MonoBehaviour {
    private bool canPress = true;
    private bool completed = true;
    private float defaultHeight;

    private void Awake()
    {
        defaultHeight = transform.position.y;
    }

    public void ButtonPressed()
    {
        if (canPress == true && completed == true)
        {
            completed = false;
            this.transform.DOMoveY(defaultHeight - 0.3054565f, 0.1f).OnComplete( () => {
                canPress = false;
                completed = true;
                this.transform.DOMoveY(defaultHeight, 0.1f);
             });
        }
    }

    public void ButtonReleased()
    {
        if (canPress == false)
        {
            completed = false;
            this.transform.DOMoveY(defaultHeight, 0.1f).OnComplete( () => {
                canPress = true;
                completed = true;
            });
        }
    }
}
