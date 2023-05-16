using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int DoorNumber;
    [SerializeField] Transform DoorPoint;
    HeroMovement Hero;
    void Start()
    {
        Hero = FindObjectOfType<HeroMovement>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DoorDetector")
        {
            Hero.CanEnterDoor = true;
            HeroMovement.DoorIndex = DoorNumber;
        }
        else { return; }
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "DoorDetector")
        {
            Hero.CanEnterDoor = false;
        }
    }
}
