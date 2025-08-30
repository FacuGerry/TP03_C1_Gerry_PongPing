using UnityEngine;

public class Extras : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject food;
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float firstPowerUp;
    [SerializeField] private float timeBetweenPowers;
    [SerializeField] private float maxPositionX;
    [SerializeField] private float maxPositionY;
    private float minPositionX;
    private float minPositionY;

    private Transform axeTrans;
    private Transform shieldTrans;
    private Transform foodTrans;
    private Transform obstacleTrans;

    private Collider2D axeCollider;
    private Collider2D shieldCollider;
    private Collider2D foodCollider;

    private float randX;
    private float randY;

    private float timeFast;
    public float timeFastMax;
    public float superSpeed;

    private BallMovement ballMovement;

    private bool isFooding = false;

    private void Awake()
    {
        minPositionX = -1 * maxPositionX;
        minPositionY = -1 * maxPositionY;

        axeTrans = axe.GetComponent<Transform>();
        shieldTrans = shield.GetComponent<Transform>();
        foodTrans = food.GetComponent<Transform>();
        obstacleTrans = obstacle.GetComponent<Transform>();

        axeCollider = axe.GetComponent<Collider2D>();
        shieldCollider = shield.GetComponent<Collider2D>();
        foodCollider = food.GetComponent<Collider2D>();

        timeFastMax = timeFast;

    }

    private void Start()
    {
        InvokeRepeating("ObjectsSpawn", firstPowerUp, timeBetweenPowers);
        InvokeRepeating("ObjectsDestroy", firstPowerUp + timeBetweenPowers, timeBetweenPowers);
    }

    private void Update()
    {
        FastBall();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == axeCollider)
        {
            axe.SetActive(false);
        }
        if (collision.collider == shieldCollider)
        {
            shield.SetActive(false);
        }
        if (collision.collider == foodCollider)
        {
            isFooding = true;
            food.SetActive(false);
        }
    }

    public void ObjectsSpawn()
    {
        float dice = Random.value;
        Debug.Log(dice.ToString("0.00"));
        randX = Random.Range(minPositionX, maxPositionX);
        randY = Random.Range(minPositionY, maxPositionY);

        if (dice < 0.25f)
        {
            axe.SetActive(true);
            axeTrans.position = new Vector2(randX, randY);
        }
        if (dice >= 0.25f && dice < 0.5f)
        {
            shield.SetActive(true);
            shieldTrans.position = new Vector2(randX, randY);
        }
        if (dice >= 0.5f && dice < 0.75f)
        {
            food.SetActive(true);
            foodTrans.position = new Vector2(randX, randY);
        }
        if (dice >= 0.75f)
        {
            obstacle.SetActive(true);
            obstacleTrans.position = new Vector2(randX, randY);
        }
    }

    public void ObjectsDestroy()
    {
        axe.SetActive(false);
        shield.SetActive(false);
        food.SetActive(false);
        obstacle.SetActive(false);
    }

    public void FastBall()
    {
        if (isFooding)
        {
            timeFast -= Time.deltaTime;
            ballMovement.currentBallSpeed += superSpeed;
            if (timeFast < 0)
            {
                ballMovement.currentBallSpeed -= superSpeed;
                isFooding = false;
            }
        }
    }
}
