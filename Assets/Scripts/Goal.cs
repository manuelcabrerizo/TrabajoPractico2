using System;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.position.x > 0)
        {
            GameManager.Instance.PlayState.Player1Score();
        }
        else
        {
            GameManager.Instance.PlayState.Player2Score();
        }
    }
}
