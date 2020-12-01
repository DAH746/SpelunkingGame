using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    Animator animator;

    //todo create ignore collisions

    [SerializeField]
    Transform player;

/*    [SerializeField] //not needed right now as is radius base chase
    Transform castPoint;*/

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    [SerializeField]
    bool isTheSpriteFacingLeft;

    bool hasBatBeenTriggered = false;

    //Animation states

    const string BAT_IDLE = "Bat_idle_Animation";
    const string BAT_FLY = "Bat_Flying_Animation";
    const string BAT_TAKE_DAMAGE = "Bat_Gets_Hit_Animation";
    

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);


        if (distToPlayer < agroRange)
        {
            animator.SetBool("activateFlight", true);
            hasBatBeenTriggered = true;
            //ChangeAnimationState(BAT_FLY);
            //code to chase player
            
        }
        if (hasBatBeenTriggered)
        {
            ChasePlayer();
        }

        //todo bat will continue to chase you till you kill it, or character dies
        
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            if(transform.position.y < player.position.y)
            {
                rb2d.velocity = new Vector2(moveSpeed, moveSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(moveSpeed, -moveSpeed);//0); //Y is 0 as not moving vertically atm
            }
            

            transform.localScale = new Vector2(-1, 1); //enemy direction facing

            isTheSpriteFacingLeft = false;
        }
        else if (transform.position.x >= player.position.x)//if enemy is to right of player, enemy will move left
        {
            if (transform.position.y < player.position.y)
            {
                rb2d.velocity = new Vector2(-moveSpeed, moveSpeed);
            }
            else
            {
                rb2d.velocity = new Vector2(-moveSpeed, -moveSpeed);
            }
                //rb2d.velocity = new Vector2(-moveSpeed, -moveSpeed);//0);

            transform.localScale = new Vector2(1, 1); //enemy direction facing

            isTheSpriteFacingLeft = true;
        }

    }

    void StopChasingPlayer()
    {
        rb2d.velocity = Vector2.zero; //same as "new Vector2(0,0);"
    }
}
