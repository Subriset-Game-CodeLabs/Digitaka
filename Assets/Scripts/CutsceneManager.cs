using System;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : PersistentSingleton<CutsceneManager>
{
    private HashSet<string> _playedCutscenes = new HashSet<string>();

    public bool HasPlayed(string cutsceneId) => _playedCutscenes.Contains(cutsceneId);
    public void MarkAsPlayed(string cutsceneId) => _playedCutscenes.Add(cutsceneId);

    public void ResetCutscene()
    {
        _playedCutscenes.Clear();
    }
}
