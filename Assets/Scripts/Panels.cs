using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panels : MonoBehaviour
{
    private PlayerMovement player1Speed;
    private Transform player1Size;

    private PlayerMovement player2Speed;
    private Transform player2Size;

    private SpriteRenderer player1Color;
    private SpriteRenderer player2Color;
    private PlayerMovement player1ColorSaved;
    private PlayerMovement player2ColorSaved;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [Header("Player 1 settings")]
    [SerializeField] private Slider sliderPlayer1Speed;
    [SerializeField] private Slider sliderPlayer1Size;
    [SerializeField] private Slider sliderPlayer1ColorR;
    [SerializeField] private Slider sliderPlayer1ColorG;
    [SerializeField] private Slider sliderPlayer1ColorB;

    [Header("Player 2 settings")]
    [SerializeField] private Slider sliderPlayer2Speed;
    [SerializeField] private Slider sliderPlayer2Size;
    [SerializeField] private Slider sliderPlayer2ColorR;
    [SerializeField] private Slider sliderPlayer2ColorG;
    [SerializeField] private Slider sliderPlayer2ColorB;

    [Header("Panel Settings")]
    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelOptions;

    [Header("Button Settings")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnMainMenu;
    [SerializeField] private Button btnBackOptions;

    [Header("Text Settings")]
    [Header("Player 1")]
    [SerializeField] private TextMeshProUGUI textSpeedPlayer1;
    [SerializeField] private TextMeshProUGUI textSizePlayer1;
    [SerializeField] private TextMeshProUGUI textColorPlayer1R;
    [SerializeField] private TextMeshProUGUI textColorPlayer1G;
    [SerializeField] private TextMeshProUGUI textColorPlayer1B;

    [Header("Player 2")]
    [SerializeField] private TextMeshProUGUI textSpeedPlayer2;
    [SerializeField] private TextMeshProUGUI textSizePlayer2;
    [SerializeField] private TextMeshProUGUI textColorPlayer2R;
    [SerializeField] private TextMeshProUGUI textColorPlayer2G;
    [SerializeField] private TextMeshProUGUI textColorPlayer2B;

    [Header("Extras")]
    public string MainMenu = "MainMenu";
    public string Game = "Game";

    private bool isPause = false;
    private bool isOptions = false;

    private void Awake()
    {
        player1Speed = player1.GetComponent<PlayerMovement>();
        player1Size = player1.GetComponent<Transform>();
        player1Color = player1.GetComponent<SpriteRenderer>();

        player2Speed = player2.GetComponent<PlayerMovement>();
        player2Size = player2.GetComponent<Transform>();
        player2Color = player2.GetComponent<SpriteRenderer>();

        btnPlay.onClick.AddListener(Resume);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnMainMenu.onClick.AddListener(OnMainMenuClicked);
        btnBackOptions.onClick.AddListener(OnBackOptionsClicked);

        ChangeSpeed();
        ChangeSize();
        ChangeColor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false && isOptions == false)
            {
                Pause();
            }
            else
            {
                if (isOptions == false)
                {
                    Resume();
                }
            }
        }
    }

    private void OnDestroy()
    {
        //Buttons
        btnPlay.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
        btnBackOptions.onClick.RemoveAllListeners();

        //P1
        sliderPlayer1Speed.onValueChanged.RemoveAllListeners();
        sliderPlayer1Size.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorR.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorG.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorB.onValueChanged.RemoveAllListeners();

        //P2
        sliderPlayer2Speed.onValueChanged.RemoveAllListeners();
        sliderPlayer2Size.onValueChanged.RemoveAllListeners();
        sliderPlayer2ColorR.onValueChanged.RemoveAllListeners();
        sliderPlayer2ColorG.onValueChanged.RemoveAllListeners();
        sliderPlayer2ColorB.onValueChanged.RemoveAllListeners();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPause = true;
        panelPause.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        isPause = false;
        panelPause.SetActive(false);
        panelOptions.SetActive(false);
    }

    public void OnOptionsClicked()
    {
        panelPause.SetActive(false);
        panelOptions.SetActive(true);
        isOptions = true;
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene(MainMenu);
    }

    public void OnBackOptionsClicked()
    {
        panelOptions.SetActive(false);
        panelPause.SetActive(true);
        isOptions = false;
    }

    public void ChangeSpeed()
    {
        sliderPlayer1Speed.onValueChanged.AddListener(OnSlidePlayer1SpeedChanged);
        sliderPlayer2Speed.onValueChanged.AddListener(OnSlidePlayer2SpeedChanged);
    }

    public void OnSlidePlayer1SpeedChanged(float speed)
    {
        player1Speed.speed = speed;
        textSpeedPlayer1.text = speed.ToString("0");
    }
    
    public void OnSlidePlayer2SpeedChanged(float speed)
    {
        player2Speed.speed = speed;
        textSpeedPlayer2.text = speed.ToString("0");
    }

    public void ChangeSize()
    {
        sliderPlayer1Size.onValueChanged.AddListener(OnSlidePlayer1SizeChanged);
        sliderPlayer2Size.onValueChanged.AddListener(OnSlidePlayer2SizeChanged);
    }

    public void OnSlidePlayer1SizeChanged(float size)
    {
        player1Size.transform.localScale = new Vector3(11, size, 11);
        textSizePlayer1.text = size.ToString("0");
    }

    public void OnSlidePlayer2SizeChanged(float size)
    {
        player2Size.transform.localScale = new Vector3(11, size, 11);
        textSizePlayer2.text = size.ToString("0");
    }

    public void ChangeColor()
    {        
        //P1
        sliderPlayer1ColorR.onValueChanged.AddListener(OnSlidePlayer1ColorRChanged);
        sliderPlayer1ColorG.onValueChanged.AddListener(OnSlidePlayer1ColorGChanged);
        sliderPlayer1ColorB.onValueChanged.AddListener(OnSlidePlayer1ColorBChanged);
        //P2
        sliderPlayer2ColorR.onValueChanged.AddListener(OnSlidePlayer2ColorRChanged);
        sliderPlayer2ColorG.onValueChanged.AddListener(OnSlidePlayer2ColorGChanged);
        sliderPlayer2ColorB.onValueChanged.AddListener(OnSlidePlayer2ColorBChanged);
    }

    public void OnSlidePlayer1ColorRChanged(float red)
    {
        player1Color.color = new Color(red, player1Color.color.g, player1Color.color.b);
        textColorPlayer1R.text = (red * 255).ToString("0");
    }

    public void OnSlidePlayer1ColorGChanged(float green)
    {
        player1Color.color = new Color(player1Color.color.r, green, player1Color.color.b);
        textColorPlayer1G.text = (green * 255).ToString("0");
    }

    public void OnSlidePlayer1ColorBChanged(float blue)
    {
        player1Color.color = new Color(player1Color.color.r, player1Color.color.g, blue);
        textColorPlayer1B.text = (blue * 255).ToString("0");
    }

    public void OnSlidePlayer2ColorRChanged(float red)
    {
        player2Color.color = new Color(red, player2Color.color.g, player2Color.color.b);
        textColorPlayer2R.text = (red * 255).ToString("0");
    }

    public void OnSlidePlayer2ColorGChanged(float green)
    {
        player2Color.color = new Color(player2Color.color.r, green, player2Color.color.b);
        textColorPlayer2G.text = (green * 255).ToString("0");
    }

    public void OnSlidePlayer2ColorBChanged(float blue)
    {
        player2Color.color = new Color(player2Color.color.r, player2Color.color.g, blue);
        textColorPlayer2B.text = (blue * 255).ToString("0");
    }
}
