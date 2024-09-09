using UnityEngine;

public class PauseState : IState
{
    public void Enter()
    {
        Time.timeScale = 0.0f;
        UIManager.Instance.OpenPauseMenu();
        GameManager.Instance.SetTextPlayerScoreActive(false);
        GameManager.Instance.SetTextCountDownActive(false);
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.StateMachine.PopState();
        }
    }

    public void Exit()
    {
        GameManager.Instance.SetTextPlayerScoreActive(true);
        if (GameManager.Instance.StateMachine.PeekState() == GameManager.Instance.ScoreState)
        {
            GameManager.Instance.SetTextCountDownActive(true);
        }

        UIManager.Instance.CloseAll();
        Time.timeScale = 1.0f;
    }
}
