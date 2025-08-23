using Ink.Runtime;
using TwoDotFiveDimension;
using UnityEngine;

public class InkExternalFunctions
{
    public void Bind(Story story)
    {
        story.BindExternalFunction("StartQuest", (string questId) => StartQuest(questId));
        story.BindExternalFunction("AdvanceQuest", (string questId) => AdvanceQuest(questId));
        story.BindExternalFunction("FinishQuest", (string questId) => FinishQuest(questId));

        story.BindExternalFunction("OpenShop", () => OpenShop());
        story.BindExternalFunction("ChangeMorale", (int amount) => ChangeMorale(amount));
    }

    public void Unbind(Story story)
    {
        story.UnbindExternalFunction("StartQuest");
        story.UnbindExternalFunction("AdvanceQuest");
        story.UnbindExternalFunction("FinishQuest");
        story.UnbindExternalFunction("OpenShop");
        story.UnbindExternalFunction("ChangeMorale");
    }


    private void StartQuest(string questId)
    {
        GameEventsManager.Instance.QuestEvents.StartQuest(questId);
    }

    private void AdvanceQuest(string questId)
    {
        GameEventsManager.Instance.QuestEvents.AdvanceQuest(questId);
    }

    private void FinishQuest(string questId)
    {
        GameEventsManager.Instance.QuestEvents.FinishQuest(questId);
    }

    private void OpenShop()
    {
        GameEventsManager.Instance.ShopEvents.ShopOpen();
    }

    private void ChangeMorale(int amount)
    {
        PlayerStats.Instance.ChangeMoralePoint(amount);
    }
}
