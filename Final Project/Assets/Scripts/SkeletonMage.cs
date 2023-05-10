using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Fireball;
    [SerializeField] Transform Staff;
    [SerializeField] Vector3 TeleportPoint;
    [SerializeField] float SightDistance = 5f;
    [SerializeField] float TeleportSightCheck = 5f;
    [SerializeField] float TeleportTime = 5f;
    [SerializeField] float TeleportEndTime = 5f;
    [SerializeField] float Cooldown = 5f;
    [SerializeField] float ShotDelay = 5f;
    [SerializeField] float ShootDuration = 5f;
    [SerializeField] float SkeletonMageMaxHealth = 5f;
    [SerializeField] float SkeletonMageHealth;
    [SerializeField] float DeadBodyTimer;
    float LastShot;
    bool Died;
    bool CanTeleport = true;
    Animator SkeletonMageAnimator;
    void Start()
    {
        SkeletonMageAnimator = GetComponent<Animator>();
        SkeletonMageHealth = SkeletonMageMaxHealth;
    }
    void Update()
    {
        Flip();
        Teleport();
    }
    void Flip()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) < SightDistance)
        {
            if (transform.position.x > Player.transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Shot();
            }
            if (transform.position.x < Player.transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Shot();
            }
        }
    }
    void Teleport()
    {
        if (Died == true) { return; }
        if (CanTeleport == false) { return; }
        if (Vector2.Distance(transform.position, Player.transform.position) < TeleportSightCheck)
        {
            SkeletonMageAnimator.SetBool("IsTeleporting", true);
            Invoke("TeleportInvoke", TeleportTime);
        }
    }
    void TeleportInvoke()
    {
        if (CanTeleport == false) { return; }
        Debug.Log("Teleport");
        transform.position = TeleportPoint;
        SkeletonMageAnimator.SetBool("IsTeleporting", false);
        SkeletonMageAnimator.SetBool("EndingTeleport", true);
        Invoke("TeleportEnd", TeleportEndTime);
        CanTeleport = false;
    }
    void TeleportEnd()
    {
        SkeletonMageAnimator.SetBool("EndingTeleport", false);
    }
    void Shot()
    {
        if (Time.time - LastShot < Cooldown) { return; }
        LastShot = Time.time;
        Invoke("Shoot", ShotDelay);
        SkeletonMageAnimator.SetBool("IsShooting", true);
        Invoke("EndFire", ShootDuration);
    }
    void Shoot()
    {
        if (SkeletonMageAnimator.GetBool("IsShooting") == true)
        {
            Fireball fireball = Instantiate(Fireball, Staff.position, transform.rotation).GetComponent<Fireball>();
            fireball.SetTarget(Player);
            CancelInvoke("Shoot");
        }
        else { return; }
    }
    void EndFire()
    {
        SkeletonMageAnimator.SetBool("IsShooting", false);
        CancelInvoke("EndFire");
    }
    public void EnemyTakeDamage(float DamageAmount)
    {
        SkeletonMageHealth -= DamageAmount;

        if (SkeletonMageHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Died = true;
        SkeletonMageAnimator.Play("Die");
        Destroy(gameObject, DeadBodyTimer);
        this.enabled = false;
    }
}
