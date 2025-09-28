using Input;
using UIController;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineSignalSetting : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector director;

    [SerializeField]
    private bool _isTutorial = false;
    [SerializeField]
    private bool _isOnTutorialScene = false;

    private bool _isFinished = false;

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
        GameEventsManager.Instance.QuestEvents.OnFinishQuest += OnFinishQuest;

        GameEventsManager.Instance.QuestEvents.OnAdvanceQuest += OnAdvanceQuest;

        director.stopped += OnDirectorStopped;
    }

    void OnDisable()
    {
        GameEventsManager.Instance.DialogueEvents.onDialogueFinishied -= DialogueFinishied;
        InputManager.Instance.PlayerInput.Attack.OnDown -= AttackPressed;
        InputManager.Instance.PlayerInput.Jump.OnDown -= DashPressed;
        InputManager.Instance.PlayerInput.Ultimate.OnDown -= UltimatePressed;
        GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= HealthPressed;
        GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer -= ManaPressed;
        GameEventsManager.Instance.QuestEvents.OnFinishQuest -= OnFinishQuest;
        GameEventsManager.Instance.QuestEvents.OnAdvanceQuest -= OnAdvanceQuest;

        director.stopped -= OnDirectorStopped;
    }

    private void OnDirectorStopped(PlayableDirector dir)
    {
        _isFinished = true;
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
            HideUI();
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
            HideUI();
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
            HideUI();
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
            HideUI();
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

    public void OnFinishQuest(string questId)
    {
        if (questId == "SelamatkanLelakiTua" || questId == "SelamatkanWargaDariPrajurit" || questId == "MasukKeKerajaan" || questId == "LawanDewataCengkar")
        {
            director.Resume();
        }
    }

    public void OnAdvanceQuest(string questId)
    {
        if (questId == "MasukKeKerajaan")
        {
            director.Resume();
        }
    }

    public void DialogueFinishied()
    {
        //sementara kayaknya buat scene B5
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "B5")
        {
            return;
        }

        if (_isTutorial)
        {
            if (_currentTutorial == 4)
            {
                return;
            }
            // HideUI();
            director.Resume();
            return;
        }
        if (!_isFinished)
        {
            Debug.Log("Continue Cutscene");
            ActiveUIMode();
            HideUI();
            director.Resume();
        }
    }

    public void HideUI()
    {
        UIManager.Instance.HideCanvas();
    }

    public void ShowUI()
    {
        UIManager.Instance.ShowCanvas();
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
        GameEventsManager.Instance.DialogueEvents.EnterDialogue(_knotName, isCutscene: true);
    }

    public void PlayDialogueNoSkip(string _knotName)
    {
        GameEventsManager.Instance.DialogueEvents.EnterDialogue(_knotName, false, true);
    }

    public void MutePlayer()
    {
        TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
        foreach (var track in timelineAsset.GetOutputTracks())
        {
            if (track.name == "Character Track")
            {
                director.SetGenericBinding(track, null);
            }
        }
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
