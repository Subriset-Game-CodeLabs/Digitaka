using QuestSystem;
using UnityEngine;

public class MissingBrotherQuestStep: QuestStep
{
    private void Start()
    {
        string status = "Find the missing brother";
        ChangeState("", status);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            string status = "You found the missing brother!";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
    }
        
}
