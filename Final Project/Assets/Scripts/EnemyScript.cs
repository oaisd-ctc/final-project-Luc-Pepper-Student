using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject PointA;
    [SerializeField] GameObject PointB;
    [SerializeField] float EnemyWalkSpeed = 5f;
    [SerializeField] float EnemyRunSpeed = 10f;
    [SerializeField] float EnemyMoveSpeed = 5f;
    [SerializeField] float AttackDuration = 5f;
    [SerializeField] float ChaseDistance = 5f;
    [SerializeField] float EnemyHealth, EnemyMaxHealth = 5f;
    [SerializeField] float DeadBodyTimer = 5f;
    public Transform PlayerTransform;
    public bool IsChasing;
    GameObject PointADefault;
    Rigidbody2D EnemyRigidBody;
    PolygonCollider2D EnemyPolygonCollider;
    BoxCollider2D EnemyBoxCollider;
    CapsuleCollider2D EnemyCapsuleCollider;
    Animator EnemyAnimator;
    Transform CurrentPoint;
    void Start()
    {
        EnemyHealth = EnemyMaxHealth;
        EnemyRigidBody = GetComponent<Rigidbody2D>();
        EnemyPolygonCollider = GetComponent<PolygonCollider2D>();
        EnemyBoxCollider = GetComponent<BoxCollider2D>();
        EnemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyMoveSpeed = EnemyWalkSpeed;
        CurrentPoint = PointA.transform;
    }
    void Update()
    {
        Chasing();
    }
    void FlipandSpeed()
    {
        Vector2 point = CurrentPoint.position - transform.position;
        if (CurrentPoint == PointB.transform)
        {
            EnemyRigidBody.velocity = new Vector2(-EnemyMoveSpeed, 0);
        }
        else
        {
            EnemyRigidBody.velocity = new Vector2(EnemyMoveSpeed, 0);
        }
        if (Vector2.Distance(transform.position, CurrentPoint.position) < 0.5f && CurrentPoint == PointA.transform)
        {
            Flip();
            CurrentPoint = PointB.transform;
        }
        if (Vector2.Distance(transform.position, CurrentPoint.position) < 0.5f && CurrentPoint == PointB.transform)
        {
            Flip();
            CurrentPoint = PointA.transform;
        }
    }

    void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("HitPlayer");
            EnemyAnimator.SetBool("IsAttacking", true);
            Invoke("EndAttack", AttackDuration);
        }
    }
    void EndAttack()
    {
        EnemyAnimator.SetBool("IsAttacking", false);
        CancelInvoke("EndAttack");
    }

    void Chasing()
    {
        if (IsChasing)
        {
            EnemyMoveSpeed = EnemyRunSpeed;
            if (transform.position.x > PlayerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.left * EnemyMoveSpeed * Time.deltaTime;
            }
            if (transform.position.x < PlayerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.right * EnemyMoveSpeed * Time.deltaTime;
            }
            if (Vector2.Distance(transform.position, PlayerTransform.position) > ChaseDistance)
            {
                IsChasing = false;
                EnemyAnimator.SetBool("IsRunning", false);
                EnemyMoveSpeed = EnemyWalkSpeed;
            }
        }
        else
        {
            if(EnemyMoveSpeed > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (EnemyMoveSpeed < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            FlipandSpeed();
            if (Vector2.Distance(transform.position, PlayerTransform.position) < ChaseDistance)
            {
                EnemyAnimator.SetBool("IsRunning", true);
                IsChasing = true;
            }
        }
    }
    public void EnemyTakeDamage(float DamageAmount)
    {
        EnemyHealth -= DamageAmount;

        if(EnemyHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        EnemyRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        EnemyAnimator.Play("Die");
        Destroy(gameObject, DeadBodyTimer);
        this.enabled = false;
    }
}


