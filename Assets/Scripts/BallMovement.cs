using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

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
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    [SerializeField] private GameObject ball;
    public float initialBallSpeed = 0.4f;
    public float currentBallSpeed = 0.4f;
    public float ballExtraSpeed = 0.01f;

    [Header("Score settings")]
    [SerializeField] private TextMeshProUGUI textScoreP1;
    [SerializeField] private TextMeshProUGUI textScoreP2;
    public int maxScore = 10;
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    [SerializeField] private GameObject panelWin;
    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;
    [SerializeField] private TextMeshProUGUI scoreWinP1;
    [SerializeField] private TextMeshProUGUI scoreWinP2;
    [SerializeField] private GameObject winP1;
    [SerializeField] private GameObject winP2;

    private Collider2D wallLeft;
    private Collider2D wallRight;
    private Collider2D rooftop;
    private Collider2D floorbot;
    private Collider2D p1;
    private Collider2D p2;

    private bool isTiming = false;
    public float timer = 3f;
    private float maxTimer = 0f;

    [SerializeField] private Extras extras;

    private Collider2D axeCollider;
    private Collider2D shieldCollider;
    private Collider2D foodCollider;

    public bool isFooding = false;

    private void Awake()
    {
        wallLeft = goalLeft.GetComponent<Collider2D>();
        wallRight = goalRight.GetComponent<Collider2D>();
        rooftop = roof.GetComponent<Collider2D>();
        floorbot = floor.GetComponent<Collider2D>();
        p1 = player1.GetComponent<Collider2D>();
        p2 = player2.GetComponent<Collider2D>();

        currentBallSpeed = initialBallSpeed;
        maxTimer = timer;

        axeCollider = extras.axe.GetComponent<Collider2D>();
        shieldCollider = extras.shield.GetComponent<Collider2D>();
        foodCollider = extras.food.GetComponent<Collider2D>();

        playAgain.onClick.AddListener(OnPlayAgainClicked);
        mainMenu.onClick.AddListener(OnMainMenuClicked);
    }

    void Start()
    {
        Timer();
    }

    private void Update()
    {
        WriteScore();
        OnWin();

        if (isTiming)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                KickOff();
                timer = maxTimer;
                isTiming = false;
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
        Rigidbody2D rigidbody2D = ballRigidbody2D;
        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, Random.value);
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, -Random.value);
            }
        }
        else
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, Random.value);
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, -Random.value);
            }
        }
    }

    public void Collisions(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = ballRigidbody2D;

        if (collision.collider == wallLeft)
        {
            ball.transform.position = Vector2.zero;
            scorePlayer2++;
            currentBallSpeed = initialBallSpeed;
            Timer();
        }

        if (collision.collider == wallRight)
        {
            ball.transform.position = Vector2.zero;
            scorePlayer1++;
            currentBallSpeed = initialBallSpeed;
            Timer();
        }

        if (collision.collider == rooftop)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, -1);
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, -1);
            }
        }

        if (collision.collider == floorbot)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, 1);
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, 1);
            }
        }

        if (collision.collider == p1)
        {
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, 1);
                currentBallSpeed += ballExtraSpeed;
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(1, -1);
                currentBallSpeed += ballExtraSpeed;
            }
        }

        if (collision.collider == p2)
        {
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, 1);
                currentBallSpeed += ballExtraSpeed;
            }
            else
            {
                rigidbody2D.velocity = currentBallSpeed * new Vector2(-1, -1);
                currentBallSpeed += ballExtraSpeed;
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
        if (scorePlayer1 >= 10 || scorePlayer2 >= 10)
        {
            Time.timeScale = 0;
            panelWin.SetActive(true);
            scoreWinP1.text = scorePlayer1.ToString("0");
            scoreWinP2.text = scorePlayer2.ToString("0");

            if (scorePlayer1 >= 10)
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
        isTiming = true;
    }

    public void PowerUps(Collision2D collision)
    {
        if (collision.collider == axeCollider)
        {
            extras.axe.SetActive(false);
            Debug.Log("IM BEING TOUCHED");
        }
        if (collision.collider == shieldCollider)
        {
            extras.shield.SetActive(false);
            Debug.Log("IM BEING TOUCHED");
        }
        if (collision.collider == foodCollider)
        {
            isFooding = true;
            extras.food.SetActive(false);
            Debug.Log("IM BEING TOUCHED");
        }
    }

    public void OnPlayAgainClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
