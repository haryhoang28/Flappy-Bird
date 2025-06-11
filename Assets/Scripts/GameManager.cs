using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("Game point to start the game.")]
    private int score;
    public float normalPipeSpeed = 5f;
    public float increasedPipeSpeed = 7f;
    public int speedUpScore = 20;
    private bool speedIncreased = false;
    public Player player;
    public Text scoreText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject gamePaused;
    private int highScore;
    public Text highScoreText;

    private Spawner spawner;
    public void Start()
    {
        ResetPipeSpeed();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
        spawner = FindObjectOfType<Spawner>();
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f; // Resume the game
        player.enabled = true; // Enable the player script
        Pipes[] pipes = FindObjectsOfType<Pipes>();

        foreach (Pipes pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }
        // Reset the spawner's pool and restart spawning
        if (spawner != null)
        {
            spawner.ResetPoolAndSpawner();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        if (gamePaused != null)
            gamePaused.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        if (gamePaused != null)
            gamePaused.SetActive(false);
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();   
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        if (score >= speedUpScore && !speedIncreased)
        {
            Pipes.globalSpeed = increasedPipeSpeed;
            speedIncreased = true;
        }
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            UpdateHighScoreUI();
        }
    }
    private void ResetPipeSpeed()
    {
        Pipes.globalSpeed = normalPipeSpeed;
        speedIncreased = false;
    }
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}
