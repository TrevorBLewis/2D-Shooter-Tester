using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script handles the scenes and a few button key presses. Mainly the pause screen but the fire mechanism I added here as well.
// I have a world speed declared and a counter for the number of little creatures destroyed by the player

public class GameManager : MonoBehaviour
{

    
    public static GameManager Instance;


    public float worldSpeed;

    public int critterCounter;
    [SerializeField] private GameObject boss1;
    


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        critterCounter = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            Pause();
        }

        if (critterCounter > 10)
        {
            critterCounter = 0;
            Instantiate(boss1, new Vector2(0, 7f), Quaternion.Euler(0,0,0));
        }
    }

    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.Instance.PlaySound(AudioManager.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.Instance.ExitBoost();
            AudioManager.Instance.PlaySound(AudioManager.Instance.unpause);
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("GameOver");
    }
}
