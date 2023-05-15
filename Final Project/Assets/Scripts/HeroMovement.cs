using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float JumpHeight = 5f;
    [SerializeField] float RollSpeed = 10f;
    [SerializeField] float RollDuration = 5f;
    [SerializeField] float ShootDuration = 5f;
    [SerializeField] float ShotDelay = 5f;
    [SerializeField] float SwingDuration = 5f;
    [SerializeField] float Cooldown = 5f;
    [SerializeField] Transform Door1;
    float LastShot;
    [SerializeField] GameObject Arrow;
    [SerializeField] Transform Bow;
    bool First = true;
    int AttackAnimationInteger = 2;
    float FallVelocity;
    float DefaultRunSpeed;
    Vector2 MoveInput;
    Vector2 PlayerVelocity;
    Vector2 PlayerJumpHeight;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    Animator myAnimator;
    EnemyScript Enemy;
    SkeletonArcher SkeleArcher;
    SpearGoblin EnemySpearGoblin;
    DataSaver DataSaver;
    SkeletonMage SkeleMage;
    public float DoorCooldown;
    public bool CanEnterDoor;
    public static int DoorIndex;
    void Start()
    {
        DefaultRunSpeed = RunSpeed;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        Enemy = FindObjectOfType<EnemyScript>();
        SkeleArcher = FindObjectOfType<SkeletonArcher>();
        EnemySpearGoblin = FindObjectOfType<SpearGoblin>();
        SkeleMage = FindObjectOfType<SkeletonMage>();
        DataSaver = FindObjectOfType<DataSaver>();
        FallVelocity = myRigidbody.velocity.y;
    }
    void Update()
    {
        Run();
        FlipSprite();
        Falling();
        Rising();
    }
    void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    void Run()
    {
        PlayerVelocity = new Vector2(MoveInput.x * RunSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = PlayerVelocity;
        Invoke("ReturnSpeedToDefault", RollDuration);
    }
    void OnJump(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        PlayerJumpHeight = new Vector2(MoveInput.x * RunSpeed, JumpHeight);
        myRigidbody.velocity = PlayerJumpHeight;
    }
    void FlipSprite()
    {
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (PlayerHasHorizontalSpeed)
        {
            myAnimator.SetBool("IsRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("IsRunning", false);
        }
    }

    void Falling()
    {
        if (transform.position.y < FallVelocity && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsFalling", true);
            myAnimator.SetBool("IsRising", false);
        }
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsFalling", false);
            myAnimator.SetBool("IsRising", false);
        }
        FallVelocity = transform.position.y;
    }
    void Rising()
    {
        if (FallVelocity <= transform.position.y && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("IsRising", true);
        }
        FallVelocity = transform.position.y;
    }
    void OnRoll(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (myAnimator.GetBool("IsShooting") == true || myAnimator.GetBool("IsSwingingSword") == true) { return; }
        RunSpeed = RollSpeed;
        myAnimator.SetBool("IsRolling", true);
    }
    void OnBowShoot(InputValue value)
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if (Time.time - LastShot < Cooldown) { return; }
        LastShot = Time.time;
        Invoke("Shoot", ShotDelay);
        myAnimator.SetBool("IsShooting", true);
        Invoke("EndFire", ShootDuration);
    }

    void ReturnSpeedToDefault()
    {
        RunSpeed = DefaultRunSpeed;
        myAnimator.SetBool("IsRolling", false);
        CancelInvoke("ReturnSpeedToDefault");
    }
    void EndFire()
    {
        myAnimator.SetBool("IsShooting", false);
        CancelInvoke("EndFire");
    }
    void Shoot()
    {
        if (myAnimator.GetBool("IsShooting") == true)
        {
            Instantiate(Arrow, Bow.position, transform.rotation);
            CancelInvoke("Shoot");
        }
        else { return; }
    }
    void OnFire()
    {
        if (Time.time - LastShot < Cooldown) { return; }
        LastShot = Time.time;
        myAnimator.SetBool("IsSwingingSword", true);
        ChangeSwingAnimation();
        Invoke("CancelSword", SwingDuration);
    }
    void CancelSword()
    {
        myAnimator.SetBool("IsSwingingSword", false);
        CancelInvoke("CancelSword");
    }
    void ChangeSwingAnimation()
    {
        AttackAnimationInteger = AttackAnimationInteger + 1;
        if (AttackAnimationInteger > 2)
        {
            AttackAnimationInteger = 0;
        }
        myAnimator.SetInteger("AttackAnimation", AttackAnimationInteger);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (myAnimator.GetBool("IsSwingingSword") == false) { return; }
        if (other.tag == "EnemyHitBox")
        {
            if (First)
            {
                First = false;
                Debug.Log("Swordhit");
                Enemy.EnemyTakeDamage(1);
            }
        }
        if (other.tag == "SkeletonArcherHitBox")
        {
            if (First)
            {
                First = false;
                Debug.Log("SwordHit");
                SkeleArcher.EnemyTakeDamage(1);
            }
        }
        if (other.tag == "SpearGoblinHitBox")
        {
            if (First)
            {
                First = false;
                Debug.Log("SwordHit");
                EnemySpearGoblin.EnemyTakeDamage(1);
            }
        }
        if (other.tag == "SkeletonMageHitBox")
        {
            if (First)
            {
                First = false;
                Debug.Log("SwordHit");
                SkeleMage.EnemyTakeDamage(1);
            }
        }
        Invoke("SetFirstToTrue", Cooldown);
    }
    void SetFirstToTrue()
    {
        First = true;
        CancelInvoke("SetFirstToTrue");
    }
    void OnEnterDoor()
    {
        Invoke("WhichDoor", DoorCooldown);
    }
    void WhichDoor()
    {
        if (CanEnterDoor == false) { return; }
        if (CanEnterDoor)
        {
            Scene CameFrom = SceneManager.GetActiveScene();
            Debug.Log("LoadScene");
            DataSaver.PlayerTransform();
            SceneManager.LoadScene(DoorIndex);
            CanEnterDoor = false;
            
            CancelInvoke("WhichDoor");
        }
    }
}