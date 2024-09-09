using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    // Since we have a single GameManager, we are going to use the singleton pattern
    public static GameManager Instance;

    private StateMachine _stateMachine;
    private MainMenuState _mainMenuState;
    private PlayState _playState;
    private PauseState _pauseState;
    private ScoreState _scoreState;
    private WinState _winState;
    
    public StateMachine StateMachine => _stateMachine;
    public MainMenuState MainMenuState => _mainMenuState;
    public PlayState PlayState => _playState;
    public PauseState PauseState => _pauseState;

    public ScoreState ScoreState => _scoreState;
    public WinState WinState => _winState;
    
    public static float PlayerMinHeight = 1.0f;
    public static float PlayerMaxHeight = 4.0f;
    public int WinningScore = 7;

    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private BallMovement ball;
    [SerializeField] private PowerUpSpawner powerUpSpawner;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private TextMeshProUGUI textPlayerScore1;
    [SerializeField] private TextMeshProUGUI textPlayerScore2;
    [SerializeField] private TextMeshProUGUI textCountDown;

    private SpriteRenderer _ballSpriteRenderer;
    private PlayerPowerUps _player1PowerUps;
    private PlayerPowerUps _player2PowerUps;
    
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
        _scoreState = new ScoreState();
        _winState = new WinState();

        _ballSpriteRenderer = ball.GetComponent<SpriteRenderer>();
        _player1PowerUps = player1.GetComponent<PlayerPowerUps>();
        _player2PowerUps = player2.GetComponent<PlayerPowerUps>();

    }

    private void Start()
    {
        _stateMachine.PushState(_mainMenuState);
    }
    
    private void Update()
    {
        _stateMachine.Update(Time.deltaTime);
    }


    public void SetTextPlayer1Score(int score)
    {
        textPlayerScore1.text = score.ToString();
    }
    
    public void SetTextPlayer2Score(int score)
    {
        textPlayerScore2.text = score.ToString();
    }
    
    public void SetTextPlayerScoreActive(bool value)
    {
        textPlayerScore1.enabled = value;
        textPlayerScore2.enabled = value;
    }
    
    public void ResetObjectsForNewGame()
    {
        // Reset the player position
        Vector2 position = player1.transform.position;
        position.y = 0;
        player1.transform.position = position;
        position = player2.transform.position;
        position.y = 0;
        player2.transform.position = position;

        // Reset the ball position and velocity
        ball.Reset();
        // Reset the spawners
        obstacleSpawner.Clear();
        powerUpSpawner.Clear();
    }
    
    public void SetTextCountDown(int value)
    {
        textCountDown.text = value.ToString();
    }

    public void SetTextCountDownActive(bool value)
    {
        textCountDown.enabled = value;
    }

    public void SetBallActive(bool value)
    {
        ball.Stop();
        _ballSpriteRenderer.enabled = value;
    }

    public void ResetBall()
    {
        ball.Reset();
    }

    public void ResetPlayerPowerUps()
    {
        _player1PowerUps.PowerUpsStateMachine.Clear();
        _player2PowerUps.PowerUpsStateMachine.Clear();
    }

}
