using System;
using Input;
using UnityEngine;

namespace QuestSystem
{
    [RequireComponent(typeof(SphereCollider))]
    public class QuestPoint : MonoBehaviour
    {
        [Header("Dialogue (optional)")]
        [SerializeField] private string _dialogueKnotName;

        [Header("Quest")]
        [SerializeField] private QuestInfoSO _questInfoForPoint;

        [Header("Config")]
        [SerializeField] private bool _startPoint = true;
        [SerializeField] private bool _finishPoint = true;

        private bool _playerIsNear = false;
        private string _questId;
        private QuestState _currentQuestState;
        private QuestIcon _questIcon;
        private void Awake()
        {
            _questId = _questInfoForPoint.id;
            _questIcon = GetComponentInChildren<QuestIcon>();
        }

        private void Start()
        {
            QuestManager.Instance.InitializeQuest();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
            InputManager.Instance.PlayerInput.Interact.OnDown += SubmitPressed;
        }

        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
            InputManager.Instance.PlayerInput.Interact.OnDown -= SubmitPressed;

        }

        private void SubmitPressed()
        {
            if (!_playerIsNear)
            {
                return;
            }

            // if we have a knot name defined, try to start dialogue with it
            if (!_dialogueKnotName.Equals(""))
            {
                Debug.Log("start");
                GameEventsManager.Instance.DialogueEvents.EnterDialogue(_dialogueKnotName);
            }
            // otherwise, start or finish the quest immediately without dialogue
            else
            {
                // start or finish a quest
                if (_currentQuestState.Equals(QuestState.CanStart) && _startPoint)
                {
                    GameEventsManager.Instance.QuestEvents.StartQuest(_questId);
                    Debug.Log("Starting quest: " + _questId);
                }
                else if (_currentQuestState.Equals(QuestState.CanFinish) && _finishPoint)
                {
                    GameEventsManager.Instance.QuestEvents.FinishQuest(_questId);
                }
            }
        }

        private void QuestStateChange(Quest quest)
        {
            // only update the quest state if this point has the corresponding quest
            if (quest.info.id.Equals(_questId))
            {
                _currentQuestState = quest.state;
                _questIcon.SetState(_currentQuestState, _startPoint, _finishPoint);
            }
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = true;
            }
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = false;
            }
        }
    }
}