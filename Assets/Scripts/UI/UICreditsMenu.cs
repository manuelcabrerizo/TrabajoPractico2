using UnityEngine;
using UnityEngine.UI;

public class UICreditsMenu : MonoBehaviour
{
    [Header("Credits menu buttons")]
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
        UIManager.Instance.CloseCreditsMenu();
        UIManager.Instance.OpenMainMenu();
    }
}
