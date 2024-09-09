using UnityEngine;

public class WinState : IState
{
    public void Enter()
    {
        UIManager.Instance.OpenGameOverMenu();
        Time.timeScale = 0;
    }

    public void Process(float dt)
    {
    }

    public void Exit()
    {
        UIManager.Instance.CloseAll();
    }
}
