using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    Queue<string> Sentences;
    void Start()
    {
        Sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting Conversation with " + dialogue.NameOfNPC);

        Sentences.Clear();
        foreach (string sentence in dialogue.Sentences)
        {
            Sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }
    public void DisplayNextSentence()
    {
        if (Sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = Sentences.Dequeue();
        Debug.Log(sentence);
    }
    void EndDialogue()
    {
        Debug.Log("EndConversation");
    }
}
