using UnityEngine;

public class PlayState : IState
{
    private int _player1Score = 0;
    private int _player2Score = 0;
    
    public void Enter()
    {
        _player1Score = 0;
        _player2Score = 0;
        GameManager.Instance.SetTextPlayer1Score(_player1Score);
        GameManager.Instance.SetTextPlayer2Score(_player2Score);
        GameManager.Instance.SetTextPlayerScoreActive(true);
        GameManager.Instance.SetBallActive(true);
        GameManager.Instance.ResetObjectsForNewGame();
        GameManager.Instance.ResetPlayerPowerUps();
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.StateMachine.PushState(GameManager.Instance.PauseState);
        }
    }

    public void Exit()
    {
        GameManager.Instance.ResetPlayerPowerUps();
        GameManager.Instance.ResetObjectsForNewGame();
        GameManager.Instance.SetBallActive(false);
    }
    
    public void Player1Score()
    {
        _player1Score++;
        GameManager.Instance.SetTextPlayer1Score(_player1Score);
        if (_player1Score < GameManager.Instance.WinningScore)
        {
            GameManager.Instance.StateMachine.PushState(GameManager.Instance.ScoreState);
        }
        else
        {
            GameManager.Instance.StateMachine.ChangeState(GameManager.Instance.WinState);
        }
    }

    public void Player2Score()
    {
        _player2Score++;
        GameManager.Instance.SetTextPlayer2Score(_player2Score);
        if (_player2Score < GameManager.Instance.WinningScore)
        {
            GameManager.Instance.StateMachine.PushState(GameManager.Instance.ScoreState);
        }
        else
        {
            GameManager.Instance.StateMachine.ChangeState(GameManager.Instance.WinState);
        }
    }
    
}
