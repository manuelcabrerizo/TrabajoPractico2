using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIMenu
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
        uiPause.CloseMainMenu();
    }

    private void OnSettingsButtonClicked()
    {
        uiPause.CloseMainMenu();
        uiPause.OpenSettingsMenu();
    }
    
    private void OnCreditsButtonClicked()
    {
        uiPause.CloseMainMenu();
        uiPause.OpenCreditsMenu();
    }
    
    private void OnExitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
}
