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

    public Vector3 destination;
    public Vector3 source;

    private Transform myTransCam;
    private bool camMoving = false;
    private float time = 0f;
    [SerializeField] private float maxTime;
    public float lerp;

    private bool isCreditsEnabled = false;

    private void Awake()
    {
        Time.timeScale = 1;
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnCredits.onClick.AddListener(OnCreditsClicked);
        btnExit.onClick.AddListener(OnExitClicked);
        btnBackCredits.onClick.AddListener(OnBackCreditsClicked);

        myTransCam = myCamera.GetComponent<Transform>();
    }

    private void Update()
    {
        if (camMoving)
        {
            time += Time.deltaTime;
            if (time > maxTime)
            {
                camMoving = false;
                time = 0;
                Credits.SetActive(isCreditsEnabled);
                init.SetActive(!isCreditsEnabled);
            }
            else 
            {
                lerp = time / maxTime;
                myTransCam.transform.position = Vector3.Lerp(source, destination, lerp);
            }
        }
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
        Time.timeScale = 1;
        SceneManager.LoadScene(Game);
    }

    public void OnCreditsClicked()
    {
        destination = new Vector3(0, 11, -10);
        source = new Vector3(0, 0, -10);
        init.SetActive(false);
        camMoving = true;
        isCreditsEnabled = true;
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnBackCreditsClicked()
    {
        destination = new Vector3(0, 0, -10);
        source = new Vector3(0, 11, -10);
        Credits.SetActive(false);
        camMoving = true;
        isCreditsEnabled = false;
    }
}
