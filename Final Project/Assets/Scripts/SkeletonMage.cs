using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMage : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] Vector3 TeleportPoint;
    [SerializeField] float SightDistance = 5f;
    [SerializeField] float TeleportSightCheck = 5f;
    [SerializeField] float TeleportTime = 5f;
    [SerializeField] float TeleportEndTime = 5f;
    bool CanTeleport = true;
    Animator SkeletonMageAnimator;
    void Start()
    {
        SkeletonMageAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        Flip();
        Teleport();
    }
    void Flip()
    {
        if (Vector2.Distance(transform.position, Player.position) < SightDistance)
        {
            if (transform.position.x > Player.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (transform.position.x < Player.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
    void Teleport()
    {
        if (CanTeleport == false) { return; }
        if (Vector2.Distance(transform.position, Player.position) < TeleportSightCheck)
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
}
