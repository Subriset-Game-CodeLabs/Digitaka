using UnityEngine.Serialization;

namespace QuestSystem
{
    [System.Serializable]
    public class QuestStepState
    {
        public string State;

        public QuestStepState(string state)
        {
            State = state;
        }

        public QuestStepState()
        {
            State = "";
        }
    }
}