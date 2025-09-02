using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{
    [Header("Borders")]
    [SerializeField] private GameObject goalLeft;
    [SerializeField] private GameObject goalRight;
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject floor;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Ball")]
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    public float initialBallSpeed = 0.4f;
    public float ballExtraSpeed = 0.01f;
    public float currentBallSpeed;

    [Header("Score settings")]
    [SerializeField] private TextMeshProUGUI textScoreP1;
    [SerializeField] private TextMeshProUGUI textScoreP2;
    public int maxScore;
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    [Header("Win Panels")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;
    [SerializeField] private TextMeshProUGUI scoreWinP1;
    [SerializeField] private TextMeshProUGUI scoreWinP2;
    [SerializeField] private GameObject winP1;
    [SerializeField] private GameObject winP2;


    [Header("Kick Off Timer")]
    public float kickOffTimer = 3f;
    private float kickOffEndTimer = 0f;
    private bool kickOffTimerIsTiming = false;
    public float matchDuration = 20f;
    [SerializeField] private TextMeshProUGUI matchTime;

    [Header("Power Ups")]
    [SerializeField] private Extras extras;
    [SerializeField] private GameObject shieldP1;
    [SerializeField] private GameObject shieldP2;
    public float powerUpsUsageTime;
    public float ballFoodSpeed;
    private float powerUpsDeath = 0f;
    private bool isShieldUsed = false;

    private Collider2D player1Collider;
    private Collider2D player2Collider;
    private Collider2D wallLeftCollider;
    private Collider2D wallRightCollider;
    private Collider2D roofCollider;
    private Collider2D floorCollider;
    private Collider2D shieldCollider;
    private Collider2D foodCollider;

    //Timer for the goals
    private float goalTimer;
    private Vector2 ballPosition;
    private bool isTheMatchOn = false;

    private void Awake()
    {
        Time.timeScale = 1;

        //Collider settlement
        wallLeftCollider = goalLeft.GetComponent<Collider2D>();
        wallRightCollider = goalRight.GetComponent<Collider2D>();
        roofCollider = roof.GetComponent<Collider2D>();
        floorCollider = floor.GetComponent<Collider2D>();
        player1Collider = player1.GetComponent<Collider2D>();
        player2Collider = player2.GetComponent<Collider2D>();

        kickOffEndTimer = kickOffTimer;
        currentBallSpeed = initialBallSpeed;

        shieldCollider = extras.shield.GetComponent<Collider2D>();
        foodCollider = extras.food.GetComponent<Collider2D>();

        playAgain.onClick.AddListener(OnPlayAgainClicked);
        mainMenu.onClick.AddListener(OnMainMenuClicked);

        ballPosition = ball.GetComponent<Transform>().position;
        goalTimer = matchDuration;
    }

    void Start()
    {
        Timer();
    }

    private void Update()
    {
        WriteScore();
        OnWin();
        Shield();
        TimerForGoals();

        if (kickOffTimerIsTiming)
        {
            kickOffTimer -= Time.deltaTime;
            if (kickOffTimer < 0)
            {
                KickOff();
                kickOffTimer = kickOffEndTimer;
                kickOffTimerIsTiming = false;
                isTheMatchOn = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collisions(collision);
        PowerUps(collision);
    }

    private void OnDestroy()
    {
        playAgain.onClick.RemoveAllListeners();
        mainMenu.onClick.RemoveAllListeners();
    }

    public void KickOff()
    {
        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f) //UP-RIGHT
            {
                ballRigidbody2D.AddForce(initialBallSpeed * new Vector2(1, Random.value).normalized, ForceMode2D.Impulse);
            }
            else //DOWN-RIGHT
            {
                ballRigidbody2D.AddForce(initialBallSpeed * new Vector2(1, -Random.value).normalized, ForceMode2D.Impulse);
            }
        }
        else
        {
            if (Random.value < 0.5f) //UP-LEFT
            {
                ballRigidbody2D.AddForce(initialBallSpeed * new Vector2(-1, Random.value).normalized, ForceMode2D.Impulse);
            }
            else //DOWN-LEFT
            {
                ballRigidbody2D.AddForce(initialBallSpeed * new Vector2(-1, -Random.value).normalized, ForceMode2D.Impulse);
            }
        }
    }

    public void Collisions(Collision2D collision)
    {
        if (collision.collider == wallLeftCollider)
        {
            ball.transform.position = Vector2.zero;
            ballRigidbody2D.velocity = Vector2.zero;
            scorePlayer2++;
            isTheMatchOn = false;
            matchTime.color = Color.black;
            goalTimer = matchDuration;
            Timer();
        }

        if (collision.collider == wallRightCollider)
        {
            ball.transform.position = Vector2.zero;
            ballRigidbody2D.velocity = Vector2.zero;
            scorePlayer1++;
            isTheMatchOn = false;
            matchTime.color = Color.black;
            goalTimer = matchDuration;
            Timer();
        }

        if (collision.collider == roofCollider)
        {
            if (ballRigidbody2D.velocity.x > 0)
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(1, -1).normalized, ForceMode2D.Impulse);
            }
            else
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(-1, -1).normalized, ForceMode2D.Impulse);
            }
        }

        if (collision.collider == floorCollider)
        {
            if (ballRigidbody2D.velocity.x > 0)
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(1, 1).normalized, ForceMode2D.Impulse);
            }
            else
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(-1, 1).normalized, ForceMode2D.Impulse);
            }
        }

        if (collision.collider == player1Collider)
        {
            if (ballRigidbody2D.velocity.y > 0)
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(1, 1).normalized, ForceMode2D.Impulse);
            }
            else
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(1, -1).normalized, ForceMode2D.Impulse);
            }
        }

        if (collision.collider == player2Collider)
        {
            if (ballRigidbody2D.velocity.y > 0)
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(-1, 1).normalized, ForceMode2D.Impulse);
            }
            else
            {
                ballRigidbody2D.AddForce(ballExtraSpeed * new Vector2(-1, -1).normalized, ForceMode2D.Impulse);
            }
        }
    }

    public void WriteScore()
    {
        textScoreP1.text = scorePlayer1.ToString("0");
        textScoreP2.text = scorePlayer2.ToString("0");
    }

    public void OnWin()
    {
        if (scorePlayer1 >= maxScore || scorePlayer2 >= maxScore)
        {
            Time.timeScale = 0;
            panelWin.SetActive(true);
            scoreWinP1.text = scorePlayer1.ToString("0");
            scoreWinP2.text = scorePlayer2.ToString("0");

            if (scorePlayer1 >= maxScore)
            {
                winP1.SetActive(true);
            }
            else
            {
                winP2.SetActive(true);
            }
        }
    }

    public void Timer()
    {
        ballRigidbody2D.velocity = Vector3.zero;
        kickOffTimerIsTiming = true;
    }

    public void TimerForGoals()
    {
        matchTime.text = goalTimer.ToString("0");
        if (isTheMatchOn)
        {
            goalTimer -= Time.deltaTime;
            if (goalTimer < 5.5) //Turns the font to RED when there are 5 secs lasting
            {
                matchTime.color = Color.red;
            }
        }
        if (goalTimer < 0)
        {
            if (ballPosition.x < 0)
            {
                ball.transform.position = Vector2.zero;
                ballRigidbody2D.velocity = Vector2.zero;
                scorePlayer2++;
                Timer();
            }
            else
            {
                ball.transform.position = Vector2.zero;
                ballRigidbody2D.velocity = Vector2.zero;
                scorePlayer1++;
                Timer();
            }
            matchTime.color = Color.black;
            isTheMatchOn = false;
            goalTimer = matchDuration;
        }
    }

    public void PowerUps(Collision2D collision)
    {
        if (collision.collider == foodCollider)
        {
            extras.food.SetActive(false);
            currentBallSpeed += ballFoodSpeed;
            Invoke("FastBall", powerUpsUsageTime);
        }
        if (collision.collider == shieldCollider)
        {
            extras.shield.SetActive(false);
            isShieldUsed = true;
            if (ballRigidbody2D.velocityX > 0)
            {
                shieldP1.SetActive(true);
            }
            if (ballRigidbody2D.velocityX < 0)
            {
                shieldP2.SetActive(true);
            }
        }
    }

    public void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void FastBall()
    {
        if (currentBallSpeed == initialBallSpeed)
        { }
        else
        {
            ballRigidbody2D.AddForce(new Vector2(ballExtraSpeed, ballExtraSpeed).normalized, ForceMode2D.Impulse);
        }
    }

    public void Shield()
    {
        if (isShieldUsed)
        {
            powerUpsDeath += Time.deltaTime;
            if (powerUpsDeath > powerUpsUsageTime)
            {
                shieldP1.SetActive(false);
                shieldP2.SetActive(false);
                isShieldUsed = false;
                powerUpsDeath = 0f;
            }
        }
    }
}
