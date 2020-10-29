using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    public float rotationsPerMinute  = 10.0f;
    
    void Update()
    {
        //Continuously rotate spike ball on spot
        transform.Rotate(0, 0, -6.0f * rotationsPerMinute * Time.deltaTime);
    }
}
