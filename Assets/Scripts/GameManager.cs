using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Since we have a single GameManager, we are going to use the singleton pattern
    public static GameManager Instance;

    private StateMachine _stateMachine;
    private MainMenuState _mainMenuState;
    private PlayState _playState;
    private PauseState _pauseState;
    
    public StateMachine StateMachine => _stateMachine;
    public MainMenuState MainMenuState => _mainMenuState;
    public PlayState PlayState => _playState;
    public PauseState PauseState => _pauseState;
    
    public static float PlayerMinHeight = 1.0f;
    public static float PlayerMaxHeight = 4.0f;
    
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
        
        _stateMachine = new StateMachine();
        _mainMenuState = new MainMenuState();
        _playState = new PlayState();
        _pauseState = new PauseState();
        
    }

    private void Start()
    {
        _stateMachine.PushState(_mainMenuState);
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }
}
