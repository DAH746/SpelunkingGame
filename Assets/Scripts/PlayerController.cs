using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{
    //Player Inventory
        //Purpose of inven is to allow for potential future feature such as: show the gems collected for the level so far in an opaque fashion as a HUD at the top of the screen
    public List<string> inventory; 

    //Statistics for player
    public float maxVelocity = 15f;
    public float jumpThrust = 10f;
    //public float jumpIfHitEnemy = 10f;
    public float speedMultiplier = 4f;
    public int health = 0;
    public int maxHealth = 20;

    //Player state handling
    public bool isGrounded = false;
    public bool isGrabbingWall = false;
    public bool facingRight = true;

    private float sqrMaxVelocity;
    private Rigidbody2D rigidBody;

    public GameObject respawnPoint = null;

    public HealthBar healthBar;

    //animation

    Animator animator;
    const string CHARACTER_IDLE = "Main_Character_Idle_Animation";
    const string CHARACTER_RUN = "Main_Character_Running_Animation";
    //bool jump = false;

    void Start() {
        //initialise player inventory
        inventory = new List<string>();

        //Find rigid body and setup player health - deal no damage to setup the player UI
        rigidBody = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damage(0);

        animator = GetComponent<Animator>();
    }

    void Awake() {
        //Calculate sqrt velocity (quicker when compared) for max velocity
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }


    void Update()
    {
        if(Input.GetAxis("Horizontal")!=0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButton("Jump"))
        {
            animator.SetTrigger("jump");
/*            jump = true;
            animator.SetBool("isJumping", false);*/

        }

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int amountOfHits = GetComponent<BoxCollider2D>().Raycast(-Vector2.up, hits, GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f);
        Debug.DrawLine(transform.position, transform.position + (Vector3)(-Vector2.up * (GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f)));
        if (amountOfHits > 0)
        {
            //Object is below us - we're grounded
            isGrounded = true;
            isGrabbingWall = false;
        }
        else
        {
            isGrounded = false;
            //Not grounded check wall jump
            RaycastHit2D facing = Physics2D.Raycast(transform.position, (facingRight ? 1 : -1) * Vector2.right, GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f);
            if (facing.collider != null && facing.collider.gameObject.tag == "Ground") {
                isGrabbingWall = true;
            }
        }
    }
/*
    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }*/

    void FixedUpdate()
    {
        //Handle horizontal and vertical movement
        float horizontal = (Input.GetAxis("Horizontal") * speedMultiplier);
        if (Input.GetButton("Jump") && isGrounded)
        {
            rigidBody.AddForce(transform.up * jumpThrust, ForceMode2D.Impulse);
            isGrounded = false;
        }
        else if (Input.GetButton("Jump") && isGrabbingWall)
        {
            horizontal = facingRight ? -5f * speedMultiplier : 5f * speedMultiplier;
            rigidBody.AddForce(transform.up * jumpThrust, ForceMode2D.Impulse);
            isGrabbingWall = false;
        }

        rigidBody.velocity = new Vector2(horizontal, rigidBody.velocity.y);
        if (horizontal > 0 && !facingRight)
        {
            reverseDirection();
        }
        else if (horizontal < 0 && facingRight)
        {
            reverseDirection();
        }

        //Limit players velocity
        if (rigidBody.velocity.sqrMagnitude > sqrMaxVelocity) { 
            rigidBody.velocity = rigidBody.velocity.normalized * maxVelocity;
        }
    }

    void reverseDirection()
    {
        //Rotate the player to face opposite direction
        facingRight = !facingRight;

        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    public void damage(int d)
    {
        //Handle player recieving damage - update heath UI according to damage recieved.
        health -= d;
        healthBar.SetHealth(health);
        if (d>0) {
            UnityEngine.Debug.Log("Player recieved " + d + " damage.");
        }
        if (health <= 0) {
            UnityEngine.Debug.Log("Player Dead!");
            health = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            if (respawnPoint!=null) {
                transform.position = respawnPoint.transform.position;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
        //Potential issue as traps are also 2d colliders, may have to do "else {do nothing}" so nothing happens here but the enemy script manages it?
        //Potential issue might be resolved: Gem pickups are labelled as "OnTrigger", whereas traps are "OnCollision"
    {
        if (collision.CompareTag("Collectable")){

            string itemType = collision.gameObject.GetComponent<CollectableScript>().itemType;
            int itemValue=0;

            if(itemType == "redGem")
            {
                itemValue = 1000;
            }else if(itemType == "greenGem")
            {
                itemValue = 3000;
            }
            else if(itemType == "pinkGem")
            {
                itemValue = 5000;
            }
            else if(itemType == "blueGem")
            {
                itemValue = 7500;
            }
            else if(itemType == "bigPinkGem")
            {
                itemValue = 10000;
            }
            else
            {
                //Should not get here
                itemValue = -1;
            }

            CashScript.cashValue += itemValue;

            inventory.Add(itemType);

            print("Player collected a " + itemType);
            Destroy(collision.gameObject);
        }
        
    }





}