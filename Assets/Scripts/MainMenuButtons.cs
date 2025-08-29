using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnBackCredits;
    [SerializeField] private GameObject init;
    [SerializeField] private GameObject Credits;
    [SerializeField] private Camera myCamera;

    public string Game = "Game";

    public float camSpeed = 1f;

    public Vector3 camMaxHeight = new Vector3(0, 11, -10);

    private Transform myTransCam;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnBackCredits.onClick.AddListener(OnBackCreditsClicked);

        myTransCam = myCamera.GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
        btnCredits.onClick.RemoveAllListeners();
        btnExit.onClick.RemoveAllListeners();
        btnBackCredits.onClick.RemoveAllListeners();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(Game);
    }

    public void OnCreditsClicked()
    {
        Vector3 destination = new Vector3(0, 11, -10);
        init.SetActive(false);
        myTransCam.transform.position = Vector3.Lerp(myTransCam.position, destination, camSpeed);
        Credits.SetActive(true);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnBackCreditsClicked()
    {
        Vector3 destination = new Vector3(0, 0, -10);
        Credits.SetActive(false);
        myTransCam.transform.position = Vector3.Lerp(myTransCam.position, destination, camSpeed);
        init.SetActive(true);

    }
}
