using System;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using TMPro;
namespace UIController
{
    public class QuestUI:MonoBehaviour
    {
        [SerializeField] private GameObject _questItemPrefab;
        [SerializeField] private GameObject _questListParent;
        [SerializeField] private TextMeshProUGUI _questStatusText;
        [SerializeField] private TextMeshProUGUI _questTitleText;
        private Dictionary<string, QuestItem> _idToItemMap = new();

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestInfoChange += QuestInfoChange;

        }
        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestInfoChange -= QuestInfoChange;
        }


        private void QuestInfoChange(string questDisplay, string text)
        {
            Debug.Log("Change quest info");
            _questTitleText.text = questDisplay;
            _questStatusText.text = text;
        }

        private void QuestStateChange(Quest quest)
        {
            // Debug.Log(quest.info.displayName);
            // Debug.Log(quest.GetFullStatusText());
            // _questStatusText.text = quest.GetFullStatusText();
            // QuestItem questItem = CreateQuestIfNotExist(quest); 
            // questItem.SetState(quest.state);
        } 
        private QuestItem CreateQuestIfNotExist(Quest quest)
        {
            QuestItem questItem;
            if (!_idToItemMap.TryGetValue(quest.info.id, out questItem))
                questItem = InstantiateQuestItem(quest);
            return questItem;
        }
        private QuestItem InstantiateQuestItem(Quest quest)
        {
            GameObject newItem = Instantiate(_questItemPrefab, _questListParent.transform);
            QuestItem questItem = newItem.GetComponent<QuestItem>();
            questItem.Initialize(quest.info.displayName); 
            _idToItemMap[quest.info.id] = questItem;
            return questItem;
        }
    }
}
   
