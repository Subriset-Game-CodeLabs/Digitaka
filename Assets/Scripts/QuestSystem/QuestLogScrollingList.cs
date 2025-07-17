using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuestSystem
{
    public class QuestLogScrollingList: MonoBehaviour
    {
        [SerializeField] private GameObject _parrentContent;
        [SerializeField] private GameObject _questLogButtonPrefab;
        [SerializeField] private RectTransform _scrollRectTransform;
        [SerializeField] private RectTransform _contentRectTransform;
        private Dictionary<string, QuestLogButton> _idToButtonMap = new();
        
        // private void Start()
        // {
        //     for(int i = 0 ; i < 50 ; i++)
        //     {
        //         QuestInfoSO questInfo = ScriptableObject.CreateInstance<QuestInfoSO>();
        //         questInfo.id = "test" + i;
        //         questInfo.displayName = "Test Quest " + i;
        //         questInfo.questStepPrefabs = new GameObject[0];
        //         Quest quest = new Quest(questInfo);
        //         QuestLogButton questLogButton = CreateQuestIfNotExists(quest, () =>
        //         {
        //             Debug.Log("Selected: " + quest.info.displayName);
        //         });
        //         if (i == 0)
        //         {
        //             questLogButton.button.Select();
        //         }
        //     }
        //
        //
        // }

        public QuestLogButton CreateQuestIfNotExists(Quest quest, UnityAction selectAction)
        {
            QuestLogButton questLogButton;
            if (!_idToButtonMap.TryGetValue(quest.info.id, out questLogButton))
                questLogButton = InstantiateQuestLogButton(quest, selectAction);
            return questLogButton;
        }
        
        private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
        {
            GameObject newButton = Instantiate(_questLogButtonPrefab, _parrentContent.transform);
            QuestLogButton questLogButton = newButton.GetComponent<QuestLogButton>();
            questLogButton.gameObject.name = quest.info.displayName + "_Button";
            RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
            questLogButton.Initialize(quest.info.id, () =>
            {
                selectAction();
                UpdateScrolling(buttonRectTransform);
            });
            _idToButtonMap[quest.info.id] = questLogButton;
            return questLogButton;
        }

        private void UpdateScrolling(RectTransform buttonRectTransform )
        {
            // callculate min max button
            float buttonMinY = Math.Abs(buttonRectTransform.anchoredPosition.y);
            float buttonMaxY = buttonMinY + buttonRectTransform.rect.height;
            
            // calculate the scroll rect height
            float contentMinY = _contentRectTransform.anchoredPosition.y;
            float contentMaxY = contentMinY + _scrollRectTransform.rect.height;

            if (buttonMaxY > contentMaxY)
            {
                _contentRectTransform.anchoredPosition = new Vector2(
                    _contentRectTransform.anchoredPosition.x,
                    buttonMaxY - _scrollRectTransform.rect.height);
            }
            else if (buttonMinY < contentMinY)
            {
                _contentRectTransform.anchoredPosition = new Vector2(
                    _contentRectTransform.anchoredPosition.x,
                    buttonMinY);
            }
    
        }
    }
}