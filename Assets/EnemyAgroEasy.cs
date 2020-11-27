using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroEasy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        //print("Distance to player: " + distToPlayer); use to figure out what kind of distance you want agro from enemy

        if(distToPlayer < agroRange){
            //code to chase player
            ChasePlayer();
        }
        else
        {
            //stop chasing player
            StopChasingPlayer();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            rb2d.velocity = new Vector2(moveSpeed, 0); //Y is 0 as not moving vertically atm

            transform.localScale = new Vector2(-1, 1); //enemy direction facing
        }
        else if (transform.position.x >= player.position.x)//if enemy is to right of player, enemy will move left
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);

            transform.localScale = new Vector2(1, 1); //enemy direction facing
        }

    }

    void StopChasingPlayer()
    {
        rb2d.velocity = Vector2.zero; //same as "new Vector2(0,0);"
    }



}
