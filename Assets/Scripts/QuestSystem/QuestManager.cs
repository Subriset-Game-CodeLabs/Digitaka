using System;
using System.Collections.Generic;
using TwoDotFiveDimension;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuestSystem
{
    public class QuestManager : PersistentSingleton<QuestManager>
    {
        [Header("Config")]
        // [SerializeField] private bool loadQuestState = true;

        private Dictionary<string, Quest> _questMap;
        private bool _alreadyInitialized = false;


        public void InitializeQuest()
        {
            if (_alreadyInitialized) return;
            _questMap = CreateQuestMap();
            foreach (Quest quest in _questMap.Values)
            {
                // initialize any loaded quest steps
                if (quest.state == QuestState.InProgress)
                {
                    quest.InstantiateCurrentQuestStep(transform);
                }
                // broadcast the initial state of all quests on startup
                GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
                Debug.Log("Load quest: " + quest.info.id + " with state: " + quest.state);
            }
            _alreadyInitialized = true;
        }
        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnStartQuest += StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest += AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += FinishQuest;
            GameEventsManager.Instance.QuestEvents.OnQuestStepStateChange += QuestStepStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestUpdate += UpdateQuest;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnStartQuest -= StartQuest;
            GameEventsManager.Instance.QuestEvents.OnAdvanceQuest -= AdvanceQuest;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= FinishQuest;
            GameEventsManager.Instance.QuestEvents.OnQuestStepStateChange -= QuestStepStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestUpdate -= UpdateQuest;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            {
                _questMap.Clear();
                _alreadyInitialized = false;
            }
        }
        private void UpdateQuest()
        {
            foreach (Quest quest in _questMap.Values)
            {
                // if we're now meeting the requirements, switch over to the CAN_START state
                if (quest.state == QuestState.RequirementsNotMet && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.info.id, QuestState.CanStart);
                }
            }
        }
        private void StartQuest(string id)
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.InProgress);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestById(id);

            // move on to the next step
            quest.MoveToNextStep();

            // if there are more steps, instantiate the next one
            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // if there are no more steps, then we've finished all of them for this quest
            else
            {
                ChangeQuestState(quest.info.id, QuestState.CanFinish);
            }
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);
            quest.ChangeState(state);
            GameEventsManager.Instance.QuestEvents.QuestStateChange(quest);
        }

        private bool CheckRequirementsMet(Quest quest)
        {
            // start true and prove to be false
            bool meetsRequirements = true;

            // check quest prerequisites for completion
            foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.Finished)
                {
                    meetsRequirements = false;
                }
            }

            return meetsRequirements;
        }
        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ClaimRewards(quest);
            ChangeQuestState(quest.info.id, QuestState.Finished);
        }

        private void ClaimRewards(Quest quest)
        {
            Debug.Log("Rewards claimed for quest: " + quest.info.id);
            Debug.Log($"Quest {quest.info.id} completed! Rewards: Gold - {quest.info.goldReward}");
            PlayerStats.Instance.AddCoin(quest.info.goldReward);
        }

        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
            QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
            // Create the quest map
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, new Quest(questInfo));
                Debug.Log("Quest added to map: " + questInfo.id);
            }
            return idToQuestMap;
        }

        private Quest GetQuestById(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogError("ID not found in the Quest Map: " + id);
            }
            return quest;
        }


    }
}

