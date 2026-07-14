using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    
    public UIDocument UIDoc;
    private Button startButton;

    void Start()
    {
        startButton = UIDoc.rootVisualElement.Q<Button>("startButton");
        startButton.clicked += openMenu;
    }

    void openMenu()
    {
        SceneManager.LoadScene("Settings");
    }
}