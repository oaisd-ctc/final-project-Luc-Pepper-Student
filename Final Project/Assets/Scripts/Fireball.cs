using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float FireSpeed = 5f;
    [SerializeField] float LifeTime = 5f;
    [SerializeField] float RiseSpeed = 5f;
    [SerializeField] GameObject Player;
    bool FireballChasing;
    void Start()
    {

    }
    void Update()
    {
        FireballChase();
        Destroy(gameObject, LifeTime);
    }
    void FireballChase()
    {
        if (transform.position.x > Player.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            transform.position += Vector3.left * FireSpeed * Time.deltaTime;
        }
        if (transform.position.x < Player.transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.position += Vector3.right * FireSpeed * Time.deltaTime;
        }
        if (transform.position.y < Player.transform.position.y)
        {
            transform.position += Vector3.up * RiseSpeed * Time.deltaTime;
        }
        if (transform.position.y > Player.transform.position.y)
        {
            transform.position += Vector3.down * RiseSpeed * Time.deltaTime;
        }
    }
    public void SetTarget(GameObject Player)
    {
        this.Player = Player;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Debug.Log("FireHitPlayer");
            Destroy(gameObject);
        }
    }
}

