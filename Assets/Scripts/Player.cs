using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PowerUpType _powerUpType;
    private bool _powerUpActive;
    private float _powerUpTime;
    private float _powerUpCurrentTime;

    private Vector2 _originalScale;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _powerUpTime = 15.0f;
        _powerUpCurrentTime = 0;
        _powerUpActive = false;
    }

    private void Update()
    {
        if (_powerUpCurrentTime > 0)
        {
            switch (_powerUpType)
            {
                case PowerUpType.Height:
                {
                    ProcessHeightPowerUp();
                } break;
                case PowerUpType.Shoot:
                {
                    ProcessShootPowerUp();
                } break;
            }
            _powerUpCurrentTime -= Time.deltaTime;
        }
        else
        {
            if (_powerUpActive)
            {
                switch (_powerUpType)
                {
                    case PowerUpType.Height:
                    {
                        ClearHeightPowerUp();
                    } break;
                }
                _powerUpActive = false;
            }
        }
    }

    private void ProcessHeightPowerUp()
    {
        Vector2 newScale = new Vector2(_originalScale.x, _originalScale.y * 3.0f);
        transform.localScale = newScale;
    }

    private void ClearHeightPowerUp()
    {
        Vector2 newScale = new Vector2(_originalScale.x, _originalScale.y);
        transform.localScale = newScale;
    }

    private void ProcessShootPowerUp()
    {
    }
    
    private void GrabPowerUp(PowerUpType type)
    {
        _powerUpType = type;
        _powerUpCurrentTime = _powerUpTime;
        _powerUpActive = true;
    }

}
