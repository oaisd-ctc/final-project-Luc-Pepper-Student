using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] int DoorNumber;
    [SerializeField] Transform DoorPoint;
    HeroMovement Hero;
    DataSaver DataSaver;
    void Start()
    {
        Hero = FindObjectOfType<HeroMovement>();
        DataSaver = FindObjectOfType<DataSaver>();
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
