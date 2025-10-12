using System;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest Info")]
    public class QuestInfoSO: QuestData
    {
        [field:SerializeField] public string id { get; private set; }
        
        [Header("General")]
        [field:SerializeField] public string displayName { get; private  set; }
        [field: SerializeField] public bool showQuestFinishInfo { get; private set; }
        
        [Header("Requirements")]
        [field:SerializeField] public QuestInfoSO[] questPrerequisites { get; private set; }

        [Header("Steps")]
        [field:SerializeField] public GameObject[] questStepPrefabs { get; private  set; }

        [Header("Rewards")]
        [field:SerializeField] public int goldReward { get; private set; }
        [field:SerializeField] public int experienceReward { get; private set; }
        private void OnValidate()
        {
            #if UNITY_EDITOR
            id = this.name;
            UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}