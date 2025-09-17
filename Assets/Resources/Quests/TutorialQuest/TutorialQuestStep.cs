using Input;
using QuestSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialQuestStep : QuestStep
{
    private int _attaackPressed = 0;
    private int _attackPresstoComplete = 3;

    void OnEnable()
    {
        InputManager.Instance.PlayerInput.Attack.OnDown += AttackPressed;
    }

    void OnDisable()
    {
        InputManager.Instance.PlayerInput.Attack.OnDown += AttackPressed;
    }

    void AttackPressed()
    {
        if (_attaackPressed < _attackPresstoComplete)
        {
            _attaackPressed++;
            UpdateState();
        }
        if (_attaackPressed >= _attackPresstoComplete)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string status = $"Tekan tombol serang {_attaackPressed} / {_attackPresstoComplete} kali";
        ChangeState("", status);
    }

    protected override void SetQuestStepState(string state)
    {
    }

}
