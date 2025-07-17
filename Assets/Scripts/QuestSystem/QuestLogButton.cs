using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QuestSystem
{
    public class QuestLogButton: MonoBehaviour, ISelectHandler
    {
        public Button button { get; private set; }
        private TextMeshProUGUI _buttonText;
        private UnityAction _onSelectAction;
        public void Initialize(string displayName, UnityAction selectAction)
        {
            button = GetComponent<Button>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
            _buttonText.text = displayName;
            _onSelectAction = selectAction;
        }

        public void OnSelect(BaseEventData eventData)
        {
            _onSelectAction?.Invoke();
        }
        public void SetState(QuestState questState)
        {
            switch (questState)
            {
                case QuestState.RequirementsNotMet: 
                case QuestState.CanStart:
                    _buttonText.color = Color.red;
                    break;
                case QuestState.InProgress:
                    _buttonText.color = Color.yellow;
                    break;
                case QuestState.CanFinish:
                case QuestState.Finished:
                    _buttonText.color = Color.green;
                    break;
                default:
                    Debug.LogWarning("Unhandled quest state: " + questState);
                    break;
            }
        }
    }

}