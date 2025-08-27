using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Panels : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private PlayerMovement player1Speed;
    [SerializeField] private Transform player1Size;
    //private Color player1Color;
    private PlayerMovement player2Speed;
    [SerializeField] private Transform player2Size;
    //private Color player2Color;

    [Header("Player 1 settings")]
    [SerializeField] private Slider sliderPlayer1Speed;
    [SerializeField] private Slider sliderPlayer1Size;
    [SerializeField] private Slider sliderPlayer1Color;

    [Header("Player 2 settings")]
    [SerializeField] private Slider sliderPlayer2Speed;
    [SerializeField] private Slider sliderPlayer2Size;
    [SerializeField] private Slider sliderPlayer2Color;

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
    [SerializeField] private TextMeshProUGUI textSpeedPlayer2;
    [SerializeField] private TextMeshProUGUI textSizePlayer2;

    //Color
    //[SerializeField] private TextMeshProUGUI textColorPlayer1;
    //[SerializeField] private TextMeshProUGUI textColorPlayer2;
    
    private bool isPause = false;
    private bool isOptions = false;
    public string MainMenu;

    private void Awake()
    {
        player1Speed = player1.GetComponent<PlayerMovement>();
        player1Size = player1.GetComponent<Transform>();

        player2Speed = player2.GetComponent<PlayerMovement>();
        player2Size = player2.GetComponent<Transform>();

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

}
