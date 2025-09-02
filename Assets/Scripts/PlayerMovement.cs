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

    [Header("Player statistics")]
    public float speed = 10f;
    public Color color;

    private void Awake()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();

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

}
