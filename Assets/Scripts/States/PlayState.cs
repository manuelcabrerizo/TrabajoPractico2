using UnityEngine;

public class PlayState : IState
{
    public void Enter()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.StateMachine.PushState(GameManager.Instance.PauseState);
        }
    }

    public void Exit()
    {
    }
}
