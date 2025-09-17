using System.Runtime.InteropServices;
using Input;
using UIController;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineSignalSetting : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private bool _isTutorial = false;
    [SerializeField]
    private bool _isOnTutorialScene = false;
    //tutorial variable
    private int _attackPressToComplete = 3;
    private int _attackPressed = 0;
    private bool _isAttackTutorialComplete = false;
    private bool _isDashTutorialComplete = false;
    private bool _isUltimateTutorialComplete = false;
    private bool _isHealthTutorialComplete = false;
    private bool _isManaTutorialComplete = false;
    private int _currentTutorial = 0;

    private void OnEnable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied += DialogueFinishied;
        InputManager.Instance.PlayerInput.Attack.OnDown += AttackPressed;
        InputManager.Instance.PlayerInput.Jump.OnDown += DashPressed;
        InputManager.Instance.PlayerInput.Ultimate.OnDown += UltimatePressed;
        GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer += HealthPressed;
        GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer += ManaPressed;

    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied -= DialogueFinishied;
        InputManager.Instance.PlayerInput.Attack.OnDown -= AttackPressed;
        InputManager.Instance.PlayerInput.Jump.OnDown -= DashPressed;
        InputManager.Instance.PlayerInput.Ultimate.OnDown -= UltimatePressed;
        GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= HealthPressed;
        GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer -= ManaPressed;
    }

    // Khusus buat scene tutorial
    void AttackPressed()
    {
        if (!_isOnTutorialScene)
        {
            return;
        }
        if (_attackPressed < _attackPressToComplete)
        {
            _attackPressed++;
        }

        if (_attackPressed >= _attackPressToComplete && !_isAttackTutorialComplete)
        {
            _isAttackTutorialComplete = true;
            ActiveUIMode();
            ToogleUI();
            PlayDialogueNoSkip("Tutorial2");
            _currentTutorial = 2;
        }
    }

    void DashPressed()
    {
        if (!_isOnTutorialScene)
        {
            return;
        }
        if (!_isDashTutorialComplete)
        {
            _isDashTutorialComplete = true;
            ActiveUIMode();
            ToogleUI();
            PlayDialogueNoSkip("Tutorial3");
            _currentTutorial = 3;
        }
    }

    void UltimatePressed()
    {
        if (!_isOnTutorialScene)
        {
            return;
        }
        if (!_isUltimateTutorialComplete)
        {
            _isUltimateTutorialComplete = true;
            ActiveUIMode();
            // ToogleUI();
            PlayDialogueNoSkip("Tutorial4");
            _currentTutorial = 4;
        }
    }

    void HealthPressed()
    {
        if (!_isOnTutorialScene)
        {
            return;
        }
        _isHealthTutorialComplete = true;
        if (_isHealthTutorialComplete && _isManaTutorialComplete)
        {
            ActiveUIMode();
            ToogleUI();
            _isTutorial = false;
            director.Resume();
        }
    }

    void ManaPressed()
    {
        if (!_isOnTutorialScene)
        {
            return;
        }
        _isManaTutorialComplete = true;
        if (_isHealthTutorialComplete && _isManaTutorialComplete)
        {
            ActiveUIMode();
            ToogleUI();
            _isTutorial = false;
            director.Resume();
        }
    }

    public void TeleportToScene(string _targetScene)
    {
        SceneManager.Instance.ChangeScene(_targetScene);
        GameManager.Instance.SetChapterScene(_targetScene);
        GameManager.Instance.StartGame();
    }


    public void DialogueFinishied()
    {
        if (_isTutorial)
        {
            if (_currentTutorial == 4)
            {
                return;
            }
            ToogleUI();
            return;
        }
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

    public void StartQuest(string _questId)
    {
        GameEventsManager.Instance.QuestEvents.StartQuest(_questId);
    }
}
