using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpear : MonoBehaviour
{
    [SerializeField] float SpearSpeed = 1f;
    [SerializeField] float SpearDrop = 0f;
    [SerializeField] float SpearLifeTime = 5f;
    Rigidbody2D myRigidBody;
    EnemyScript Enemy;
    SpearGoblin SpearGoblinScript;
    HeroMovement player;

    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<HeroMovement>();
        SpearGoblinScript = FindObjectOfType<SpearGoblin>();
        Enemy = FindObjectOfType<EnemyScript>();
        xSpeed = SpearGoblinScript.transform.localScale.x * SpearSpeed;
    }
    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, -SpearDrop);
        FlipSpear();
        Destroy(gameObject, SpearLifeTime);
    }
    void FlipSpear()
    {
        bool SpearDirection = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (SpearDirection)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        if (other.tag == "Player")
        {
            Debug.Log("SpearHitPlayer");
        }
    }
}

