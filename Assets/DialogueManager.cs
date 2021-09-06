using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Dialogue;

public class DialogueManager : MonoBehaviour
{
    public DialogueSection currentSection;

    [Header("Text Components")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentsText;

    [Header("Dialogue Choice")]
    public GameObject proceedConverstaionObject;
    public GameObject dialogueChoiceObject;
    public Transform parentChoicesTo;

    [Header("Fade")]
    public float canvasGroupFadeTime = 5F;
    bool canvasGroupDisplaying;
    public CanvasGroup dialogueCanvasGroup;

    private void Start()
    {
        InitalizePanel();
    }

    private void InitalizePanel()
    {
        dialogueCanvasGroup.alpha = 0F;
    }

    private void Update()
    {
        UpdateCanvasOpacity();
        PrepareForOptionDisplay();
        DisplayDialogueOptions();
    }

    private void UpdateCanvasOpacity()
    {
        dialogueCanvasGroup.alpha = Mathf.Lerp(dialogueCanvasGroup.alpha, canvasGroupDisplaying ? 1F : 0F, Time.deltaTime * canvasGroupFadeTime);
    }

    private void PrepareForOptionDisplay()
    {
        if (optionsBeenDisplayed)
        {
            return;
        }

        if (typeof(Choices).IsInstanceOfType(currentSection))
        {
            ResetDisplayOptionsFlags();
        }
    }

    public void StartDialogue(DialogueSection start)
    {
        canvasGroupDisplaying = true;
        ClearAllOptions();
        currentSection = start;
        DisplayDialogue();
    }

    public void ProceedToNext()
    {
        if (displayingChoices)
        {
            return;
        }

        if (currentSection.GetNextSection() != null)
        {
            currentSection = currentSection.GetNextSection();
            DisplayDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    public void DisplayDialogue()
    {
        if (currentSection == null)
        {
            EndDialogue();
            return;
        }

        bool isMonologue = typeof(Monologue).IsInstanceOfType(currentSection);

        proceedConverstaionObject.SetActive(isMonologue);

        ClearAllOptions();

        DisplayText();
    }

    private void DisplayText()
    {
        optionsBeenDisplayed = false;

        nameText.text = currentSection.GetSpeakerName();
        contentsText.text = currentSection.GetSpeechContents();
    }

    private void EndDialogue()
    {
        canvasGroupDisplaying = false;
        ClearAllOptions();
    }

    private void ClearAllOptions()
    {
        GameObject[] currentDialogueOptions = GameObject.FindGameObjectsWithTag("DialogueChoice");

        foreach (var entry in currentDialogueOptions)
        {
            Destroy(entry);
        }
    }

    int indexOfCurrentChoice = 0;
    [HideInInspector] public bool displayingChoices;
    private bool optionsBeenDisplayed;

    public void ResetDisplayOptionsFlags()
    {
        optionsBeenDisplayed = true;
        displayingChoices = true;
        indexOfCurrentChoice = 0;
    }

    public void DisplayDialogueOptions()
    {
        if(!typeof(Choices).IsInstanceOfType(currentSection))
        {
            return;
        }

        Choices choices = (Choices)currentSection;

        if (displayingChoices)
        {
            if(indexOfCurrentChoice < choices.choices.Count)
            {
                Tuple<string, DialogueSection> option = choices.choices[indexOfCurrentChoice];

                GameObject s = Instantiate(dialogueChoiceObject, Vector3.zero, Quaternion.identity);
                s.transform.SetParent(parentChoicesTo);
                s.GetComponent<RectTransform>().localScale = Vector3.one;

                DialogueOptionDisplay optionDisplayBehavior = s.GetComponent<DialogueOptionDisplay>();

                optionDisplayBehavior.SetDisplay(option.Item1, option.Item2);

                indexOfCurrentChoice++;
            }
            else
            {
                displayingChoices = false;
            }
        }
    }
}