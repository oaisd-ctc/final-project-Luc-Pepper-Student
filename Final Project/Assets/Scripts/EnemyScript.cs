using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float EnemyWalkSpeed = 5f;
    [SerializeField] float EnemyRunSpeed = 10f;
    [SerializeField] float EnemyMoveSpeed = 5f;

    bool CanSeePlayer;
    Rigidbody2D EnemyRigidBody;
    BoxCollider2D EnemyBoxCollider;
    CapsuleCollider2D EnemyCapsuleCollider;
    Animator EnemyAnimator;
    void Start()
    {
        EnemyRigidBody = GetComponent<Rigidbody2D>();
        EnemyBoxCollider = GetComponent<BoxCollider2D>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyMoveSpeed = EnemyWalkSpeed;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        EnemyRigidBody.velocity = new Vector2(EnemyMoveSpeed, 0f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemyMoveSpeed = EnemyRunSpeed;
            EnemyAnimator.SetBool("IsRunning", true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        EnemyAnimator.SetBool("IsRunning", false);
        EnemyMoveSpeed = EnemyWalkSpeed;
    }
    void OnTriggerEnter2D(CapsuleCollider2D other)
    {
        if (other.tag == "Ground")
        {
            transform.localScale = new Vector2(Mathf.Sign(-EnemyRigidBody.velocity.x), 1f);
        }
    }
}


