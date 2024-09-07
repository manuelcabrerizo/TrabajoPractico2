using System;
using System.Collections;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    [SerializeField] private float powerUpHeight = 4.0f;
    [SerializeField] private float heightTime = 15.0f;
    [SerializeField] private float shootTime = 15.0f;

    private const float OriginalHeight = 2.0f;

    private PlayerShoot _playerShoot;

    private void Awake()
    {
        _playerShoot = GetComponent<PlayerShoot>();
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
                StartCoroutine(ShootPowerUp());
            } break;
        }
    }

    private IEnumerator HeightPowerUp()
    {
        Vector2 scale = transform.localScale;  
        scale.y = powerUpHeight;
        transform.localScale = scale;
        yield return new WaitForSeconds(heightTime);
        scale.y = OriginalHeight;
        transform.localScale = scale;
    }

    // TODO:  fix this powerup 
    private IEnumerator ShootPowerUp()
    {
        _playerShoot.Active = true;
        yield return new WaitForSeconds(shootTime);
        _playerShoot.Active = false;
    }

}
