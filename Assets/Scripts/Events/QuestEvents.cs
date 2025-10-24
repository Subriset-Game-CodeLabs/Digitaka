using System;
using QuestSystem;

namespace Events
{
    public class QuestEvents
    {
        public event Action<string> OnStartQuest;
        public void StartQuest(string id)
        {
            OnStartQuest?.Invoke(id);
        }

        public event Action<string> OnAdvanceQuest;
        public void AdvanceQuest(string id)
        {
            OnAdvanceQuest?.Invoke(id);
        }

        public event Action<string> OnFinishQuest;
        public void FinishQuest(string id)
        {
            OnFinishQuest?.Invoke(id);
        }

        public event Action<Quest> OnQuestStateChange;
        public void QuestStateChange(Quest quest)
        {
            OnQuestStateChange?.Invoke(quest);
            QuestUpdate();
        }
        
        public event Action OnQuestDelete;
        public void QuestDelete()
        {
            OnQuestDelete?.Invoke();
        }

        public event Action OnQuestUpdate;
        private void QuestUpdate()
        {
            OnQuestUpdate?.Invoke();
        }
        public event Action<string, int, QuestStepState> OnQuestStepStateChange;
        public void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            OnQuestStepStateChange?.Invoke(id, stepIndex, questStepState);
        }

        public event Action<string, string> OnQuestInfoChange;
        public void QuestInfoChange(string questDisplay, string text)
        {
            OnQuestInfoChange?.Invoke(questDisplay, text);
        }
    }
}