using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour
{
    //sceneにまたがるUI系の情報？

    public static Engine instance;

    [SerializeField, NonEditable]
    private TextMeshProUGUI scoreText;

    [SerializeField, NonEditable]
    private TextMeshProUGUI gameoverText;

    //public Button restartButton;

    private bool debug = true;
    private bool inGame;

    [SerializeField, NonEditable]
    private bool isGameover;

    [SerializeField, NonEditable]
    private int score;

    [SerializeField, NonEditable]
    private float timeLapse;

    private float gravityModifier = 2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainScene") Init();
        Physics.gravity *= gravityModifier; //staticな変数はシーンロードで初期化されないので注意
    }

    // Update is called once per frame
    private void Update()
    {
        if (inGame) timeLapse += Time.deltaTime;
    }

    private void Init()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        gameoverText = GameObject.Find("Canvas").transform.Find("Gameover").GetComponent<TextMeshProUGUI>();

        timeLapse = 0;
        isGameover = false;
        inGame = true;
        UpdateScore(-score);
        InvokeRepeating("AddTimeScore", 0, 1.0f);

        Debug.Log("init called");
        Debug.Log(gameoverText);
        Debug.Log(instance.gameoverText);
    }

    public void StartGame()
    {
        SceneManager.sceneLoaded += OnMainSceneLoaded;
        SceneManager.LoadScene("MainScene");
    }

    private void OnMainSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        instance.Init();//instance. が重要

        SceneManager.sceneLoaded -= OnMainSceneLoaded;
    }

    public void GameOver()
    {
        isGameover = true;
        inGame = false;
        gameoverText.gameObject.SetActive(true);
    }

    public void UpdateScore(int scoreToAdd)
    {
        if (!isGameover)
        {
            score += scoreToAdd;
            scoreText.text = "Score: " + score;
        }
    }

    private void AddTimeScore()
    {
        UpdateScore(1);
    }
}