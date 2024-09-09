using System.Collections;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [SerializeField] private float heightTime = 15.0f;
    [SerializeField] private float shootTime = 15.0f;
    
    private PlayerShoot _playerShoot;
    private bool _heightPowerUpActive;

    private void Awake()
    {
        _playerShoot = GetComponent<PlayerShoot>();
        _heightPowerUpActive = false;
    }

    private void GrabPowerUp(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Height:
            {
                StartCoroutine(HeightPowerUp());
            } break;
            case PowerUpType.Shoot:
            {
                _playerShoot.TryActivateShoot(shootTime);
            } break;
        }
    }

    private IEnumerator HeightPowerUp()
    {
        if (!_heightPowerUpActive)
        {
            _heightPowerUpActive = true;
            Vector2 scale = transform.localScale;
            scale.y *= 1.5f;
            transform.localScale = scale;
            yield return new WaitForSeconds(heightTime);
            scale.y /= 1.5f;
            transform.localScale = scale;
            _heightPowerUpActive = false;
        }
    }
}
