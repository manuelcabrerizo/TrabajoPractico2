using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    private StateMachine _powerUpsStateMachine;
    private PlayerHeightPowerUpState _playerHeightPowerUpState;
    private PlayerShootPowerUpState _playerShootPowerUpState;
    
    public StateMachine PowerUpsStateMachine => _powerUpsStateMachine;

    
    private void Awake()
    {
        _powerUpsStateMachine = new StateMachine();
        _playerHeightPowerUpState = GetComponent<PlayerHeightPowerUpState>();
        _playerShootPowerUpState = GetComponent<PlayerShootPowerUpState>();
    }
    
    private void Update()
    {
        if (!_powerUpsStateMachine.IsEmpty())
        {
            _powerUpsStateMachine.Update(Time.deltaTime);
        }
    }

    private void GrabPowerUp(PowerUpType type)
    {
        if (_powerUpsStateMachine.IsEmpty())
        {
            switch (type)
            {
                case PowerUpType.Height:
                {
                    _powerUpsStateMachine.PushState(_playerHeightPowerUpState);
                }
                break;
                case PowerUpType.Shoot:
                {
                    _powerUpsStateMachine.PushState(_playerShootPowerUpState);
                }
                break;
            }
        }
    }
}
