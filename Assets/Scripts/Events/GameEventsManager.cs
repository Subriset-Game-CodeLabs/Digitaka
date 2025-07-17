using Events;

public class GameEventsManager: PersistentSingleton<GameEventsManager>
{
    public QuestEvents QuestEvents { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        QuestEvents = new QuestEvents();   
    }
}
