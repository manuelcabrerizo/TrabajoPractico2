using UnityEngine;

public class PauseState : IState
{
    public void Enter()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.OpenPauseMenu();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.StateMachine.PopState();
        }
    }

    public void Exit()
    {
        UIManager.Instance.CloseAll();
        Time.timeScale = 1.0f;
    }
}
