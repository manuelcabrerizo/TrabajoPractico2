using UnityEngine;

public class PlayerHeightPowerUpState : MonoBehaviour, IState
{
    private PlayerPowerUps _playerPowerUps;

    [SerializeField] private float duration = 15;
    private float _time = 0;

    private void Awake()
    {
        _playerPowerUps = GetComponent<PlayerPowerUps>();
    }

    public void Enter()
    {
        Vector2 scale = transform.localScale;
        scale.y *= 2;
        transform.localScale = scale;

        _time = duration;
    }

    public void Process(float dt)
    {
        if (_time <= 0)
        {
            _playerPowerUps.PowerUpsStateMachine.PopState();
        }
        _time -= dt;
    }

    public void Exit()
    {
        Vector2 scale = transform.localScale;
        scale.y /= 2;
        transform.localScale = scale;
    }
}
