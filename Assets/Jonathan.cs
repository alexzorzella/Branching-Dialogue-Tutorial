using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialogue;

public class Jonathan : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(Conversation());
    }

    private DialogueSection Conversation()
    {
        string localName = "Jonathan";

        Monologue fine = new Monologue(localName, "That's nice to hear. Have a nice day!");
        Monologue not_fine = new Monologue(localName, "That's too bad... hope it improves!");

        Choices b = new Choices(localName, "How are you today?", ChoiceList(Choice("Fine", fine), Choice("Not so fine...", not_fine)));
        Monologue a = new Monologue(localName, "Good morning, I'm Jonathan.", b);

        return a;
    }
}