using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DialogueEvents
{
    public event Action<string, bool, bool> onEnterDialogue;
    public event Action<bool> onDialogueStarted;
    public event Action onDialogueFinishied;
    public event Action<string> onSpeakerChanged;
    public event Action<Sprite> onPotraitChanged;
    public event Action<string, List<Choice>, bool> onDisplayDialogue;
    public event Action<int> onUpdateChoiceIndex;
    public event Action<string, Ink.Runtime.Object> onUpdateInkDialogueVariables;
    public event Action<bool> onLineTypingAnimation;
    public event Action onDialogueResume;

    public void EnterDialogue(string knotName, bool allowSkip = true, bool isCutscene = false)
    {
        onEnterDialogue?.Invoke(knotName, allowSkip, isCutscene);
    }

    public void DialogueStarted(bool isCutscene = false)
    {
        onDialogueStarted?.Invoke(isCutscene);
    }

    public void DialogueFinsihed()
    {
        onDialogueFinishied?.Invoke();
    }

    public void DialogueResumed()
    {
        onDialogueResume?.Invoke();
    }

    public void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices, bool instant = false)
    {
        onDisplayDialogue?.Invoke(dialogueLine, dialogueChoices, instant);
    }

    public void UpdateChoiceIndex(int choiceIndex)
    {
        onUpdateChoiceIndex?.Invoke(choiceIndex);
    }

    public void UpdateInkDialogueVariables(string name, Ink.Runtime.Object value)
    {
        onUpdateInkDialogueVariables?.Invoke(name, value);
    }

    public void SpeakerChanged(string speaker)
    {
        onSpeakerChanged?.Invoke(speaker);
    }

    public void PortraitChanged(Sprite portrait)
    {
        onPotraitChanged?.Invoke(portrait);
    }
    public void LineTypingAnimation(bool isRunning)
    {
        onLineTypingAnimation?.Invoke(isRunning);
    }
}
