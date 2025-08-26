using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private GameObject panelCredits;

    [Header("Button Settings")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnBackOptions;
    [SerializeField] private Button btnBackCredits;

    [Header("Text Settings")]
    [SerializeField] private TextMeshPro textSpeedPlayer1;
    [SerializeField] private TextMeshPro textSizePlayer1;
    [SerializeField] private TextMeshPro textColorPlayer1;
    [SerializeField] private TextMeshPro textSpeedPlayer2;
    [SerializeField] private TextMeshPro textSizePlayer2;
    [SerializeField] private TextMeshPro textColorPlayer2;

    private bool isPause = false;
    private bool isOptions = false;
    private bool isCredits = false;

    private void Awake()
    {
        player1Speed = player1.GetComponent<PlayerMovement>();
        player2Speed = player2.GetComponent<PlayerMovement>();

        btnPlay.onClick.AddListener(Resume);
        btnOptions.onClick.AddListener(OnOptionsClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnBackOptions.onClick.AddListener(OnBackOptionsClicked);
        btnBackCredits.onClick.AddListener(OnBackCreditsClicked);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause == false && isOptions == false && isCredits == false)
            {
                Pause();
            }
            else
            {
                if (isOptions == false && isCredits == false)
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
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnBackOptions.onClick.RemoveAllListeners();
        btnBackCredits.onClick.RemoveAllListeners();


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
    }

    public void OnOptionsClicked()
    {
        panelPause.SetActive(false);
        panelOptions.SetActive(true);
        isOptions = true;
    }

    public void OnCreditsClicked()
    {
        panelPause.SetActive(false);
        panelCredits.SetActive(true);
        isOptions = false;
    }

    public void OnExitClicked()
    {
        EditorApplication.ExitPlaymode();
        Application.Quit();
    }

    public void OnBackOptionsClicked()
    {
        panelOptions.SetActive(false);
        panelPause.SetActive(true);
        isOptions = false;
    }

    public void OnBackCreditsClicked()
    {
        panelCredits.SetActive(false);
        panelPause.SetActive(true);
        isCredits = false;
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
