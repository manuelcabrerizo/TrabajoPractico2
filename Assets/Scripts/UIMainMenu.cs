using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    //================================================================
    // Panels
    //================================================================
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;
    //================================================================
    // Main menu buttons
    //================================================================
    [Header("Main menu buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;
    //================================================================
    // Setting menu buttons and widgets
    //================================================================
    [Header("Settings menu buttons")]
    [SerializeField] private Button settingsBackButton;
    //================================================================
    // Credits buttons and widgets
    //================================================================
    [Header("Credits menu buttons")]
    [SerializeField] private Button creditsBackButton;
    
    
    private void Awake()
    {
        //================================================================
        // Initialize main menu buttons callbacks
        //================================================================
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);
        //================================================================
        // Initialize settings buttons callbacks
        //================================================================
        settingsBackButton.onClick.AddListener(OnSettingsBackButtonClicked);
        //================================================================
        // Initialize credits buttons callbacks
        //================================================================
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);
        
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf || settingsPanel.activeSelf || creditsPanel.activeSelf)
            {
                pausePanel.SetActive(false);
                settingsPanel.SetActive(false);
                creditsPanel.SetActive(false);
            }
            else
            {
                pausePanel.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
    }
    //================================================================
    // Main Menu callbacks implementation
    //================================================================
    private void OnPlayButtonClicked()
    {
        pausePanel.SetActive(false);
        Debug.Log("onPlayButtonClicked()");
    }

    private void OnSettingsButtonClicked()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    
    private void OnCreditsButtonClicked()
    {
        pausePanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
    
    private void OnExitButtonClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    
    //================================================================
    // Settings Menu callbacks implementation
    //================================================================
    private void OnSettingsBackButtonClicked()
    {
        settingsPanel.SetActive(false);
    }
    
    //================================================================
    // Credits Menu callbacks implementation
    //================================================================
    private void OnCreditsBackButtonClicked()
    {
        creditsPanel.SetActive(false);
    }
    
    //================================================================
    // Getters and Setters
    //================================================================
    public bool IsActive()
    {
        return pausePanel.activeSelf || settingsPanel.activeSelf || creditsPanel.activeSelf;
    }
}
