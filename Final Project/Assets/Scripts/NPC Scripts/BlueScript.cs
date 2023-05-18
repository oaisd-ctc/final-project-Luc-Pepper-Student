using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueScript : MonoBehaviour
{
    bool HasBeenSaved;
    void Start()
    {
        // Make HasBeenSaved False once everything else complete.
        HasBeenSaved = true;
    }
    void Update()
    {
        if (HasBeenSaved == false) { return; }
    }
}
