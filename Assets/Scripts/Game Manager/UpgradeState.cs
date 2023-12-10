using UnityEngine;

public class UpgradeState : GameManagerState
{
    public UpgradeState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Game Manager entered Upgrade State");
        Time.timeScale = 0f;
    }

    public override void Exit()
    {
        Debug.Log("Game Manager exited Upgrade State");
        Time.timeScale = 1f;
    }

    public override void Update()
    {
    }
}
