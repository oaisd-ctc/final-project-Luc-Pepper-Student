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
    SkeletonArcher SkeleArcher;
    SpearGoblin EnemySpearGoblin;
    HeroMovement player;
    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<HeroMovement>();
        SkeleArcher = FindObjectOfType<SkeletonArcher>();
        Enemy = FindObjectOfType<EnemyScript>();
        EnemySpearGoblin = FindObjectOfType<SpearGoblin>();
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
        if (other.tag == "SkeletonArcherHitBox")
        {
            Debug.Log("ArrowHit");
            SkeleArcher.EnemyTakeDamage(1);
        }
        if (other.tag == "SpearGoblinHitBox")
        {
            Debug.Log("ArrowHit");
            EnemySpearGoblin.EnemyTakeDamage(1);
        }
    }
}

