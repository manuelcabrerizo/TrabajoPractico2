using UnityEngine;

public class MainMenuState : IState
{
    public void Enter()
    {
        Time.timeScale = 0.0f;
        GameManager.Instance.SetTextPlayerScoreActive(false);
        GameManager.Instance.SetTextCountDownActive(false);
        GameManager.Instance.SetBallActive(false);
        UIManager.Instance.OpenMainMenu();
    }

    public void Process(float dt)
    {
    }

    public void Exit()
    {
        UIManager.Instance.CloseMainMenu();
        Time.timeScale = 1.0f;
    }
}
