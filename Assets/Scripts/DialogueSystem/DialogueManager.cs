using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Input;
using QuestSystem;
using UIController;
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
    private bool _allowSkip;
    private string _currentText;

    private bool _resumeRequested = false;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string PAUSE_TAG = "pause";


    protected override void Awake()
    {
        base.Awake();
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
        InputManager.Instance.UIInput.Interact.OnDown += SubmitPressed;
        GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
        GameEventsManager.Instance.DialogueEvents.onLineTypingAnimation += LineTypeAnimation;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.Instance.DialogueEvents.onUpdateChoiceIndex -= UpdateChoiceIndex;
        GameEventsManager.Instance.DialogueEvents.onUpdateInkDialogueVariables -= UpdateInkDialogueVariable;
        InputManager.Instance.UIInput.Interact.OnDown -= SubmitPressed;
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
        if (!dialougePlaying)
        {
            return;
        }

        if (_isPlayingTypingAnimation && !_allowSkip)
        {
            return;
        }
        if (story.currentChoices.Count > 0)
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
        ContinueOrExitStory();
    }

    private void EnterDialogue(string knotName, bool allowSkip, bool isCutscene)
    {
        if (dialougePlaying)
        {
            return;
        }

        Debug.Log("Play dialogue " + knotName);
        dialougePlaying = true;
        _allowSkip = allowSkip;

        GameEventsManager.Instance.DialogueEvents.DialogueStarted(isCutscene);
        InputManager.Instance.UIMode();
        UIManager.Instance.HideCanvas();

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
            if (_isPlayingTypingAnimation)
            {
                HandleTags(story.currentTags);
                GameEventsManager.Instance.DialogueEvents.DisplayDialogue(
            story.currentText,
            story.currentChoices,
            true
            );
                return;
            }

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
                case PAUSE_TAG:
                    StartCoroutine(PauseDialogue());
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator PauseDialogue()
    {
        _resumeRequested = false;
        Debug.Log("Dialogue Paused");
        UIManager.Instance.HideDialoguePanel();

        GameEventsManager.Instance.DialogueEvents.onDialogueResume += HandleResume;

        yield return new WaitUntil(() => _resumeRequested);

        Debug.Log("Dialogue Resume");
        UIManager.Instance.ShowDialoguePanel();
        GameEventsManager.Instance.DialogueEvents.onDialogueResume -= HandleResume;

        ContinueOrExitStory();
    }

    private void HandleResume()
    {
        _resumeRequested = true;
    }

    private void ExitDialogue()
    {
        Debug.Log("Exiting Dialogue start");

        dialougePlaying = false;

        InputManager.Instance.PlayerMode();
        UIManager.Instance.ShowCanvas();
        GameEventsManager.Instance.DialogueEvents.DialogueFinsihed();

        GameEventsManager.Instance.StatsEvents.ChangePlayerMorale();
        inkDialogueVariables.StopListening(story);

        story.ResetState();
        Debug.Log("Exiting Dialogue Finish");
    }

    private bool IsLineBlank(string dialogueLine)
    {
        return dialogueLine.Trim().Equals("") || dialogueLine.Trim().Equals("\n");
    }


}
