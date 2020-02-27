using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    //sceneにまたがるUI系の情報？


    public static WorldManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;

    public bool isGameover;
    public int score;
    private void Awake()
    {
        if (instance == null)
        {
        instance = this;

        }else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        isGameover = true;
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    public void UpdateScore(int scoreToAdd)
    {
        if (!isGameover)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        UpdateScore(0);
    }
}
