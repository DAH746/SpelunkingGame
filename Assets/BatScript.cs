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
    int health = 1;

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
        Physics2D.IgnoreLayerCollision(11, 0, true); //ignore collision between "Enemy layer - layer 11" and the "default layer - layer 0 "
        Physics2D.IgnoreLayerCollision(11, 12, false);
        Physics2D.IgnoreLayerCollision(11, 31, true);
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

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if(otherObject.gameObject.tag == "Player")
        {
            if(otherObject.GetComponent<Rigidbody2D>().velocity.y <= 0f)
            {
                otherObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rb2d.velocity.x, (otherObject.GetComponent<PlayerController>().jumpThrust)*0.4f);
                //Destroy(this.gameObject);
                health = health-1;

                if(health <= 0)
                {
                    Destroy(this.gameObject);
                }

            }
        }
    }

/*    void OnCollisionStay(Collision collide)
    {
        // Output the name of the GameObject you collide with
        Debug.Log("I hit the GameObject : " + collide.gameObject.name);
    }*/


}
