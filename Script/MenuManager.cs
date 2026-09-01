using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    //
    public GameObject gameSystem;
    public GameObject mainMenuPanel;
    public GameObject exitMenuPanel;
    public GameObject gameOverMenuPanel;
    public GameObject pauseMenuPanel;
    public GameObject level2MenuPanel;
    public GameObject level4MenuPanel;
    public GameObject level6MenuPanel;

    public AudioClip gameOverMusic;
    public AudioClip youWinMusic;
    public AudioSource playerAudio;


    //
    // public GameObject pauseMenuPanel;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    // FOR STARTING GAME
    public void StartGame()
    {
        Time.timeScale = 1f;
        gameSystem.SetActive(true);
        mainMenuPanel.SetActive(false);
        gameOverMenuPanel.SetActive(false);
        //PlayerController.Instance.PlayerReset();
    }

    // FOR EXIT GAME
    public void ExitGame()
    {
        exitMenuPanel.SetActive(true);
    }
    public void YesExit()
    {
        Application.Quit();
    }
    public void NoExit()
    {
       exitMenuPanel.SetActive(false); 
    }

    // FOR GAMEOVER GAME
    public void GameOver()
    {
        gameOverMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        mainMenuPanel.SetActive(true);
        gameOverMenuPanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // FOR PAUSE GAME
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }
    public void PauseGameResume()
    {
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
    public void PauseGameExit()
    {
        mainMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    // FOR PLAYER LEVELING 2
    public void Level2Player()
    {
        Time.timeScale = 0f;
        level2MenuPanel.SetActive(true);
    }
    public void Level2Continue()
    {
        Time.timeScale = 1f;
        level2MenuPanel.SetActive(false);
    }

    // FOR PLAYER LEVELING 4
    public void Level4Player()
    {
        Time.timeScale = 0f;
        level4MenuPanel.SetActive(true);
    }
    public void Level4Continue()
    {
        Time.timeScale = 1f;
        level4MenuPanel.SetActive(false);
    }

    // FOR PLAYER LEVELING 6
    public void Level6Player()
    {
        Time.timeScale = 0f;
        level6MenuPanel.SetActive(true);
    }
    public void Level6Continue()
    {
        Time.timeScale = 1f;
        level6MenuPanel.SetActive(false);
    }

    // YOU WIN MUSIC
    public void YouWinMusic()
    {
        playerAudio.clip = youWinMusic;
        playerAudio.Play();
    }
    // GAMEOVER MUSIC
    public void GameOverMusic()
    {
        playerAudio.clip = gameOverMusic;
        playerAudio.Play();
    }
}
