using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Settings : MonoBehaviour
{

    public int players = 2;
    public int roundsToWin;
    public int maxBallSpeed;
    public string startingBall = "winners";

    public UIDocument UIDoc;

    private UnityEngine.UIElements.Button onePlayer;
    private UnityEngine.UIElements.Button twoPlayers;
    private SliderInt roundsToWinSlider;
    private SliderInt maxBallSpeedSlider;
    private UnityEngine.UIElements.Button losersButton;
    private UnityEngine.UIElements.Button winnersButton;
    private UnityEngine.UIElements.Button randomizerButton;
    private UnityEngine.UIElements.Button startButton;

    //private Vector2 defaultOnePlayerPos = new Vector2(0,0);

    private void Awake() {
        DontDestroyOnLoad(gameObject); //This gameobject now persits between scenes
    }

    void Start()
    {
        onePlayer = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("1Player");
        twoPlayers = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("2Players");
        maxBallSpeedSlider = UIDoc.rootVisualElement.Q<SliderInt>("MaxBallSpeed");
        roundsToWinSlider = UIDoc.rootVisualElement.Q<SliderInt>("RoundsToWin");
        losersButton = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("LosersButton");
        winnersButton = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("WinnersButton");
        randomizerButton = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("RandomizedButton");
        startButton = UIDoc.rootVisualElement.Q<UnityEngine.UIElements.Button>("StartButton");

        RectTransform uiPos = UIDoc.GetComponent<RectTransform>();

        onePlayer.clicked += onePlayerPressed;
        twoPlayers.clicked += twoPlayersPressed;

        losersButton.clicked += LosersBall;
        winnersButton.clicked += WinnersBall;
        randomizerButton.clicked += RandomBall;

        startButton.clicked += StartGame;

        Highlight();
    }

    private void Highlight()
    {
        if(players == 2)
        {
            twoPlayers.style.backgroundColor = Color.white;
            twoPlayers.style.color = Color.black;

            onePlayer.style.backgroundColor = Color.black;
            onePlayer.style.color = Color.white;
        }
        else if(players == 1)
        {
            onePlayer.style.backgroundColor = Color.white;
            onePlayer.style.color = Color.black;

            twoPlayers.style.backgroundColor = Color.black;
            twoPlayers.style.color = Color.white;
        }

        if(startingBall == "losers")
        {
            losersButton.style.backgroundColor = Color.white;
            losersButton.style.color = Color.black;

            winnersButton.style.backgroundColor = Color.black;
            winnersButton.style.color = Color.white;

            randomizerButton.style.backgroundColor = Color.black;
            randomizerButton.style.color = Color.white;
        }
        else if(startingBall == "winners")
        {
            winnersButton.style.backgroundColor = Color.white;
            winnersButton.style.color = Color.black;

            losersButton.style.backgroundColor = Color.black;
            losersButton.style.color = Color.white;

            randomizerButton.style.backgroundColor = Color.black;
            randomizerButton.style.color = Color.white;
        }
        else if(startingBall == "random")
        {
            randomizerButton.style.backgroundColor = Color.white;
            randomizerButton.style.color = Color.black;

            losersButton.style.backgroundColor = Color.black;
            losersButton.style.color = Color.white;

            winnersButton.style.backgroundColor = Color.black;
            winnersButton.style.color = Color.white;
        }
    }

    private void onePlayerPressed()
    {
        players = 1;
        Highlight();
    }

    private void twoPlayersPressed()
    {
        players = 2;
        Highlight();
    }

    private void LosersBall()
    {
        startingBall = "losers";
        Highlight();
    }
    private void WinnersBall()
    {
        startingBall = "winners";
        Highlight();
    }
    private void RandomBall()
    {
        startingBall = "random";
        Highlight();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    void Update()
    {
        roundsToWin = roundsToWinSlider.value;
        roundsToWinSlider.label = "Rounds to Win: " + roundsToWinSlider.value;

        maxBallSpeed = maxBallSpeedSlider.value;
        maxBallSpeedSlider.label = "Maximum Ball Speed: " + maxBallSpeedSlider.value;
    }
}
