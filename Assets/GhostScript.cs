using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    //Animator animator1;

    //todo create ignore collisions

    [SerializeField]
    Transform player;

    /*    [SerializeField] //not needed right now as is radius base chase
        Transform castPoint;*/

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d1;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    bool isTheSpriteFacingLeft;

    bool hasGhostBeenTriggered = false;

    //Animation states



    void Start()
    {
        //animator1 = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d1 = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(13, 0, true); //ignore collision between "Enemy layer - layer 11" and the "default layer - layer 0 "
        Physics2D.IgnoreLayerCollision(13, 12, true);
        Physics2D.IgnoreLayerCollision(13, 31, true);
        //Physics2D.IgnoreLayerCollision(11, 12, true);
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);


        if (distToPlayer < agroRange)
        {
           
            hasGhostBeenTriggered = true;
            //ChangeAnimationState(BAT_FLY);
            //code to chase player

        }
        if (hasGhostBeenTriggered)
        {
            ChasePlayer();
        }

        //todo bat will continue to chase you till you kill it, or character dies

    }

    void ChasePlayer()
    {

        if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            if (transform.position.y < player.position.y)
            {
                rb2d1.velocity = new Vector2(moveSpeed, moveSpeed);
            }
            else
            {
                rb2d1.velocity = new Vector2(moveSpeed, -moveSpeed);//0); //Y is 0 as not moving vertically atm
            }

            if (spriteRenderer.flipX == true) //if the sprite is flipped 
            {
                transform.localScale = new Vector2(1, 1); //enemy direction facing
            }
            else
            {
                transform.localScale = new Vector2(-1, 1);
            }

            isTheSpriteFacingLeft = false; //for raycast
        }
        else if (transform.position.x >= player.position.x)//if enemy is to right of player, enemy will move left
        {
            if (transform.position.y < player.position.y)
            {
                rb2d1.velocity = new Vector2(-moveSpeed, moveSpeed);
            }
            else
            {
                rb2d1.velocity = new Vector2(-moveSpeed, -moveSpeed);
            }

            if (spriteRenderer.flipX == true) //if the sprite is flipped 
            {
                transform.localScale = new Vector2(-1, 1); //enemy direction facing
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }

            isTheSpriteFacingLeft = true; //for raycast
        }



/*       
 *       old code:
 *       
 *       if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            if (transform.position.y < player.position.y)
            {
                rb2d1.velocity = new Vector2(moveSpeed, moveSpeed);
            }
            else
            {
                rb2d1.velocity = new Vector2(moveSpeed, -moveSpeed);//0); //Y is 0 as not moving vertically atm
            }


            transform.localScale = new Vector2(-1, 1); //enemy direction facing

            isTheSpriteFacingLeft = false;
        }
        else if (transform.position.x >= player.position.x)//if enemy is to right of player, enemy will move left
        {
            if (transform.position.y < player.position.y)
            {
                rb2d1.velocity = new Vector2(-moveSpeed, moveSpeed);
            }
            else
            {
                rb2d1.velocity = new Vector2(-moveSpeed, -moveSpeed);
            }
            //rb2d.velocity = new Vector2(-moveSpeed, -moveSpeed);//0);

            transform.localScale = new Vector2(1, 1); //enemy direction facing

            isTheSpriteFacingLeft = true;
        }
*/
    }

    void StopChasingPlayer()
    {
        rb2d1.velocity = Vector2.zero; //same as "new Vector2(0,0);"
    }
}
