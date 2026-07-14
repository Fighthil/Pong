using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Ball : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb;
    public float startSpeed;
    private float speed;
    public float maxSpeed;

    private int _P1Score = 0;
    private int _P2Score = 0;

    public GameObject topWall;
    public GameObject bottomWall;

    private int _direction;
    public float angleIntensity;
    private int _oldPaddle = 0;
    private int _newPaddle;

    public GameObject player1;
    public GameObject player2;


    private int _oldWall = 0;
    private int _justWon = 0;
    private int _newWall;
    private float heightDifference;
    public UIDocument UIDoc;
    public PhysicsMaterial2D PhysBounce;
    Label _scoreText;

    #endregion Variables

    void Start()
    {
        _scoreText = UIDoc.rootVisualElement.Q<Label>("Score");
        speed = startSpeed;
        rb = GetComponent<Rigidbody2D>(); //getting a referance to the rigidbody
        Launch();
        ChangeScore();
    }

    private void ChangeScore()
    {
        _scoreText.text = _P1Score + " - " + _P2Score;
    }

    private void Launch()
    {
        transform.position = new Vector3(0, 0, 0); //setting up the starting position

        if(_P1Score == _P2Score)
        {
            _direction = Random.Range(0,2);
            if(_direction == 0)
            {
                rb.linearVelocity = new Vector2(speed, Random.Range(-3,4));
            } else
            {
                rb.linearVelocity = new Vector2(-speed, Random.Range(-3,4));
            }
        }
        else
        {
            rb.linearVelocity = new Vector2(speed * _justWon, Random.Range(-5,6));
        }
    }

    private float BounceAngle()
    {
        if(_newPaddle == 1)
        {
            heightDifference = transform.position.y - player1.transform.position.y;
            return heightDifference * angleIntensity;
        }
        else
        {
            heightDifference = transform.position.y - player2.transform.position.y;
            return heightDifference * angleIntensity;
        }
    }

    private void Bounce()
    {
        rb.linearVelocityX = -rb.linearVelocityX;
        rb.linearVelocityY = BounceAngle();
        Debug.Log(rb.linearVelocityX);
        if(rb.linearVelocityX >= maxSpeed || rb.linearVelocityX <= -maxSpeed)
        {
            PhysBounce.bounciness = 1;
        } else
        {
            PhysBounce.bounciness = 1.02f;
        }

        if(rb.linearVelocityX > maxSpeed)
        {
            rb.linearVelocityX = maxSpeed;
        }
    }

    private void AddScore(string player)
    {
        if(player == "Right")
        {
            _P1Score += 1;
            _justWon = -1;
        }
        else
        {
            _P2Score += 1;
            _justWon = 1;
        }

        ChangeScore();
        Launch();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Paddles"))
        {
            _oldPaddle = _newPaddle;
            if(collision.gameObject.name == "Player 1")
            {
                _newPaddle = 1;
            } else
            {
                _newPaddle = 2;
            }

            if(_newPaddle != _oldPaddle)
            {
                Bounce();
            }
        }

        if(collision.gameObject.CompareTag("Walls"))
        {
            _oldWall = _newWall;
            if(collision.gameObject.name == "Bottom")
            {
                _newWall = 1;
            } else
            {
                _newWall = 2;
            }

            if(_newWall != _oldWall)
            {
                rb.linearVelocityY = -rb.linearVelocityY;
            }
        }

        if(collision.gameObject.CompareTag("Goals"))
        {
            AddScore(collision.gameObject.name);
        }
    }

    void Update()
    {
        if(rb.linearVelocityX > 0)
        {
            if(rb.linearVelocityX < startSpeed)
            {
                rb.linearVelocityX = startSpeed;
            }
        }
        else
        {
            if(rb.linearVelocityX > -startSpeed)
            {
                rb.linearVelocityX = -startSpeed;
            }
        }

        if(_newPaddle == 1 && rb.linearVelocityX < 0 && transform.position.x < -4)
        {
            rb.linearVelocityX = -rb.linearVelocityX;
        }

        if(_newPaddle == 2 && rb.linearVelocityX < 0 && transform.position.x > 4)
        {
            rb.linearVelocityX = -rb.linearVelocityX;
        }
    }
}
