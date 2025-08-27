using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject init;
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;

    public string Game = "Game";

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnOptions.onClick.AddListener(OnOptsClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
    }

    public void OnPlayClicked()
    {
        init.SetActive(false);
        SceneManager.LoadScene(Game);
    }

    public void OnOptsClicked()
    {

    }

    public void OnCreditsClicked()
    {

    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
