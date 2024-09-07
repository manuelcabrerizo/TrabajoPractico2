using UnityEngine;

public class MainMenuState : IState
{
    public void Enter()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.OpenMainMenu();
    }

    public void Update()
    {
    }

    public void Exit()
    {
        UIManager.Instance.CloseMainMenu();
        Time.timeScale = 1.0f;
    }
}
