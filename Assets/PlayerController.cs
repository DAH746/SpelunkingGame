using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpThrust = 10f;
    public float speedMultiplier = 4f;
    private Rigidbody2D rigidBody;

    public bool isGrounded = false;
    public bool isGrabbingWall = false;
    public bool facingRight = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
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

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground") {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
            
            if (hit.collider!=null)
            {
                if (hit.collider.gameObject == c.gameObject)
                {
                    isGrounded = true;
                    isGrabbingWall = false;
                }
                else
                {
                    isGrabbingWall = true;
                    isGrounded = false;
                }
            }
            else
            {
                isGrabbingWall = true;
                isGrounded = false;
            }
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "Ground")
        {
            isGrounded = false;
            isGrabbingWall = false;
        }
    }

    void reverseDirection()
    {
        facingRight = !facingRight;

        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
