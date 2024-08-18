using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsMenu : UIMenu
{
    [Header("Settings menu buttons")]
    [SerializeField] private Button backButton;

    [Header("Settings menu sliders")] 
    [SerializeField] private Slider sliderPlayer1;
    [SerializeField] private Slider sliderPlayer2;

    [Header("Players speed text")] 
    [SerializeField] private TextMeshProUGUI speed1;
    [SerializeField] private TextMeshProUGUI speed2;
    
    
    [Header("Players game object")] 
    [SerializeField] private Movement player1;
    [SerializeField] private Movement player2;

    private void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        sliderPlayer1.onValueChanged.AddListener(OnPlayer1ValueChange);
        sliderPlayer2.onValueChanged.AddListener(OnPlayer2ValueChange);

        speed1.text = sliderPlayer1.value.ToString();
        speed2.text = sliderPlayer2.value.ToString();
    }
    
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        sliderPlayer1.onValueChanged.RemoveAllListeners();
        sliderPlayer2.onValueChanged.RemoveAllListeners();
    }

    private void OnPlayer1ValueChange(float value)
    {
        speed1.text = value.ToString();
        player1.SetSpeed(value);
    }
    
    private void OnPlayer2ValueChange(float value)
    {
        speed2.text = value.ToString();
        player2.SetSpeed(value);
    }

    private void OnBackButtonClicked()
    {
        Pause.CloseSettingsMenu();
        Pause.OpenMainMenu();
    }
}
