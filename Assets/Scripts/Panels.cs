using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
#if USINGEDITOR
using UnityEditor;
#endif

public class Panels : MonoBehaviour
{
    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private PlayerMovement player1Speed;
    private PlayerMovement player2Speed;

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
    [SerializeField] private TextMeshPro textSpeedPlayer1;
    [SerializeField] private TextMeshPro textSizePlayer1;
    [SerializeField] private TextMeshPro textColorPlayer1;
    [SerializeField] private TextMeshPro textSpeedPlayer2;
    [SerializeField] private TextMeshPro textSizePlayer2;
    [SerializeField] private TextMeshPro textColorPlayer2;

    private bool isPause = false;
    private bool isOptions = false;

    private void Awake()
    {
        player1Speed = player1.GetComponent<PlayerMovement>();
        player2Speed = player2.GetComponent<PlayerMovement>();

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
#if USINGEDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
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
            textSpeedPlayer1.text = speed.ToString("0.00");

        });

        sliderPlayer2Speed.onValueChanged.AddListener((speed) =>
        {
            player2Speed.speed = speed;
            textSpeedPlayer2.text = speed.ToString("0.00");

        });

    }

}
