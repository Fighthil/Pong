using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb;
    public float startSpeed;
    private float speed;
    public int maxSpeed;
    public int roundsToWin;
    public string startingBall = "winners";

    public int playerNum;
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

    private Settings settings;

    private int _oldWall = 0;
    private int _justWon = 0;
    private int _newWall;
    private float heightDifference;
    public UIDocument UIDoc;
    public PhysicsMaterial2D PhysBounce;
    private Label _scoreText;
    private Button _menuButton;

    #endregion Variables

    void Start()
    {
        GameObject settingsObject = GameObject.Find("SettingsManager");

        if(settingsObject != null)
        {
            settings = settingsObject.GetComponent<Settings>();

            playerNum = settings.players;
            maxSpeed = settings.maxBallSpeed;
            roundsToWin = settings.roundsToWin;
            startingBall = settings.startingBall;
        } else
        {
            Debug.LogError("SettingsManager not found!");
            return;
        }

        _scoreText = UIDoc.rootVisualElement.Q<Label>("Score");
        _menuButton = UIDoc.rootVisualElement.Q<Button>("MenuButton");

        _menuButton.clicked += ReturnToMenu;

        speed = startSpeed;
        rb = GetComponent<Rigidbody2D>(); //getting a referance to the rigidbody
        Invoke(nameof(Launch), 0.5f);
        ChangeScore();
    }

    private void ChangeScore()
    {
        if(_P1Score < roundsToWin && _P2Score < roundsToWin)
        {
            _scoreText.text = _P1Score + " - " + _P2Score;
            _menuButton.SetEnabled(false);
            _menuButton.visible = false;
        }
    }

    private void Launch()
    {
        transform.position = new Vector3(0, 0, 0); //setting up the starting position
        rb.linearVelocity = Vector2.zero;

        _oldPaddle = 0;
        _newPaddle = 0;

        if(startingBall != "random")
        {
            if(_P1Score == _P2Score)
            {
                _direction = Random.Range(0,2);
                if(_direction == 0)
                {
                    rb.linearVelocity = new Vector2(startSpeed, Random.Range(-3,4));
                } else
                {
                    rb.linearVelocity = new Vector2(-startSpeed, Random.Range(-3,4));
                }
            }
            else
            {
                if(startingBall == "winners")
                {
                    rb.linearVelocity = new Vector2(startSpeed * _justWon, Random.Range(-3,4));
                }
                else if(startingBall == "losers")
                {
                    rb.linearVelocity = new Vector2(startSpeed * -_justWon, Random.Range(-3,4));
                }
            }
        }
        else
        {
            _direction = Random.Range(0,2);
            if(_direction == 0)
            {
                rb.linearVelocity = new Vector2(startSpeed, Random.Range(-3,4));
            } else
            {
                rb.linearVelocity = new Vector2(-startSpeed, Random.Range(-3,4));
            }
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

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Winner()
    {
        if(_P1Score >= roundsToWin)
        {
            _scoreText.text = "Player 1 Wins!";
        }
        else if(_P2Score >= roundsToWin)
        {
            _scoreText.text = "Player 2 Wins!";
        }
        startSpeed = 0;
        rb.linearVelocity = Vector2.zero;
        GetComponent<SpriteRenderer>().enabled = false;
        _menuButton.SetEnabled(true);
        _menuButton.visible = true;
    }

    void FixedUpdate()
    {
        if(_P1Score >= roundsToWin || _P2Score >= roundsToWin)
        {
            Winner();
        }

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
