using System;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace UIController
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private GameObject _questItemPrefab;
        [SerializeField] private GameObject _questListParent;
        [SerializeField] private TextMeshProUGUI _questStatusText;
        [SerializeField] private TextMeshProUGUI _questTitleText;
        [SerializeField] private TextMeshProUGUI _sideQuestStatusText;
        [SerializeField] private GameObject _questFinishPanel;
        [SerializeField] private TextMeshProUGUI _questFinishTitleText;
        [SerializeField] private TextMeshProUGUI _questFinishCoinText;
        private Dictionary<string, QuestItem> _idToItemMap = new();

        [Header("Animation Setting For Quest Finish")]
        [SerializeField] private float _slideDistance = 100f;
        [SerializeField] private float _slideDuration = 1f;
        [SerializeField] private float _waitBeforeDisappear = 2f;
        private Vector3 _startPos;
        private CanvasGroup _canvasGroup;
        void Start()
        {
            _startPos = _questFinishPanel.transform.localPosition;
            _canvasGroup = _questFinishPanel.GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange += QuestStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestInfoChange += QuestInfoChange;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest += FinishQuest;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;


        }
        private void OnDisable()
        {
            GameEventsManager.Instance.QuestEvents.OnQuestStateChange -= QuestStateChange;
            GameEventsManager.Instance.QuestEvents.OnQuestInfoChange -= QuestInfoChange;
            GameEventsManager.Instance.QuestEvents.OnFinishQuest -= FinishQuest;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void FinishQuest(string id)
        {
            Quest quest = QuestManager.Instance.GetQuestById(id);

            if (quest.info.showQuestFinishInfo)
            {
                _questFinishTitleText.text = $"Misi {quest.info.displayName} Selesai";
                _questFinishCoinText.text = $"+ {quest.info.goldReward}";
                SlideDownThenDisappear();
            }

        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu")
            {
                _questTitleText.text = "";
                _questStatusText.text = "";
            }
        }


        private void QuestInfoChange(string questDisplay, string text)
        {
            if (questDisplay.StartsWith("S"))
            {
                _sideQuestStatusText.text = questDisplay + "\n" + text;
                return;
            }
            if (questDisplay.StartsWith("R"))
            {
                _sideQuestStatusText.text = "";
                return;
            }

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

        private void SlideDownThenDisappear()
        {
            _questFinishPanel.transform.localPosition = _startPos;

            // Create sequence
            Sequence seq = DOTween.Sequence();

            // Slide from top to bottom
            seq.Append(_questFinishPanel.transform.DOLocalMoveY(_startPos.y - _slideDistance, _slideDuration).SetEase(Ease.OutQuad));

            // Wait a few seconds
            seq.AppendInterval(_waitBeforeDisappear);

            seq.Append(_canvasGroup.DOFade(0, 0.5f)); // fade out in 0.5 sec
            seq.OnComplete(() => _questFinishPanel.SetActive(false));
        }
    }
}

