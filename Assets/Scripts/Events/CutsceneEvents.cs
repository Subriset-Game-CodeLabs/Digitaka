using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Events
{
    public class CutsceneEvents
    {
        public event Action<string> OnPlayCutscene;
        public void PlayCutscene(string cutsceneId)
        {
            OnPlayCutscene?.Invoke(cutsceneId);
        }
    }
}
