using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    //[SerializeField] private GameObject init;
    [SerializeField] private Button btnPlay;
    //[SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnExit;
    [SerializeField] private Camera myCamera;

    private Transform myTransCam;

    public string Game = "Game";
    public float camSpeed = 5f;
    public float camMaxHeight = 11f;

    private void Awake()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        //btnOptions.onClick.AddListener(OnOptsClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);

        myTransCam = myCamera.GetComponent<Transform>();
    }

    private void OnDestroy()
    {
        btnPlay.onClick.RemoveAllListeners();
    }

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(Game);
    }

    //public void OnOptsClicked(){}

    public void OnCreditsClicked()
    {
        bool isMovingDone = false;
        float camMoving = camSpeed * myTransCam.position.y;

        while (isMovingDone == false)
        {
            if (myTransCam.position.y < camMaxHeight)
            {
                myTransCam.transform.position = new Vector3(0, camMoving, 0);
            }
            else
            {
                isMovingDone = true;
            }
        }
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }
}
