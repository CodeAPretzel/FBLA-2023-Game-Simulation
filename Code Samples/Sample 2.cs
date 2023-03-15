//Below is a Code Sample for the Movement and Controls of the Player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    bool WeCanMove{
        set{
            isMoving = value;
            animator.SetBool("isMoving", isMoving);
        }
    }

    public float moveSpeed = 600f;
    public float maxSpeed = 8f;

    //Each frame of physics, what precentage of the speed should be shaved off the velocity out of 1 (100%)
    public float idleFriction = 0.8f;

    public GameObject swordHitbox;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    Collider2D swordCollider;
    Vector2 moveInput = Vector2.zero;

    bool lockingMovement = false;
    bool isMoving = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        
        if (!lockingMovement && moveInput != Vector2.zero){
            //Move animation and add velocity

            //Accelerate the player while run direction is pressed
            //BUT don't allow player to run faster than the max speed in any direction
            
            //Disable Knockback:
            rb.velocity = Vector2.ClampMagnitude((rb.velocity / 2) + (moveInput * moveSpeed * Time.fixedDeltaTime), maxSpeed);
            
            //Enable Knockback:
            //rb.AddForce(1.5f*(moveInput * moveSpeed * Time.deltaTime));

            if(rb.velocity.magnitude > maxSpeed){
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
            }

            //Control whether looking left or right
            if (moveInput.x > 0 && rb.simulated){
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("IsFacingRight", true);
            } else if (moveInput.x < 0 && rb.simulated){
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }

            WeCanMove = true;

        } else {
            //No movement so Interpolate velocity towards 0
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
        
            WeCanMove = false;
        }
    }

    //Get Input values for player movement
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnFire(){
        SoundManagerScript.PlaySound("swordSwing");
        animator.SetTrigger("swordAttack");
    }

    public void LockMovement(){
        lockingMovement = true;
    }

    public void UnLockMovement(){
        lockingMovement = false;
    }
}
