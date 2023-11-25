public abstract class GameManagerState
{
    protected GameManager gameManager;

    public GameManagerState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
}
