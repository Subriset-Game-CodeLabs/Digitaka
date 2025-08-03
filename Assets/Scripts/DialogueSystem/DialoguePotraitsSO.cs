using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialoguePotraitsSO", menuName = "Scriptable Objects/DialoguePotraitsSO")]
public class DialoguePotraitsSO : ScriptableObject
{
    [System.Serializable]
    public class PotraitEntry
    {
        public string key;
        public Sprite sprite;
    }

    [SerializeField]
    private List<PotraitEntry> potraits = new List<PotraitEntry>();
    private Dictionary<string, Sprite> _portraitDict;

    private void OnEnable()
    {
        _portraitDict = new Dictionary<string, Sprite>();
        foreach (var entry in potraits)
        {
            if (!string.IsNullOrEmpty(entry.key) && !_portraitDict.ContainsKey(entry.key))
            {
                _portraitDict.Add(entry.key, entry.sprite);
            }
        }
    }

    public Sprite this[string key]
    {
        get
        {
            if (_portraitDict != null && _portraitDict.TryGetValue(key, out var sprite))
            {
                return sprite;
            }
            Debug.LogWarning($"Key '{key}' not found in ListOfPortrait.");
            return null;
        }
    }
}
