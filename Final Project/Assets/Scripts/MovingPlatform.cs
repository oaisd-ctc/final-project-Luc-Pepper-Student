using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float PlatformSpeed = 5f;
    public Transform PositionA, PositionB;
    Vector2 TargetPosition;
    void Start()
    {
        TargetPosition = PositionB.position;
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, PositionA.position) < .1f) TargetPosition = PositionB.position;

        if (Vector2.Distance(transform.position, PositionB.position) < .1f) TargetPosition = PositionA.position;

        transform.position = Vector2.MoveTowards(transform.position, TargetPosition, PlatformSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
        if (collision.CompareTag("Enemy"))
        {
            collision.transform.SetParent(null);
        }
    }
}
