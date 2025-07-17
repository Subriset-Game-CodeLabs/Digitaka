using System;
using QuestSystem;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VisitPlaceQuestStep : QuestStep
{
    [SerializeField] private string _pillarNumberString = "First";

    private void Start()
    {
        string status = "Visit the " + _pillarNumberString + " Pillar";
        ChangeState("", status);
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            string status = "Visited the " + _pillarNumberString + " Pillar";
            ChangeState("", status);
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        // no state is needed for this quest step
    }
}
