using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int collisionDamage = 1; 
    void OnCollisionEnter2D(Collision2D c)
    {
        //General enemy damage which can be used for all objects which deal damage to the player
        if (c.gameObject.tag=="Player") {
            c.gameObject.GetComponent<PlayerController>().damage(collisionDamage);
        }
    }
}
