using UnityEngine;
using UnityEngine.SceneManagement;

// A script that loads the new scene...which is currently only one and quits the game.

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
