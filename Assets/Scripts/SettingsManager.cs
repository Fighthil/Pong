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
    public int startingBall;

    public UIDocument UIDoc;

    private UnityEngine.UIElements.Button onePlayer;
    private UnityEngine.UIElements.Button twoPlayers;
    private SliderInt roundsToWinSlider;
    private SliderInt maxBallSpeedSlider;
    private UnityEngine.UIElements.Button losersButton;
    private UnityEngine.UIElements.Button winnersButton;
    private UnityEngine.UIElements.Button randomizerButton;
    private UnityEngine.UIElements.Button startButton;

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


        startButton.clicked += StartGame;
    }

    private void Highlight()
    {
        
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
        maxBallSpeedSlider.label = "Maximum Ball Speek: " + maxBallSpeedSlider.value;
    }
}
