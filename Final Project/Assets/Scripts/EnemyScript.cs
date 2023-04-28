using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject PointA;
    [SerializeField] GameObject PointB;
    [SerializeField] GameObject RunPoint;
    [SerializeField] float EnemyWalkSpeed = 5f;
    [SerializeField] float EnemyRunSpeed = 10f;
    [SerializeField] float EnemyMoveSpeed = 5f;
    Rigidbody2D EnemyRigidBody;
    public PolygonCollider2D EnemyPolygonCollider;
    BoxCollider2D EnemyBoxCollider;
    CapsuleCollider2D EnemyCapsuleCollider;
    Animator EnemyAnimator;
    Transform CurrentPoint;
    void Start()
    {
        EnemyRigidBody = GetComponent<Rigidbody2D>();
        EnemyBoxCollider = GetComponent<BoxCollider2D>();
        EnemyCapsuleCollider = GetComponent<CapsuleCollider2D>();
        EnemyAnimator = GetComponent<Animator>();
        EnemyMoveSpeed = EnemyWalkSpeed;
        CurrentPoint = PointA.transform;
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (EnemyAnimator.GetBool("IsRunning") == true)
        {
            PointA = RunPoint;
            FlipandSpeed();
        }
        else
        {
            FlipandSpeed();
        }
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
        if (other.tag == "Player")
        {
            EnemyAnimator.SetBool("IsRunning", false);
            EnemyMoveSpeed = EnemyWalkSpeed;
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
        Gizmos.DrawWireSphere(RunPoint.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}


