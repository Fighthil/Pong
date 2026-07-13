using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D player1;
    public Rigidbody2D player2;
    public float speed; //the speed that the player can move
    public float P1Pos;
    public float P2Pos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //the starting positions of the players
        player1.transform.position = new Vector3(-4, 0, 0);
        player2.transform.position = new Vector3(4, 0, 0);
    }

    // Update is called once per frame
    void Update()
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

        #endregion Player 2 Movement
    }
}
