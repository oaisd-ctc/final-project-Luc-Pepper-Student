using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantScript : MonoBehaviour
{
    GameObject Canvas;
    bool HeroInteracting;
    HeroMovement Hero;
    void Start()
    {
        Canvas = GameObject.Find("Canvas");
        Hero = FindObjectOfType<HeroMovement>();
        Canvas.SetActive(false);
    }
    void Update()
    {
        HeroInteracting = Hero.InteractWithMerchant;
    }
    void ShowingCanvas()
    {
        Canvas.SetActive(true);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && HeroInteracting ==  true)
        {
            ShowingCanvas();
        }
    }
}
