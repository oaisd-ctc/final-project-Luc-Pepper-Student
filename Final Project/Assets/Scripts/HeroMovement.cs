using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float JumpHeight = 5f;
    float FallVelocity;
    Vector2 MoveInput;
    Vector2 PlayerVelocity;
    Vector2 PlayerJumpHeight;
    Rigidbody2D myRigidbody;
    PolygonCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<PolygonCollider2D>();
        FallVelocity = myRigidbody.velocity.y;
    }
    void Update()
    {
        Run();
        FlipSprite();
        Falling();
        Rising();
    }
    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        PlayerJumpHeight = new Vector2(MoveInput.x * RunSpeed, JumpHeight);
        myRigidbody.velocity = PlayerJumpHeight;
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

    void Falling()
    {
        if (transform.position.y < FallVelocity && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsFalling", true);
            myAnimator.SetBool("IsRising", false);
        }
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsFalling", false);
            myAnimator.SetBool("IsRising", false);
        }
        FallVelocity = transform.position.y;
    }
    void Rising()
    {
        if (FallVelocity <= transform.position.y && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsRising", true);
        }
        FallVelocity = transform.position.y;
    }
}

