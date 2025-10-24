using System;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : PersistentSingleton<CutsceneManager>
{
    private Dictionary<string, string> _playedCutscenes = new Dictionary<string, string>();

    public bool HasPlayed(string cutsceneId)
    {
        return _playedCutscenes.ContainsKey(cutsceneId);
    }

    public void MarkAsPlayed(string cutsceneId, string sceneName)
    {
        if (!_playedCutscenes.ContainsKey(cutsceneId))
        {
            _playedCutscenes.Add(cutsceneId, sceneName);
        }
    }

    public void ResetCutscene(string sceneName)
    {
        List<string> keysToRemove = new List<string>();

        foreach (var pair in _playedCutscenes)
        {
            if (pair.Value == sceneName)
            {
                keysToRemove.Add(pair.Key);
            }
        }
        
        foreach (var key in keysToRemove)
        {
            _playedCutscenes.Remove(key);
        }
    }
    
    public void ResetCutscene()
    {
        _playedCutscenes.Clear();
    }
}
