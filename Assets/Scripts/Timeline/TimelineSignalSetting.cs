using Input;
using UIController;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSignalSetting : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    private void OnEnable() {
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied += DialogueFinishied;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied -= DialogueFinishied;
    }

    public void DialogueFinishied()
    {
        ActiveUIMode();
        director.Resume();
    }

    public void ToogleUI()
    {
        UIManager.Instance.HideCanvas();
    }

    public void ActiveUIMode()
    {
        InputManager.Instance.UIMode();
    }

    public void ActivePlayerMode()
    {
        InputManager.Instance.PlayerMode();
    }

    public void PlayDialogueSkip(string _knotName)
    {
        GameEventsManager.Instance.DialogueEvents.EnterDialogue(_knotName);
    }

    public void PlayDialogueNoSkip(string _knotName)
    {
        GameEventsManager.Instance.DialogueEvents.EnterDialogue(_knotName, false);
    }

    public void PauseDirector()
    {
        director.Pause();
    }
}
