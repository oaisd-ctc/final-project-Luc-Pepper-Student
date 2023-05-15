using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    HeroMovement Player;
    bool SaveData;
    public Vector3 ExitPosition;
    void Start()
    {
        Player = GetComponent<HeroMovement>();
    }
    void Update()
    {
        if(SaveData)
        {
            ExitPosition = Player.transform.position;
        }
    }
    public void PlayerTransform()
    {
        Player.transform.position = ExitPosition;
    }
    void StopSavingData()
    {
        SaveData = false;
    }
}
