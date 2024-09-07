using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsMenu : MonoBehaviour
{
    [Header("Settings menu buttons")]
    [SerializeField] private Button backButton;

    [Header("Settings speed sliders")] 
    [SerializeField] private Slider sliderSpeedPlayer1;
    [SerializeField] private Slider sliderSpeedPlayer2;

    [Header("Players speed text")] 
    [SerializeField] private TextMeshProUGUI textSpeed1;
    [SerializeField] private TextMeshProUGUI textSpeed2;
    
    [Header("Settings height sliders")] 
    [SerializeField] private Slider sliderHeightPlayer1;
    [SerializeField] private Slider sliderHeightPlayer2;

    [Header("Players height text")] 
    [SerializeField] private TextMeshProUGUI textHeight1;
    [SerializeField] private TextMeshProUGUI textHeight2;
    
    [Header("Settings color sliders")] 
    [SerializeField] private Slider sliderColorRedPlayer1;
    [SerializeField] private Slider sliderColorGreenPlayer1;
    [SerializeField] private Slider sliderColorBluePlayer1;
    [SerializeField] private Slider sliderColorRedPlayer2;
    [SerializeField] private Slider sliderColorGreenPlayer2;
    [SerializeField] private Slider sliderColorBluePlayer2;
    
    [Header("Players color text")] 
    [SerializeField] private TextMeshProUGUI textRed1;
    [SerializeField] private TextMeshProUGUI textGreen1;
    [SerializeField] private TextMeshProUGUI textBlue1;
    [SerializeField] private TextMeshProUGUI textRed2;
    [SerializeField] private TextMeshProUGUI textGreen2;
    [SerializeField] private TextMeshProUGUI textBlue2;

    [Header("Players color images")] 
    [SerializeField] private Image colorImage1;
    [SerializeField] private Image colorImage2;
    
    [Header("Players game object")] 
    [SerializeField] private PlayerMovement player1;
    [SerializeField] private PlayerMovement player2;
    private Transform _transform1;
    private Transform _transform2;
    private SpriteRenderer _spriteRenderer1;
    private SpriteRenderer _spriteRenderer2;
    
    private void Awake()
    {
        backButton.onClick.AddListener(OnBackButtonClicked);
        // Speed
        sliderSpeedPlayer1.onValueChanged.AddListener(OnSliderSpeedPlayer1ValueChange);
        sliderSpeedPlayer2.onValueChanged.AddListener(OnSliderSpeedPlayer2ValueChange);
        textSpeed1.text = sliderSpeedPlayer1.value.ToString();
        textSpeed2.text = sliderSpeedPlayer2.value.ToString();
        // Height
        sliderHeightPlayer1.onValueChanged.AddListener(OnSliderHeightPlayer1ValueChange);
        sliderHeightPlayer2.onValueChanged.AddListener(OnSliderHeightPlayer2ValueChange);

        // Color
        sliderColorRedPlayer1.onValueChanged.AddListener(OnSliderColorRedPlayer1ValueChange);
        sliderColorGreenPlayer1.onValueChanged.AddListener(OnSliderColorGreenPlayer1ValueChange);
        sliderColorBluePlayer1.onValueChanged.AddListener(OnSliderColorBluePlayer1ValueChange);
        sliderColorRedPlayer2.onValueChanged.AddListener(OnSliderColorRedPlayer2ValueChange);
        sliderColorGreenPlayer2.onValueChanged.AddListener(OnSliderColorGreenPlayer2ValueChange);
        sliderColorBluePlayer2.onValueChanged.AddListener(OnSliderColorBluePlayer2ValueChange);
        textRed1.text = sliderColorRedPlayer1.value.ToString();
        textGreen1.text = sliderColorGreenPlayer1.value.ToString();
        textBlue1.text = sliderColorBluePlayer1.value.ToString();
        textRed2.text = sliderColorRedPlayer2.value.ToString();
        textGreen2.text = sliderColorGreenPlayer2.value.ToString();
        textBlue2.text = sliderColorBluePlayer2.value.ToString();

        _transform1 = player1.GetComponent<Transform>();
        _transform2 = player2.GetComponent<Transform>();
        _spriteRenderer1 = player1.GetComponent<SpriteRenderer>();
        _spriteRenderer2 = player2.GetComponent<SpriteRenderer>();

        colorImage1.color = new Color(sliderColorRedPlayer1.value, sliderColorGreenPlayer1.value, sliderColorBluePlayer1.value);
        colorImage2.color = new Color(sliderColorRedPlayer2.value, sliderColorGreenPlayer2.value, sliderColorBluePlayer2.value);

        player1.SetSpeed(sliderSpeedPlayer1.value);
        player2.SetSpeed(sliderSpeedPlayer2.value);

        {
            float y = Mathf.Lerp(GameManager.PlayerMinHeight, GameManager.PlayerMaxHeight, sliderHeightPlayer1.value);
            Vector3 scale = _transform1.localScale;
            scale.y = y;
            _transform1.localScale = scale;
            textHeight1.text = y.ToString();
        }
        
        {
            float y = Mathf.Lerp(GameManager.PlayerMinHeight, GameManager.PlayerMaxHeight, sliderHeightPlayer2.value);
            Vector3 scale = _transform2.localScale;
            scale.y = y;
            _transform2.localScale = scale;
            textHeight2.text = y.ToString();
        }
    }
    
    private void OnDestroy()
    {
        backButton.onClick.RemoveAllListeners();
        // Speed
        sliderSpeedPlayer1.onValueChanged.RemoveAllListeners();
        sliderSpeedPlayer2.onValueChanged.RemoveAllListeners();
        // Height
        sliderHeightPlayer1.onValueChanged.RemoveAllListeners();
        sliderHeightPlayer2.onValueChanged.RemoveAllListeners();
        // Color
        sliderColorRedPlayer1.onValueChanged.RemoveAllListeners();
        sliderColorGreenPlayer1.onValueChanged.RemoveAllListeners();
        sliderColorBluePlayer1.onValueChanged.RemoveAllListeners();
        sliderColorRedPlayer2.onValueChanged.RemoveAllListeners();
        sliderColorGreenPlayer2.onValueChanged.RemoveAllListeners();
        sliderColorBluePlayer2.onValueChanged.RemoveAllListeners();
    }

    //  Speed
    private void OnSliderSpeedPlayer1ValueChange(float value)
    {
        textSpeed1.text = value.ToString();
        player1.SetSpeed(value);
    }
    
    private void OnSliderSpeedPlayer2ValueChange(float value)
    {
        textSpeed2.text = value.ToString();
        player2.SetSpeed(value);
    }
    
    // Height
    private void OnSliderHeightPlayer1ValueChange(float value)
    {
        float y = Mathf.Lerp(GameManager.PlayerMinHeight, GameManager.PlayerMaxHeight, value);
        Vector3 scale = _transform1.localScale;
        scale.y = y;
        _transform1.localScale = scale;
        textHeight1.text = y.ToString();
    }
    
    private void OnSliderHeightPlayer2ValueChange(float value)
    {
        float y = Mathf.Lerp(GameManager.PlayerMinHeight, GameManager.PlayerMaxHeight, value);
        Vector3 scale = _transform2.localScale;
        scale.y = y;
        _transform2.localScale = scale;
        textHeight2.text = y.ToString();
    }
    
    // Color
    private void OnSliderColorRedPlayer1ValueChange(float value)
    {
        Color color = _spriteRenderer1.color;
        color.r = value;
        _spriteRenderer1.color = color;
        textRed1.text = value.ToString();
        colorImage1.color = new Color(sliderColorRedPlayer1.value, sliderColorGreenPlayer1.value, sliderColorBluePlayer1.value);

    }
    private void OnSliderColorGreenPlayer1ValueChange(float value)
    {
        Color color = _spriteRenderer1.color;
        color.g = value;
        _spriteRenderer1.color = color;
        textGreen1.text = value.ToString();
        colorImage1.color = new Color(sliderColorRedPlayer1.value, sliderColorGreenPlayer1.value, sliderColorBluePlayer1.value);
    }
    private void OnSliderColorBluePlayer1ValueChange(float value)
    {
        Color color = _spriteRenderer1.color;
        color.b = value;
        _spriteRenderer1.color = color;
        textBlue1.text = value.ToString();
        colorImage1.color = new Color(sliderColorRedPlayer1.value, sliderColorGreenPlayer1.value, sliderColorBluePlayer1.value);
    }
    private void OnSliderColorRedPlayer2ValueChange(float value)
    {
        Color color = _spriteRenderer2.color;
        color.r = value;
        _spriteRenderer2.color = color;
        textRed2.text = value.ToString();
        colorImage2.color = new Color(sliderColorRedPlayer2.value, sliderColorGreenPlayer2.value, sliderColorBluePlayer2.value);
    }
    private void OnSliderColorGreenPlayer2ValueChange(float value)
    {
        Color color = _spriteRenderer2.color;
        color.g = value;
        _spriteRenderer2.color = color;
        textGreen2.text = value.ToString();
        colorImage2.color = new Color(sliderColorRedPlayer2.value, sliderColorGreenPlayer2.value, sliderColorBluePlayer2.value);
    }
    private void OnSliderColorBluePlayer2ValueChange(float value)
    {
        Color color = _spriteRenderer2.color;
        color.b = value;
        _spriteRenderer2.color = color;
        textBlue2.text = value.ToString();
        colorImage2.color = new Color(sliderColorRedPlayer2.value, sliderColorGreenPlayer2.value, sliderColorBluePlayer2.value);
    }

    
    private void OnBackButtonClicked()
    {
        UIManager.Instance.CloseSettingsMenu();
        if (GameManager.Instance.StateMachine.PeekState() == GameManager.Instance.MainMenuState)
        {
            UIManager.Instance.OpenMainMenu();
        }
        else
        {
            UIManager.Instance.OpenPauseMenu();
        }
    }
}
