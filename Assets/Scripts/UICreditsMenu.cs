using UnityEngine;
using UnityEngine.UI;

public class UICreditsMenu : UIMenu
{
    [Header("Credits menu buttons")]
    [SerializeField] private Button backButton;

    private new void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(OnBackButtonClicked);
    }
    
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
    }

    private void OnBackButtonClicked()
    {
        Pause.CloseCreditsMenu();
        Pause.OpenMainMenu();
    }
}
