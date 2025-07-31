using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueChoiceButton : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private TextMeshProUGUI choiceText;

    private int choiceIndex;

    public void SetChoiceText(string choiceText)
    {
        this.choiceText.text = choiceText;
    }

    public void SetChoiceIndex(int choiceIndex)
    {
        this.choiceIndex = choiceIndex;
    }

    public void SelectButton()
    {
        button.Select();
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameEventsManager.Instance.DialogueEvents.UpdateChoiceIndex(choiceIndex);
    }
}
