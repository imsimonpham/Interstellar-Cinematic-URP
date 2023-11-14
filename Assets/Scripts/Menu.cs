using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    private string _mainScene = "Main";
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _instructions;
    [SerializeField] private TextMeshProUGUI  _gameTitle;


    void Start()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene(_mainScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowInstructions()
    {
        _pauseMenu.SetActive(false);
        _instructions.SetActive(true);
        _gameTitle.color = new Color(0.8113208f, 0.3056465f, 0.1186365f, 0.3f);
    }
   
    public void ShowPauseMenu()
    {
        _pauseMenu.SetActive(true);
        _instructions.SetActive(false);
        _gameTitle.color = new Color(0.8113208f, 0.3056465f, 0.1186365f, 1f);
    }
}
