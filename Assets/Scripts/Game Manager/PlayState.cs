using UnityEngine;

public class PlayState : GameManagerState
{
    public PlayState(GameManager gameManager) : base(gameManager) { }

    public override void Enter()
    {
        Debug.Log("Game Manager entered Play state");
    }

    public override void Exit()
    {
        Debug.Log("Game Manager exited Play state");
    }

    public override void Update()
    {

    }
}
