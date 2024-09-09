using UnityEngine;
using UnityEngine.UI;

public class UIGameOverMenu : MonoBehaviour
{
    [Header("GameOver menu buttons")]
    [SerializeField] private Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
    }
    
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClicked()
    {
        GameManager.Instance.StateMachine.ChangeState(GameManager.Instance.MainMenuState);
    }
}
