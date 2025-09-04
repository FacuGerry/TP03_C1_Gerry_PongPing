using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Key bindings")]
    [SerializeField] private KeyCode GoUp = KeyCode.W;
    [SerializeField] private KeyCode GoLeft = KeyCode.A;
    [SerializeField] private KeyCode GoDown = KeyCode.S;
    [SerializeField] private KeyCode GoRight = KeyCode.D;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody2D playerRigidbody2D;
    [SerializeField] private Collider2D playerCollider;

    [Header("Player statistics")]
    public float speed = 10f;

    [Header("Colliders")]
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;
    [SerializeField] private GameObject wallMiddle;
    [SerializeField] private GameObject ball;

    private Collider2D floorCollider;
    private Collider2D roofCollider;
    private Collider2D wallLCollider;
    private Collider2D wallRCollider;
    private Collider2D wallMCollider;
    private Collider2D ballCollider;

    private SpriteRenderer playerColor;

    private void Awake()
    {
        playerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        playerColor = gameObject.GetComponent<SpriteRenderer>();

        roofCollider = roof.GetComponent<Collider2D>();
        floorCollider = floor.GetComponent<Collider2D>();
        wallLCollider = wallLeft.GetComponent<Collider2D>();
        wallRCollider = wallRight.GetComponent<Collider2D>();
        wallMCollider = wallMiddle.GetComponent<Collider2D>();
        ballCollider = ball.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    public void PlayerMove()
    {
        if (Input.GetKey(GoUp))
        {
            playerRigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.up);
        }
        if (Input.GetKey(GoDown))
        {
            playerRigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.down);
        }
        if (Input.GetKey(GoLeft))
        {
            playerRigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.left);
        }
        if (Input.GetKey(GoRight))
        {
            playerRigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.right);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.collider == wallLCollider
            || collision2D.collider == wallRCollider
            || collision2D.collider == wallMCollider
            || collision2D.collider == floorCollider
            || collision2D.collider == roofCollider)
        {
            playerColor.color = new Color(0, 0, 0, 1);
        }
        if (collision2D.collider == ballCollider)
        {
           playerColor.color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }

    //public void OnCollisionExit2D(Collision2D collision2D)
    //{
    //    if (collision2D.collider == wallLCollider
    //        || collision2D.collider == wallRCollider
    //        || collision2D.collider == wallMCollider
    //        || collision2D.collider == floorCollider
    //        || collision2D.collider == roofCollider)
    //    {
    //        playerColor.color = new Color(Random.value, Random.value, Random.value, 1);
    //    }
    //}
}
