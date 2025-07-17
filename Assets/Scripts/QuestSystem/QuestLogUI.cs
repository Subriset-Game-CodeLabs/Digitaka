using System;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QuestSystem
{
    public class QuestLogUI: MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private GameObject _contentParent;
        [SerializeField] private QuestLogScrollingList _questLogScrollingList;
        [SerializeField] private TextMeshProUGUI _questNameText;
        [SerializeField] private TextMeshProUGUI _questStatusText;
        [SerializeField] private TextMeshProUGUI _questGoldRewardText;
        [SerializeField] private TextMeshProUGUI _questExpRewardText;
        [SerializeField] private TextMeshProUGUI _questRequirementText;
        
        private Button _questFirstButton;
        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
            InputManager.Instance.PlayerInput.QuestLog.OnDown += QuestLogToggle;
            InputManager.Instance.UIInput.QuestLog.OnDown += QuestLogToggle;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
            InputManager.Instance.PlayerInput.QuestLog.OnDown -= QuestLogToggle;
            InputManager.Instance.UIInput.QuestLog.OnDown -= QuestLogToggle;
        }

        private void QuestLogToggle()
        {
            if (_contentParent.activeInHierarchy)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }
        }
        private void ShowUI()
        {
            _contentParent.SetActive(true);
            InputManager.Instance.UIMode();
            if (_questFirstButton != null)
            {
                _questFirstButton.Select();
            }
        }
        private void HideUI()
        {
            _contentParent.SetActive(false);
            InputManager.Instance.PlayerMode();
            EventSystem.current.SetSelectedGameObject(null);
        }
        private void QuestStateChange(Quest quest)
        {
            QuestLogButton questLogButton = _questLogScrollingList.CreateQuestIfNotExists(quest, () =>
            {
                Debug.Log("quest: " + quest.info.displayName);
                SetQuestLogInfo(quest);
            });
            if(_questFirstButton == null)
            {
                _questFirstButton = questLogButton.button;
            }
            
            questLogButton.SetState(quest.state);
        }
        private void SetQuestLogInfo(Quest quest)
        {
            _questNameText.text = quest.info.displayName;
            _questStatusText.text = quest.GetFullStatusText();
            _questGoldRewardText.text = $"{quest.info.goldReward} Gold";
            _questExpRewardText.text = $"{quest.info.experienceReward} Exp";
            _questRequirementText.text = "";
            foreach (var questPrerequisite in quest.info.questPrerequisites)
            {
                _questRequirementText.text = questPrerequisite.displayName;
            }
        }
 
    }
}