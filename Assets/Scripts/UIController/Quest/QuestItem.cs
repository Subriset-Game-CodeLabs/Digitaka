using QuestSystem;
using TMPro;
using UnityEngine;

namespace UIController
{
    public class QuestItem:MonoBehaviour
    {
        private TMP_Text _questNameText;
        public void Initialize(string displayName)
        {
            _questNameText = GetComponent<TMP_Text>();
            _questNameText.text = displayName;
        }
        public void SetState(QuestState questState)
        {
            switch (questState)
            {
                case QuestState.RequirementsNotMet: 
                case QuestState.CanStart:
                    _questNameText.color = Color.red;
                    gameObject.SetActive(false);
                    break;
                case QuestState.InProgress:
                    _questNameText.color = Color.yellow;
                    gameObject.SetActive(true);
                    break;
                case QuestState.CanFinish:
                case QuestState.Finished:
                    _questNameText.color = Color.green;
                    gameObject.SetActive(false);
                    break;
                default:
                    Debug.LogWarning("Unhandled quest state: " + questState);
                    break;
            }
        }
    }
}