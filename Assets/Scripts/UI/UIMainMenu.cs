using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [Header("Main menu buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    
    private void Awake()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
    }
    
    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
    
    private void OnPlayButtonClicked()
    {
        GameManager.Instance.StateMachine.ChangeState(GameManager.Instance.PlayState);
        GameManager.Instance.StateMachine.PushState(GameManager.Instance.ScoreState);
    }

    private void OnSettingsButtonClicked()
    {
        UIManager.Instance.CloseMainMenu();
        UIManager.Instance.OpenSettingsMenu();
    }
    
    private void OnCreditsButtonClicked()
    {
        UIManager.Instance.CloseMainMenu();
        UIManager.Instance.OpenCreditsMenu();
    }
    
    private void OnExitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
}
