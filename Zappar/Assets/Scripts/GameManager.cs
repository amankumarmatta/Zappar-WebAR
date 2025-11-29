using TMPro;
using UnityEngine;
using Zappar;

public class GameManager : MonoBehaviour
{
[Header("Zappar")]
    public ZapparInstantTrackingTarget instantTracker;

    [Header("Prefabs")]
    public GameObject floorPrefab;
    public GameObject uiPrefab;
    public ObjectPool coinPool;
    public CoinSpawner coinSpawner;

    [Header("UI Refs")]
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI timerText;
    
    [Header("UI Parent")]
    public Transform canvasParent;

    private bool anchored = false;
    private GameObject floorInstance;
    private GameObject uiInstance;

    private int score = 0;
    private float gameTime = 30f;
    private bool gameActive = false;

    void Update()
    {
        if (!anchored)
        {
            AnchorAndSetup();
            return;
        }

        if (gameActive)
        {
            RunTimer();
        }
    }

    void AnchorAndSetup()
    {
        instantTracker.PlaceTrackerAnchor();
        anchored = true;
        
        floorInstance = Instantiate(
            floorPrefab,
            instantTracker.transform.position,
            Quaternion.identity,
            instantTracker.transform
        );
        floorInstance.transform.localRotation = Quaternion.identity;
        
        uiInstance = Instantiate(uiPrefab, canvasParent); 
        
        scoreText = uiInstance.transform.Find("Score").GetComponent<TextMeshProUGUI>();
        timerText = uiInstance.transform.Find("Timer").GetComponent<TextMeshProUGUI>();

        scoreText.text = "Score: 0";
        timerText.text = "Time: 30";
        
        coinSpawner.StartSpawning(floorInstance);
        
        gameActive = true;
    }

    void RunTimer()
    {
        gameTime -= Time.deltaTime;
        if (gameTime <= 0)
        {
            timerText.text = "Time: 0";
            gameTime = 0;
            gameActive = false;
            coinSpawner.StopSpawning();
            return;
        }

        timerText.text = "Time: " + Mathf.Ceil(gameTime);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public bool IsGameActive()
    {
        return gameActive;
    }
}