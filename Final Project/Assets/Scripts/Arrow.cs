using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float ArrowSpeed = 1f;
    [SerializeField] float ArrowDrop = 0f;
    [SerializeField] float ArrowLifeTime = 5f;
    Rigidbody2D myRigidBody;
    EnemyScript Enemy;
    HeroMovement player;
    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<HeroMovement>();
        Enemy = FindObjectOfType<EnemyScript>();
        xSpeed = player.transform.localScale.x * ArrowSpeed;
    }
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, -ArrowDrop);
        Destroy(gameObject, ArrowLifeTime);
        FlipArrow();
    }
    void FlipArrow()
    {
        bool ArrowDirection = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (ArrowDirection)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.tag == "EnemyHitBox")
        {
            Debug.Log("ArrowHit");
            Enemy.EnemyTakeDamage(1);
        }
    }
}

