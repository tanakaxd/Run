using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//rumetimeinitializationで作られる
public class Engine : MonoBehaviour
{
    //sceneにまたがるUI系の情報？

    public static Engine instance;

    [SerializeField, NonEditable]
    private TextMeshProUGUI scoreText;

    [SerializeField, NonEditable]
    private Text moneyText;

    [SerializeField, NonEditable]
    private TextMeshProUGUI gameoverText;

    private GameObject[] enemies;

    //public Button restartButton;

    private bool inGame;

    [SerializeField, NonEditable]
    private bool isGameover=false;

    [SerializeField, NonEditable]
    private int score=0;
    public int Score { get {return score; } set {score=value; } }

    [SerializeField, NonEditable]
    private int money=0;
    public int Money { get { return money; } set { money = value; } }

    [SerializeField, NonEditable]
    private float timeLapse =0;

    [SerializeField]
    private float gravityModifier = 3;

    private int stage = 1;
    public int Stage { get { return stage; } }

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
        InvokeRepeating("AddInterest", 0, 10.0f);
        Physics.gravity *= gravityModifier; //staticな変数はシーンロードで初期化されないので注意。startで一回のみに
    }

    // Update is called once per frame
    private void Update()
    {
        if (inGame) timeLapse += Time.deltaTime;
        if (timeLapse >= 60 && inGame)
        {
            StageClear();
        }
    }

    private void Init()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        gameoverText = GameObject.Find("Canvas").transform.Find("Gameover").GetComponent<TextMeshProUGUI>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("stage: "+stage);
        //foreach(GameObject enemy in enemies)
        //{
        //    enemy.GetComponent<Enemy>().ShotsPerFire = stage;
        //}

        timeLapse = 0;
        isGameover = false;
        inGame = true;
        UpdateScore(0);
        UpdateMoney(0);
        InvokeRepeating("AddTimeScore", 0, 1.0f);

        Debug.Log("init called");
        //Debug.Log(gameoverText);
        //Debug.Log(instance.gameoverText);
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

    public void StageClear()
    {
        stage++;
        inGame = false;
        CancelInvoke("AddTimeScore");
        SceneManager.LoadScene("PurchaseScene");
    }

    public void GameOver()
    {
        isGameover = true;
        inGame = false;
        stage = 1;
        score = 0;
        money = 0;
        gameoverText.gameObject.SetActive(true);
        ItemManager.instance.Initialize();
        CancelInvoke("AddTimeScore");
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

    public void UpdateMoney(int moneyToAdd)
    {
        if (!isGameover)
        {
            money += moneyToAdd;
            moneyText.text = "Money: " + money;
        }
    }

    private void AddInterest()
    {
        if (inGame && !isGameover)
        {
            int interest = (int)(Engine.instance.Money * 0.1);
            UpdateMoney(interest);
            Debug.Log("interest piled: +" + interest);
        }
    }
}