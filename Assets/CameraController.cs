using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject follow = null;

    void Update() {
        //Follow specified game object - would generally be the player assigned 
        //as the follow object in the editor, but could change depending on death handling
        this.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, -5);
    }
}
