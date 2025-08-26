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

    public float ballSpeed = 200f;

    private void Awake()
    {

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
        /*collision.gameObject.SetActive(true);
        if (collision.Equals(goalRight) || collision.Equals(goalLeft))
        {
            ball.transform.position = Vector3.zero;
        }
        if ()
        {



        }*/

    }

    public void KickOff()
    {
        Rigidbody2D rigidbody2D = ballRigidbody2D;
        if (Random.value < 0.5f)
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.AddForce(ballSpeed * Time.fixedDeltaTime * new Vector2(1, Random.value));
                Debug.Log("Saque a la DERECHA ARRIBA");
            }
            else
            {
                rigidbody2D.AddForce(ballSpeed * Time.fixedDeltaTime * new Vector2(1, -Random.value));
                Debug.Log("Saque a la DERECHA ABAJO");
            }
        }
        else
        {
            if (Random.value < 0.5f)
            {
                rigidbody2D.AddForce(ballSpeed * Time.fixedDeltaTime * new Vector2(-1, Random.value));
                Debug.Log("Saque a la IZQUIERDA ARRIBA");
            }
            else
            {
                rigidbody2D.AddForce(ballSpeed * Time.fixedDeltaTime * new Vector2(-1, -Random.value));
                Debug.Log("Saque a la IZQUIERDA ABAJO");
            }
        }

    }

    public void BallMoving()
    {



    }



}
