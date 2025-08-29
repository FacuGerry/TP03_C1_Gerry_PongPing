using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panels : MonoBehaviour
{
    private PlayerMovement player1Speed;
    private Transform player1Size;

    private PlayerMovement player2Speed;
    private Transform player2Size;

    private Color player1ColorR;
    private Color player1ColorG;
    private Color player1ColorB;

    private Color player2ColorR;
    private Color player2ColorG;
    private Color player2ColorB;

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
    [SerializeField] private TextMeshProUGUI textSpeedPlayer1;
    [SerializeField] private TextMeshProUGUI textSizePlayer1;

    [SerializeField] private TextMeshProUGUI textColorPlayer1R;
    [SerializeField] private TextMeshProUGUI textColorPlayer1G;
    [SerializeField] private TextMeshProUGUI textColorPlayer1B;

    [SerializeField] private TextMeshProUGUI textSpeedPlayer2;
    [SerializeField] private TextMeshProUGUI textSizePlayer2;

    [SerializeField] private TextMeshProUGUI textColorPlayer2R;
    [SerializeField] private TextMeshProUGUI textColorPlayer2G;
    [SerializeField] private TextMeshProUGUI textColorPlayer2B;

    private bool isPause = false;
    private bool isOptions = false;
    public string MainMenu = "MainMenu";
    public string Game = "Game";

    private void Awake()
    {
        player1Speed = player1.GetComponent<PlayerMovement>();
        player1Size = player1.GetComponent<Transform>();
        player1ColorR = player1.GetComponent<SpriteRenderer>().color;
        player1ColorG = player1.GetComponent<SpriteRenderer>().color;
        player1ColorB = player1.GetComponent<SpriteRenderer>().color;

        player2Speed = player2.GetComponent<PlayerMovement>();
        player2Size = player2.GetComponent<Transform>();
        player2ColorR = player2.GetComponent<SpriteRenderer>().color;
        player2ColorG = player2.GetComponent<SpriteRenderer>().color;
        player2ColorB = player2.GetComponent<SpriteRenderer>().color;

        btnPlay.onClick.AddListener(Resume);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnMainMenu.onClick.AddListener(OnMainMenuClicked);
        btnBackOptions.onClick.AddListener(OnBackOptionsClicked);
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

        ChangeSpeed();
        ChangeSize();


    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnOptions.onClick.RemoveAllListeners();
        btnMainMenu.onClick.RemoveAllListeners();
        btnBackOptions.onClick.RemoveAllListeners();

        sliderPlayer1Speed.onValueChanged.RemoveAllListeners();
        sliderPlayer1Size.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorR.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorG.onValueChanged.RemoveAllListeners();
        sliderPlayer1ColorB.onValueChanged.RemoveAllListeners();

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
        sliderPlayer1Speed.onValueChanged.AddListener((speed) =>
        {
            player1Speed.speed = speed;
            textSpeedPlayer1.text = speed.ToString("0");

        });

        sliderPlayer2Speed.onValueChanged.AddListener((speed) =>
        {
            player2Speed.speed = speed;
            textSpeedPlayer2.text = speed.ToString("0");

        });

    }

    public void ChangeSize()
    {
        sliderPlayer1Size.onValueChanged.AddListener((size) =>
        {
            player1Size.transform.localScale = new Vector3(11, size, 11);
            textSizePlayer1.text = size.ToString("0");

        });

        sliderPlayer2Size.onValueChanged.AddListener((size) =>
        {
            player2Size.transform.localScale = new Vector3(11, size, 11);
            textSizePlayer2.text = size.ToString("0");

        });
    }

    public void ChangeColor()
    {
        sliderPlayer1ColorR.onValueChanged.AddListener((red) =>
        {
            red = player1ColorR.r;
            //GetComponent<SpriteRenderer>().color = Random.ColorHSV();


        });



    }

}
