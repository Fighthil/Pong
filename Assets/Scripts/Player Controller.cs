using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player1;
    public Rigidbody2D player2;
    public GameObject BallObject;
    public float speed; //the speed that the player can move
    public float P1Pos;
    public float P2Pos;
    private float prediction;
    private float aimError;
    private Rigidbody2D ballRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = BallObject.GetComponent<Rigidbody2D>();

        //the starting positions of the players
        player1.transform.position = new Vector3(-4, 0, 0);
        player2.transform.position = new Vector3(4, 0, 0);
    }

    private void CalculateBallPos()
    {
        Vector2 simulatedPos = BallObject.transform.position;
        Vector2 simulatedVel = ballRb.linearVelocity;

        if(ballRb.linearVelocityX <= 0)
        {
            prediction = 0f;
            return;
        }

        while (simulatedPos.x < player2.transform.position.x)
        {
            simulatedPos += simulatedVel * Time.fixedDeltaTime;

            if (simulatedPos.y >= 4.75f)
            {
                simulatedPos.y = 4.75f;
                simulatedVel.y *= -1;
            }

            if (simulatedPos.y <= -4.75f)
            {
                simulatedPos.y = -4.75f;
                simulatedVel.y *= -1;
            }
        }

        prediction = simulatedPos.y;
    }

    private void BotMovement()
    {
        if(ballRb.linearVelocityX > 0)
        {
            player2.transform.position = new Vector3(4, Mathf.MoveTowards(player2.transform.position.y, prediction + aimError, speed*0.8f*Time.fixedDeltaTime), 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Player 1 Movement
        P1Pos = player1.transform.position.y;

        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if(player1.transform.position.y < 3.25)
            {
                player1.linearVelocity = new Vector2(0, speed);
            } else
            {
                player1.linearVelocity = new Vector2(0, 0);
            }
        }

        if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if(player1.transform.position.y > -3.25)
            {
                player1.linearVelocity = new Vector2(0, -speed);
            } else
            {
                player1.linearVelocity = new Vector2(0, 0);
            }
        }

        if((!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.W)))
        {
            player1.linearVelocity = new Vector2(0, 0);
        }
        
        #endregion Player 1 Movement

        #region Player 2 Movement
        if(BallObject.GetComponent<Ball>().playerNum == 2)
        {
            P2Pos = player2.transform.position.y;
            
            if(Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                if(player2.transform.position.y < 3.25)
                {
                    player2.linearVelocity = new Vector2(0, speed);
                } else
                {
                    player2.linearVelocity = new Vector2(0, 0);
                }
            }

            if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
            {
                if(player2.transform.position.y > -3.25)
                {
                    player2.linearVelocity = new Vector2(0, -speed);
                } else
                {
                    player2.linearVelocity = new Vector2(0, 0);
                }
            }

            if((!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow)) || (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.UpArrow)))
            {
                player2.linearVelocity = new Vector2(0, 0);
            }
        }
        #endregion Player 2 Movement
        
        #region Bot Movement
        else if (BallObject.GetComponent<Ball>().playerNum == 1) 
        { 
            CalculateBallPos(); 
            if(ballRb.linearVelocityX > 0)
            {
                Invoke(nameof(BotMovement), UnityEngine.Random.Range(0.25f, 0.35f));
            }
            else
            {
                aimError = UnityEngine.Random.Range(-1.25f, 1.25f);
            }
        }
        #endregion Bot Movement
    }
}
