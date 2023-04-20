using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float JumpHeight = 5f;
    Vector2 MoveInput;
    Vector2 PlayerVelocity;
    Vector2 PlayerJumpHeight;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        Run();
        FlipSprite();
    }
    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        PlayerJumpHeight = new Vector2(MoveInput.x * RunSpeed, JumpHeight);
        myRigidbody.velocity = PlayerJumpHeight;
        if(myRigidbody.velocity.y > PlayerJumpHeight.y)
        {
            myAnimator.SetBool("IsJumping", true);
        }
        else 
        {
            myAnimator.SetBool("IsJumping", false);
        }
        
    }
    void Run()
    {
        PlayerVelocity = new Vector2(MoveInput.x * RunSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = PlayerVelocity;
    }
    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            myAnimator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("IsRunning", false);
        }
    }
}

