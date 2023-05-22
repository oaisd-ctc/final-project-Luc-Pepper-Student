using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcher : MonoBehaviour
{
    [SerializeField] float DeadBodyTimer = 5f;
    [SerializeField] float SkeletonArcherHealth = 3f;
    [SerializeField] float MaxSkeletonArcherHealth = 5f;
    [SerializeField] float SightDistance = 10f;
    [SerializeField] float ShotDelay = 5f;
    [SerializeField] float ShootDuration = 5f;
    [SerializeField] float Cooldown = 5f;
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform Bow;
    EnemyArrow SkeleArcherEnemyArrow;
    float  SkeleArcherArrowSpeed;
    float LastShot;
    Rigidbody2D SkeletonArcherRigidBody;
    public Transform PlayerTransform;
    Animator SkeletonArcherAnimator;
    void Start()
    {
        SkeletonArcherHealth = MaxSkeletonArcherHealth;
        SkeletonArcherRigidBody = GetComponent<Rigidbody2D>();
        SkeletonArcherAnimator = GetComponent<Animator>();
        SkeleArcherEnemyArrow = FindObjectOfType<EnemyArrow>();
    }
    void Update()
    {
        FlipandShoot();
    }
    public void EnemyTakeDamage(float DamageAmount)
    {
        SkeletonArcherHealth -= DamageAmount;

        if (SkeletonArcherHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        SkeletonArcherRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        SkeletonArcherAnimator.Play("Die");
        Destroy(gameObject, DeadBodyTimer);
        this.enabled = false;
    }
    void FlipandShoot()
    {
        if (Vector2.Distance(transform.position, PlayerTransform.position) < SightDistance)
        {
            if (transform.position.x > PlayerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Shot();
            }
            if (transform.position.x < PlayerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Shot();
            }
        }
    }
    void Shot()
    {
        if (Time.time - LastShot < Cooldown) { return; }
        LastShot = Time.time;
        Invoke("Shoot", ShotDelay);
        SkeletonArcherAnimator.SetBool("IsShooting", true);
        Invoke("EndFire", ShootDuration);
    }
    void Shoot()
    {
        if (SkeletonArcherAnimator.GetBool("IsShooting") == true)
        {
            Instantiate(Arrow, Bow.position, transform.rotation);
            CancelInvoke("Shoot");
        }
        else { return; }
    }
    void EndFire()
    {
        SkeletonArcherAnimator.SetBool("IsShooting", false);
        CancelInvoke("EndFire");
    }
}
