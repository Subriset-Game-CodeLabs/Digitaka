using System.Collections.Generic;
using DefaultNamespace;
using Ink.Runtime;
using Input;
using QuestSystem;
using UnityEngine;

public class DialogueManager : PersistentSingleton<DialogueManager>
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset inkJson;

    [Header("Portrait Data")]
    [SerializeField] private DialoguePotraitsSO PortraitList;

    private Story story;
    private int currentChoiceIndex = -1;
    private bool dialougePlaying = false;

    private InkExternalFunctions inkExternalFunctions;
    private InkDialogueVariables inkDialogueVariables;

    private bool _isPlayingTypingAnimation;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";

    void Awake()
    {
        story = new Story(inkJson.text);
        inkExternalFunctions = new InkExternalFunctions();
        inkExternalFunctions.Bind(story);

        inkDialogueVariables = new InkDialogueVariables(story);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        inkExternalFunctions.Unbind(story);
    }

    void OnEnable()
    {
        GameEventsManager.Instance.DialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.Instance.DialogueEvents.onUpdateChoiceIndex += UpdateChoiceIndex;
        GameEventsManager.Instance.DialogueEvents.onUpdateInkDialogueVariables += UpdateInkDialogueVariable;
        InputManager.Instance.PlayerInput.Interact.OnDown += SubmitPressed;
        GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
        GameEventsManager.Instance.DialogueEvents.onLineTypingAnimation += LineTypeAnimation;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.Instance.DialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
        GameEventsManager.Instance.DialogueEvents.onUpdateInkDialogueVariables -= UpdateInkDialogueVariable;
        InputManager.Instance.PlayerInput.Interact.OnDown -= SubmitPressed;
        GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
        GameEventsManager.Instance.DialogueEvents.onLineTypingAnimation -= LineTypeAnimation;
    }

    private void LineTypeAnimation(bool value)
    {
        _isPlayingTypingAnimation = value;
    }

    private void QuestStateChange(Quest quest)
    {
        GameEventsManager.Instance.DialogueEvents.UpdateInkDialogueVariables(
            quest.info.id + "State",
            new StringValue(quest.state.ToString())
        );
    }

    private void SubmitPressed()
    {
        if (!dialougePlaying )
        {
            return;
        }

        if (_isPlayingTypingAnimation)
        {
            return;
        }

        ContinueOrExitStory();
    }

    private void UpdateInkDialogueVariable(string name, Ink.Runtime.Object value)
    {
        inkDialogueVariables.UpdateVariableState(name, value);
    }

    private void UpdateChoiceIndex(int choiceIndex)
    {
        currentChoiceIndex = choiceIndex;
    }

    private void EnterDialogue(string knotName)
    {
        if (dialougePlaying)
        {
            return;
        }

        dialougePlaying = true;

        GameEventsManager.Instance.DialogueEvents.DialogueStarted();

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            // Warning log
        }

        inkDialogueVariables.SyncVariablesAndStartListening(story);

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.currentChoices.Count > 0 && currentChoiceIndex != -1)
        {
            story.ChooseChoiceIndex(currentChoiceIndex);

            currentChoiceIndex = -1;
        }

        if (story.canContinue)
        {
            string dialogueLine = story.Continue();

            while (IsLineBlank(dialogueLine) && story.canContinue)
            {
                dialogueLine = story.Continue();
            }

            if (IsLineBlank(dialogueLine) && !story.canContinue)
            {
                Debug.Log("Exit");
                ExitDialogue();
            }
            else
            {
                HandleTags(story.currentTags);
                GameEventsManager.Instance.DialogueEvents.DisplayDialogue(dialogueLine, story.currentChoices);
            }
        }
        else if (story.currentChoices.Count == 0)
        {
            ExitDialogue();
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // Loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    GameEventsManager.Instance.DialogueEvents.SpeakerChanged(tagValue);
                    break;
                case PORTRAIT_TAG:
                    Sprite portrait = PortraitList[tagValue];
                    GameEventsManager.Instance.DialogueEvents.PortraitChanged(portrait);
                    break;
                default:
                    break;
            }
            
        }
    }

    private void ExitDialogue()
    {
        Debug.Log("Exiting Dialogue");

        dialougePlaying = false;

        GameEventsManager.Instance.DialogueEvents.DialogueFinsihed();

        inkDialogueVariables.StopListening(story);

        story.ResetState();
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }


}
