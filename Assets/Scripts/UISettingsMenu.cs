using UnityEngine;
using UnityEngine.UI;

public class UISettingsMenu : UIMenu
{
    [Header("Settings menu buttons")]
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
        uiPause.CloseSettingsMenu();
    }
}
