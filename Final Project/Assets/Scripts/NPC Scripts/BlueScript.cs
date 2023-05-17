using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueScript : MonoBehaviour
{
    public Dialogue Dialogue;
    bool HasBeenSaved;
    void Start()
    {
        // Make HasBeenSaved False once everything else complete.
        HasBeenSaved = true;
    }
    void Update()
    {
        if(HasBeenSaved == false) { return; }
        PlayerWalksIntoShop();
    }
    void PlayerWalksIntoShop()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            TriggerDialogue();
        }
        else { return; }
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Dialogue);
    }
}
