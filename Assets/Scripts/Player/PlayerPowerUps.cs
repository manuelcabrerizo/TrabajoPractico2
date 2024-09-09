using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    // here im using a state machine to handle the different power ups
    
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
        // only update the state machine if the player have grabed a power up
        if (!_powerUpsStateMachine.IsEmpty())
        {
            _powerUpsStateMachine.Update(Time.deltaTime);
        }
    }

    private void GrabPowerUp(PowerUpType type)
    {
        // only push a new power up the player haven't have one already
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
