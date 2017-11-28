using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PeopleFear : MonoBehaviour {
    private bool trembled = false;
    public void Trembling()
    {
        if (!trembled)
        {
            trembled = true;
            //this.transform.DOShakeScale(3f, 0.1f, 3, 30f, true).OnComplete( () => trembled = false );
        }
    }
}
