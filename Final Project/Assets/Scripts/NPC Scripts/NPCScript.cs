using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    void Start()
    {

    }
    void Update()
    {
        Flip();
    }
    void Flip()
    {
        if (transform.position.x > Player.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (transform.position.x < Player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
