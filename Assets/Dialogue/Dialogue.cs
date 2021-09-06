﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public interface DialogueSection
    {
        string GetSpeakerName();
        string GetSpeechContents();
        DialogueSection GetNextSection();
    }

    public class Monologue : DialogueSection
    {
        public string speaker_name;
        public string content;
        public DialogueSection next;

        public Monologue(
            string speaker_name = "Default_Speaker",
            string content = "There's nothing here...",
            DialogueSection next = null)
        {
            this.speaker_name = speaker_name;
            this.content = content;
            this.next = next;
        }

        public DialogueSection GetNextSection()
        {
            return next;
        }

        public string GetSpeakerName()
        {
            return speaker_name;
        }

        public string GetSpeechContents()
        {
            return content;
        }
    }

    public class Choices : DialogueSection
    {
        public string speaker_name;
        public string contents;
        public List<Tuple<string, DialogueSection>> choices;

        public Choices(
            string speaker_name = "Default_Speaker",
            string contents = "There's nothing here... make a choice.",
            List<Tuple<string, DialogueSection>> choices = null)
        {
            this.speaker_name = speaker_name;
            this.contents = contents;
            this.choices = choices;
        }

        public DialogueSection GetNextSection()
        {
            return null;
        }

        public string GetSpeakerName()
        {
            return speaker_name;
        }

        public string GetSpeechContents()
        {
            return contents;
        }
    }

    public static Tuple<string, DialogueSection> Choice(string choice, DialogueSection next)
    {
        return new Tuple<string, DialogueSection>(choice, next);
    }

    public static List<Tuple<string, DialogueSection>> ChoiceList(params Tuple<string, DialogueSection>[] entries)
    {
        List<Tuple<string, DialogueSection>> result = new List<Tuple<string, DialogueSection>>();

        foreach (var tuple in entries)
        {
            result.Add(tuple);
        }

        return result;
    }
}