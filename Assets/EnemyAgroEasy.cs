using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroEasy : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    int health = 1;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    bool isTheSpriteFacingLeft;

    [SerializeField]
    bool DoYouWantRadiusBasedChasing;

    bool isAgro = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (!DoYouWantRadiusBasedChasing)
        {
            //This portion of code within this if-statement will initiate chase when the enemy see's the player
            //The else branch is for radius based chasing

            if (CanSeePlayer(agroRange))
            {
                
                isAgro = true;
                ChasePlayer();
            }
            else
            {

                if (isAgro && distToPlayer < agroRange)
                {
                    //InvokeRepeating("StopChasingPlayer", 3, 3); 
                    ChasePlayer();
                }
                else
                {
                    StopChasingPlayer();
                    isAgro = false;
                }
            }
        }
        else
        {

            //for debugging use:
            //print("Distance to player: " + distToPlayer); use to figure out what kind of distance you want agro from enemy

            if (distToPlayer < agroRange)
            {
                //code to chase player
                ChasePlayer();
            }
            else
            {
                //stop chasing player
                StopChasingPlayer();
            }
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;
        Vector2 endPos;

        if (isTheSpriteFacingLeft)
        {
            endPos = castPoint.position + Vector3.left * castDist;
        }else
        { 
            endPos = castPoint.position + Vector3.right * castDist;
        }

        //Old code:
        //Vector2 endPos = castPoint.position + Vector3.left * castDist; // "Vector3.<left/right>" denotes which location the character is looking

        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
            //part1 :for debugging use (uncomment subsequent comment too):
            //Debug.DrawLine(castPoint.position, endPos, Color.yellow);
        }
/*        //part2: For debugging use
 *        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.red);
        }*/

        return val;
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            rb2d.velocity = new Vector2(moveSpeed, 0); //Y is 0 as not moving vertically atm

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
            rb2d.velocity = new Vector2(-moveSpeed, 0);

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

    }


/*    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)//if enemy is to left of player, enemy will move right
        {
            rb2d.velocity = new Vector2(moveSpeed, 0); //Y is 0 as not moving vertically atm

            transform.localScale = new Vector2(-1, 1); //enemy direction facing

            isTheSpriteFacingLeft = false;
        }
        else if (transform.position.x >= player.position.x)//if enemy is to right of player, enemy will move left
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);

            transform.localScale = new Vector2(1, 1); //enemy direction facing

            isTheSpriteFacingLeft = true;
        }

    }*/

    void StopChasingPlayer()
    {
        isAgro = false;
        rb2d.velocity = Vector2.zero; //same as "new Vector2(0,0);"
    }

    void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (otherObject.gameObject.tag == "Player")
        {
            if (otherObject.GetComponent<Rigidbody2D>().velocity.y <= 0f)
            {
                otherObject.GetComponent<Rigidbody2D>().velocity = new Vector2(rb2d.velocity.x, (otherObject.GetComponent<PlayerController>().jumpThrust) * 0.4f);
                //Destroy(this.gameObject);
                health = health - 1;

                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }

            }
        }
    }

}
