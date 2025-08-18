using Events;

public class GameEventsManager: PersistentSingleton<GameEventsManager>
{
    public QuestEvents QuestEvents { get; private set; }
    public StatsEvents StatsEvents { get; private set; }
    public DialogueEvents DialogueEvents { get; private set; }
    public ShopEvents ShopEvents { get; private set; }
    public PlayerActionsEvents PlayerActionsEvents { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        QuestEvents = new QuestEvents();
        DialogueEvents = new DialogueEvents();
        ShopEvents = new ShopEvents();
        StatsEvents = new StatsEvents();
        PlayerActionsEvents = new PlayerActionsEvents();
    }
}
