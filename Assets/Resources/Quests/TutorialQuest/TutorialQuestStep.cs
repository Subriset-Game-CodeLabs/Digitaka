using Input;
using QuestSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialQuestStep : QuestStep
{
    private int _attaackPressed = 0;
    private int _attackPresstoComplete = 3;
    private int _healthPotionUsed = 0;
    private int _manaPotionUsed = 0;
    private int _tutorialProggress = 1;

    void Start()
    {
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Tekan tombol serang {_attaackPressed} / {_attackPresstoComplete} kali"
            );
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        InputManager.Instance.PlayerInput.Attack.OnDown += AttackPressed;
        InputManager.Instance.PlayerInput.Jump.OnDown += DashPressed;
        InputManager.Instance.PlayerInput.Ultimate.OnDown += UltimatePressed;
        GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer += HealthPressed;
        GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer += ManaPressed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        InputManager.Instance.PlayerInput.Attack.OnDown -= AttackPressed;
        InputManager.Instance.PlayerInput.Jump.OnDown -= DashPressed;
        InputManager.Instance.PlayerInput.Ultimate.OnDown -= UltimatePressed;
        GameEventsManager.Instance.StatsEvents.OnChangeHealthPlayer -= HealthPressed;
        GameEventsManager.Instance.StatsEvents.OnChangeManaPlayer -= ManaPressed;
    }

    void AttackPressed()
    {
        if (_tutorialProggress != 1)
        {
            return;
        }

        if (_attaackPressed < _attackPresstoComplete)
        {
            _attaackPressed++;
            GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Tekan tombol serang {_attaackPressed} / {_attackPresstoComplete} kali"
            );
            Debug.Log("Quest Info Changed");
        }
        if (_attaackPressed >= _attackPresstoComplete)
        {
            GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Tekan tombol dash"
            );
            GameEventsManager.Instance.DialogueEvents.EnterDialogue("Tutorial2", false);

            _tutorialProggress += 1;
        }
    }

    void DashPressed()
    {
        if (_tutorialProggress != 2)
        {
            return;
        }

        GameEventsManager.Instance.DialogueEvents.EnterDialogue("Tutorial3", false);
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Tekan tombol ultimate"
            );
        _tutorialProggress += 1;
    }

    void UltimatePressed()
    {
        if (_tutorialProggress != 3)
        {
            return;
        }


        GameEventsManager.Instance.DialogueEvents.EnterDialogue("Tutorial4", false);
        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Gunakan Potion Darah {_healthPotionUsed} / 1 \n Gunakan Potion Darah {_manaPotionUsed} / 1"
            );
        _tutorialProggress += 1;
    }


    void HealthPressed()
    {
        if (_tutorialProggress != 4)
        {
            return;
        }
        _healthPotionUsed++;

        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Gunakan Potion Darah {_healthPotionUsed} / 1 \n Gunakan Potion Darah {_manaPotionUsed} / 1"
            );

        if (_healthPotionUsed >= 1 && _manaPotionUsed >= 1)
        {
            GameEventsManager.Instance.QuestEvents.FinishQuest("TutorialQuest");
             GameEventsManager.Instance.QuestEvents.QuestInfoChange("","");
            Destroy(gameObject);
        }
    }

    void ManaPressed()
    {
        if (_tutorialProggress != 4)
        {
            return;
        }
        _manaPotionUsed++;

        GameEventsManager.Instance.QuestEvents.QuestInfoChange(
                "Main Quest: Latihan",
                $"Gunakan Potion Darah {_healthPotionUsed} / 1 \n Gunakan Potion Darah {_manaPotionUsed} / 1"
            );
        if (_healthPotionUsed >= 1 && _manaPotionUsed >= 1)
        {
            GameEventsManager.Instance.QuestEvents.FinishQuest("TutorialQuest");
            GameEventsManager.Instance.QuestEvents.QuestInfoChange("", "");
            Destroy(gameObject);
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
