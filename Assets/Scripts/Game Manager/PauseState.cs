using UnityEngine;

public class PauseState : GameManagerState
{
    public PauseState(GameManager gameManager) : base(gameManager)
    {
    }

    public override void Enter()
    {
        Debug.Log("Game Manager entered Pause State");
        Time.timeScale = 0f;
    }

    public override void Exit()
    {
        Debug.Log("Game Manager exited Pause State");
        Time.timeScale = 1f;
    }

    public override void Update()
    {

    }
}
