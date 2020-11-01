﻿using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Statistics for player
    public float maxVelocity = 15f;
    public float jumpThrust = 10f;
    public float speedMultiplier = 4f;
    public int health = 0;
    public int maxHealth = 20;
    public GameObject healthIndicator = null;

    //Player state handling
    public bool isGrounded = false;
    public bool isGrabbingWall = false;
    public bool facingRight = true;

    private float sqrMaxVelocity;
    private Rigidbody2D rigidBody;

    void Start() {
        //Find rigid body and setup player health - deal no damage to setup the player UI
        rigidBody = GetComponent<Rigidbody2D>();
        health = maxHealth;
        damage(0);
    }

    void Awake() {
        //Calculate sqrt velocity (quicker when compared) for max velocity
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }

    void Update()
    {
        RaycastHit2D below = Physics2D.Raycast(transform.position, -Vector2.up, GetComponent<BoxCollider2D>().bounds.extents.y + 0.1f);
        if (below.collider != null)
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
        healthIndicator.GetComponent<Text>().text = "Health: " + health;
        if (d>0) {
            UnityEngine.Debug.Log("Player recieved " + d + " damage.");
        }
        if (health <= 0) {
            UnityEngine.Debug.Log("Player Dead!");
            healthIndicator.GetComponent<Text>().text = "Health: Dead!";
        }
    }
}
