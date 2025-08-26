using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Key bindings")]
    [SerializeField] private KeyCode GoUp = KeyCode.W;
    [SerializeField] private KeyCode GoDown = KeyCode.S;

    [Header("Rigidbody")]
    [SerializeField] private Rigidbody2D playerRigidbody2D;

    [Header("Player statistics")]
    public float speed = 10f;
    public int size = 1;
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
            Rigidbody2D rigidbody2D = playerRigidbody2D;
            rigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.up);
        }
        if (Input.GetKey(GoDown))
        {
            Rigidbody2D rigidbody2D = playerRigidbody2D;
            rigidbody2D.AddForce(speed * Time.fixedDeltaTime * Vector2.down);
        }


    }

}
