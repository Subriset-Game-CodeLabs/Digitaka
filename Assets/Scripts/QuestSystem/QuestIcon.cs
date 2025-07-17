using UnityEngine;

namespace QuestSystem
{
    public class QuestIcon:MonoBehaviour
    {
        [Header("Icons")]
        [SerializeField] private GameObject _requirementsNotMetToStartIcon;
        [SerializeField] private GameObject _canStartIcon;
        [SerializeField] private GameObject _requirementsNotMetToFinishIcon;
        [SerializeField] private GameObject _canFinishIcon;

        public void SetState(QuestState newState, bool startPoint, bool finishPoint)
        {
            // set all to inactive
            _requirementsNotMetToStartIcon.SetActive(false);
            _canStartIcon.SetActive(false);
            _requirementsNotMetToFinishIcon.SetActive(false);
            _canFinishIcon.SetActive(false);

            // set the appropriate one to active based on the new state
            switch (newState)
            {
                case QuestState.RequirementsNotMet:
                    if (startPoint) { _requirementsNotMetToStartIcon.SetActive(true); }
                    break;
                case QuestState.CanStart:
                    if (startPoint) { _canStartIcon.SetActive(true); }
                    break;
                case QuestState.InProgress:
                    if (finishPoint) { _requirementsNotMetToFinishIcon.SetActive(true); }
                    break;
                case QuestState.CanFinish:
                    if (finishPoint) { _canFinishIcon.SetActive(true); }
                    break;
                case QuestState.Finished:
                    break;
                default:
                    Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                    break;
            }
        }
    }
}