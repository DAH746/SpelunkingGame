using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThatMoves : MonoBehaviour
{
    [SerializeField]
    int collisionDamage = 1;

    [SerializeField]
    int enemyHealth = 1;

    [SerializeField]
    bool deathIfHitTrap;

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().damage(collisionDamage);
        }else if (collision.gameObject.tag == "Trap" && deathIfHitTrap == true)
        {
            Destroy(this.gameObject);
        }
    }
}
