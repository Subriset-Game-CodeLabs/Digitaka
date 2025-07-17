using UnityEngine;

namespace QuestSystem
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool _isFinished = false;
        private string _questId;
        private int _stepIndex;
        public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
        {
           _questId = questId;
           _stepIndex = stepIndex;
            if (questStepState != null && string.IsNullOrEmpty(questStepState))
            {
                SetQuestStepState(questStepState);
            }
        }

        protected void FinishQuestStep()
        {
            if (!_isFinished)
            {
                _isFinished = true;
                GameEventsManager.Instance.QuestEvents.AdvanceQuest(_questId);
                Destroy(gameObject);
            }
        }
        protected void ChangeState(string newState, string newStatus)
        {
            GameEventsManager.Instance.QuestEvents.QuestStepStateChange(_questId, _stepIndex, new QuestStepState(newState, newStatus));
        }

        protected abstract void SetQuestStepState(string state);
    }
}