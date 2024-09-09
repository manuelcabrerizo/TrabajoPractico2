using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreState : IState
{
    private float _timeToWait = 3;
    private float _time = 0;
    public void Enter()
    {
        _time = _timeToWait;
        GameManager.Instance.SetTextCountDown((int)_time + 1);
        GameManager.Instance.SetTextCountDownActive(true);
        GameManager.Instance.SetBallActive(false);
    }

    public void Process(float dt)
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.StateMachine.PushState(GameManager.Instance.PauseState);
        }
        
        if (_time <= 0.0f)
        {
            GameManager.Instance.StateMachine.PopState();
        }
        _time -= dt;
        GameManager.Instance.SetTextCountDown((int)_time + 1);
    }

    public void Exit()
    {
        GameManager.Instance.SetBallActive(true);
        GameManager.Instance.ResetBall();
        GameManager.Instance.SetTextCountDownActive(false);
    }
}
