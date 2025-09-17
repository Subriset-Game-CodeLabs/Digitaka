using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelUI : MonoBehaviour
{
    [SerializeField]
    private GameObject contentParent;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private TextMeshProUGUI speakerText;
    [SerializeField]
    private Image portraitSprite;
    [SerializeField]
    private DialogueChoiceButton[] choiceButtons;

    private Coroutine displayLineCoroutine;

    private void Awake()
    {
        contentParent.SetActive(false);
        ResetPanel();
    }

    void OnEnable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueStarted += DialgoueStarted;
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied += DialogueFinishied;
        GameEventsManager.Instance.DialogueEvents.onDisplayDialogue += DisplayDialogue;
        GameEventsManager.Instance.DialogueEvents.onSpeakerChanged += SpeakerChanged;
        GameEventsManager.Instance.DialogueEvents.onPotraitChanged += PortraitChanged;
        InputManager.Instance.UIInput.Interact.OnDown += SubmitPressed;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueStarted -= DialgoueStarted;
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied -= DialogueFinishied;
        GameEventsManager.Instance.DialogueEvents.onDisplayDialogue -= DisplayDialogue;
        GameEventsManager.Instance.DialogueEvents.onSpeakerChanged -= SpeakerChanged;
        GameEventsManager.Instance.DialogueEvents.onPotraitChanged -= PortraitChanged;
        InputManager.Instance.UIInput.Interact.OnDown -= SubmitPressed;
    }

    private void SpeakerChanged(string speaker)
    {
        speakerText.text = speaker;
    }

    private void PortraitChanged(Sprite sprite)
    {
        portraitSprite.sprite = sprite;
    }

    private void DialgoueStarted()
    {
        contentParent.SetActive(true);
    }

    private void DialogueFinishied()
    {
        contentParent.SetActive(false);

        ResetPanel();
    }

    private void SubmitPressed()
    {

    }

    private void DisplayDialogue(string dialogueLine, List<Choice> dialogueChoices, bool instant)
    {
        if (displayLineCoroutine != null)
        {
            StopCoroutine(displayLineCoroutine);
            displayLineCoroutine = null;
            GameEventsManager.Instance.DialogueEvents.LineTypingAnimation(false);

        }
        Debug.Log(instant);
        if (!instant)
        {
            displayLineCoroutine = StartCoroutine(DisplayLine(dialogueLine));
        }
        else
        {
            dialogueText.text = dialogueLine;
        }

        if (dialogueChoices.Count > choiceButtons.Length)
        {
            // choices kebanyakan error
        }

        foreach (DialogueChoiceButton choiceButton in choiceButtons)
        {
            choiceButton.gameObject.SetActive(false);
        }

        // Handle Choices
        int choiceButtonIndex = dialogueChoices.Count - 1;
        for (int inkChoiceIndex = 0; inkChoiceIndex < dialogueChoices.Count; inkChoiceIndex++)
        {
            Choice dialogueChoice = dialogueChoices[inkChoiceIndex];
            DialogueChoiceButton choiceButton = choiceButtons[choiceButtonIndex];

            choiceButton.gameObject.SetActive(true);
            choiceButton.SetChoiceText(dialogueChoice.text);
            choiceButton.SetChoiceIndex(inkChoiceIndex);

            if (inkChoiceIndex == 0)
            {
                choiceButton.SelectButton();
                // GameEventsManager.Instance.dialogueEvents.UpdateChoiceIndex(0);
            }

            choiceButtonIndex--;
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = "";
        GameEventsManager.Instance.DialogueEvents.LineTypingAnimation(true);
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        GameEventsManager.Instance.DialogueEvents.LineTypingAnimation(false);
    }

    private void ResetPanel()
    {
        dialogueText.text = "";
    }
}
