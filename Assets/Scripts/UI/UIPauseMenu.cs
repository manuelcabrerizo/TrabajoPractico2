using UnityEngine;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    [Header("Pause menu buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(OnResumeButtonClicked);
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }
    
    private void OnDestroy()
    {
        resumeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        mainMenuButton.onClick.RemoveAllListeners();
    }

    
    private void OnResumeButtonClicked()
    {
        GameManager.Instance.StateMachine.PopState();
    }

    private void OnRestartButtonClicked()
    {
        GameManager.Instance.StateMachine.ChangeState(GameManager.Instance.PlayState);
        GameManager.Instance.StateMachine.PushState(GameManager.Instance.ScoreState);
    }

    private void OnSettingsButtonClicked()
    {
        UIManager.Instance.ClosePauseMenu();
        UIManager.Instance.OpenSettingsMenu();
    }

    private void OnMainMenuButtonClicked()
    {
        GameManager.Instance.StateMachine.Clear();
        GameManager.Instance.StateMachine.PushState(GameManager.Instance.MainMenuState);
    }

}
