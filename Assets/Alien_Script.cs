using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alien_Script : MonoBehaviour
{ 
    public Rigidbody2D myRigidbody;
    [SerializeField] private float jumpForce = 19f;
    private bool isGrounded = false;
    [SerializeField] private Transform playerPosition;
    // A reference to a Transform object representing the position from which to check if the player is grounded
    [SerializeField] private float groundCheckRadius = 0.4f;
    // Determines the radius of the circle for ground checking
    [SerializeField] private LayerMask groundLayer;
    // A reference to the ground layer 

    private bool isJumping = false;
    [SerializeField] private float jumpTime = 1f;
    private float jumpTimer = 0f;

    [SerializeField] private Transform Alien;
    [SerializeField] private float shrinkY = 1.7f;
    [SerializeField] private float shrinkX = 1.7f;


    void Start()
    {
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(playerPosition.position, groundCheckRadius, groundLayer);
        // Checks if the player is grounded by using Physics2D.OverlapCircle method.
        // It casts a circle from playerPosition with a radius of groundCheckRadius
        // and checks if it collides with any object on the ground layer.

        // JUMP

        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
            // If the alien is grounded and the up key pressed alien jumps
        {
            isJumping = true;
            myRigidbody.velocity = Vector2.up * jumpForce;    
        }

        if (isJumping && Input.GetKey(KeyCode.UpArrow))
        {
            if (jumpTimer < jumpTime)
            {
                myRigidbody.velocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
            jumpTimer = 0f;
        }

        // SHRINK
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Alien.localScale = new Vector2(shrinkX, shrinkY);

            if (isJumping)
            {
                Alien.localScale = new Vector2(shrinkX, shrinkY);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Alien.localScale = new Vector2(2.3f, 2.3f);

        }
    }
    
}
