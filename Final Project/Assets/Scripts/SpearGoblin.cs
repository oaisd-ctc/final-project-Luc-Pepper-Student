using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGoblin : MonoBehaviour
{

    [SerializeField] float DeadBodyTimer = 5f;
    [SerializeField] float SpearGoblinHealth = 3f;
    [SerializeField] float MaxSpearGoblinHealth = 5f;
    [SerializeField] float SightDistance = 10f;
    [SerializeField] float ShotDelay = 5f;
    [SerializeField] float ShootDuration = 5f;
    [SerializeField] float Cooldown = 5f;
    [SerializeField] GameObject Spear;
    [SerializeField] Transform ThrowPoint;
    EnemySpear SpearGoblinSpear;
    float LastShot;
    Rigidbody2D SpearGoblinRigidBody;
    public Transform PlayerTransform;
    Animator SpearGoblinAnimator;
    void Start()
    {
        SpearGoblinHealth = MaxSpearGoblinHealth;
        SpearGoblinRigidBody = GetComponent<Rigidbody2D>();
        SpearGoblinAnimator = GetComponent<Animator>();
        SpearGoblinSpear = FindObjectOfType<EnemySpear>();
    }
    void Update()
    {
        FlipandShoot();
    }
    public void EnemyTakeDamage(float DamageAmount)
    {
        SpearGoblinHealth -= DamageAmount;

        if (SpearGoblinHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        SpearGoblinRigidBody.constraints = RigidbodyConstraints2D.FreezePosition;
        SpearGoblinAnimator.Play("Die");
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
        SpearGoblinAnimator.SetBool("IsShooting", true);
        Invoke("EndFire", ShootDuration);
    }
    void Shoot()
    {
        if (SpearGoblinAnimator.GetBool("IsShooting") == true)
        {
            Instantiate(Spear, ThrowPoint.position, transform.rotation);
            CancelInvoke("Shoot");
        }
        else { return; }
    }
    void EndFire()
    {
        SpearGoblinAnimator.SetBool("IsShooting", false);
        CancelInvoke("EndFire");
    }
}


