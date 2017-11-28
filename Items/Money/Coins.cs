using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {
    public void PickedUp()
    {
        // Create the Animation
        Destroy(this.gameObject);
    }
}
