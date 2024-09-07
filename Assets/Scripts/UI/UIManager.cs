using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Since we have a single UIManager, we are going to use the singleton pattern
    public static UIManager Instance;
    
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private GameObject creditsMenuPanel;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CloseAll();
    }
    
    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }
    
    public void OpenPauseMenu()
    {
        pauseMenuPanel.SetActive(true);
    }
    
    public void OpenSettingsMenu()
    {
        settingsMenuPanel.SetActive(true);
    }

    public void OpenCreditsMenu()
    {
        creditsMenuPanel.SetActive(true);
    }
    
    public void CloseMainMenu()
    {   
        mainMenuPanel.SetActive(false);
    }
    
    public void ClosePauseMenu()
    {   
        pauseMenuPanel.SetActive(false);
    }

    public void CloseSettingsMenu()
    {
        settingsMenuPanel.SetActive(false);
    }

    public void CloseCreditsMenu()
    {
        creditsMenuPanel.SetActive(false);
    }

    public void CloseAll()
    {
        CloseMainMenu();
        ClosePauseMenu();
        CloseSettingsMenu();
        CloseCreditsMenu();
    }

}