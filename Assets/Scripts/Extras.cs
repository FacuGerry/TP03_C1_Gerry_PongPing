using UnityEngine;

public class Extras : MonoBehaviour
{
    public GameObject shield;
    public GameObject food;
    public GameObject obstacle;
    [SerializeField] private float firstPowerUpSpawn;
    [SerializeField] private float timeBetweenPowersSpawn;
    [SerializeField] private float maxPositionX;
    [SerializeField] private float maxPositionY;
    private float minPositionX;
    private float minPositionY;

    private Transform shieldTrans;
    private Transform foodTrans;
    private Transform obstacleTrans;

    private float randX;
    private float randY;

    private float timeFast;
    public float timeFastMax;
    public float superSpeed;

    private void Awake()
    {
        minPositionX = -1 * maxPositionX;
        minPositionY = -1 * maxPositionY;

        shieldTrans = shield.GetComponent<Transform>();
        foodTrans = food.GetComponent<Transform>();
        obstacleTrans = obstacle.GetComponent<Transform>();

        timeFastMax = timeFast;
    }

    private void Start()
    {
        InvokeRepeating("ObjectsSpawn", firstPowerUpSpawn, timeBetweenPowersSpawn);
        InvokeRepeating("ObjectsDestroy", firstPowerUpSpawn + timeBetweenPowersSpawn, timeBetweenPowersSpawn);
    }

    public void ObjectsSpawn()
    {
        float dice = Random.value;
        Debug.Log(dice.ToString("0.00"));
        randX = Random.Range(minPositionX, maxPositionX);
        randY = Random.Range(minPositionY, maxPositionY);

        if (dice < 0.33f)
        {
            shield.SetActive(true);
            shieldTrans.position = new Vector2(randX, randY);
        }
        if (dice >= 0.33f && dice < 0.66f)
        {
            food.SetActive(true);
            foodTrans.position = new Vector2(randX, randY);
        }
        if (dice >= 0.66f)
        {
            obstacle.SetActive(true);
            obstacleTrans.position = new Vector2(randX, randY);
        }
    }

    public void ObjectsDestroy()
    {
        shield.SetActive(false);
        food.SetActive(false);
        obstacle.SetActive(false);
    }
}
