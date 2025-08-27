using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Borders")]
    [SerializeField] private GameObject goalLeft;
    [SerializeField] private GameObject goalRight;
    [SerializeField] private GameObject roof;
    [SerializeField] private GameObject floor;

    [Header("Players")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;

    [SerializeField] private Rigidbody2D ballRigidbody2D;
    [SerializeField] private GameObject ball;

    public float ballSpeed = 10000f;

    private Collider2D wallLeft;
    private Collider2D wallRight;
    private Collider2D rooftop;
    private Collider2D floorbot;
    private Collider2D p1;
    private Collider2D p2;

    private void Awake()
    {
        wallLeft = goalLeft.GetComponent<Collider2D>();
        wallRight = goalRight.GetComponent<Collider2D>();
        rooftop = roof.GetComponent<Collider2D>();
        floorbot = floor.GetComponent<Collider2D>();
        p1 = player1.GetComponent<Collider2D>();
        p2 = player2.GetComponent<Collider2D>();
    }

    void Start()
    {
        KickOff();
    }

    private void FixedUpdate()
    {
        BallMoving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = ballRigidbody2D;

        if (collision.collider == wallLeft || collision.collider == wallRight)
        {
            ball.transform.position = Vector2.zero;
            //Agregar una funcion para que espere 3 segundos y vuelva a sacar
            KickOff();
        }

        if (collision.collider == rooftop)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, -1).normalized;
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, -1).normalized;
            }
        }

        if (collision.collider == floorbot)
        {
            if (rigidbody2D.velocity.x > 0)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, 1).normalized;
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, 1).normalized;
            }
        }

        if (collision.collider == p1)
        {
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, 1).normalized;
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, -1).normalized;
            }
        }

        if (collision.collider == p2)
        {
            if (rigidbody2D.velocity.y > 0)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, 1).normalized;
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, -1).normalized;
            }
        }
    }

    public void KickOff()
    {
        Rigidbody2D rigidbody2D = ballRigidbody2D;
        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, Random.value).normalized;
                Debug.Log("Saque a la DERECHA ARRIBA");
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(1, -Random.value).normalized;
                Debug.Log("Saque a la DERECHA ABAJO");
            }
        }
        else
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, Random.value).normalized;
                Debug.Log("Saque a la IZQUIERDA ARRIBA");
            }
            else
            {
                rigidbody2D.velocity = ballSpeed * new Vector2(-1, -Random.value).normalized;
                Debug.Log("Saque a la IZQUIERDA ABAJO");
            }
        }

    }

    public void BallMoving()
    {



    }



}
