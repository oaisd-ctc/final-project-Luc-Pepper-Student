using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public float ArrowSpeed = 1f;
    [SerializeField] float ArrowDrop = 0f;
    [SerializeField] float ArrowLifeTime = 5f;
    Rigidbody2D myRigidBody;
    EnemyScript Enemy;
    SkeletonArcher SkeleArcher;
    HeroMovement player;

    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<HeroMovement>();
        SkeleArcher = FindObjectOfType<SkeletonArcher>();
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
        if (transform.position.x > player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.tag == "Player")
        {
            Debug.Log("ArrowHitPlayer");
        }
    }
}

