using System;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
namespace UIController
{
    public class QuestUI:MonoBehaviour
    {
        [SerializeField] private GameObject _questItemPrefab;
        [SerializeField] private GameObject _questListParent;
        private Dictionary<string, QuestItem> _idToItemMap = new();

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;

        }
        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
        }
        
        private void QuestStateChange(Quest quest)
        {
            QuestItem questItem = CreateQuestIfNotExist(quest); 
            questItem.SetState(quest.state);
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
   
