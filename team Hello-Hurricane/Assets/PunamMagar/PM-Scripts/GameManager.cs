using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event Action OnGameReset;

    [Header("Gameplay Settings")]
    [SerializeField] LevelDifficulty easy_levelDifficulty;
    [SerializeField] LevelDifficulty medium_levelDifficulty;
    [SerializeField] LevelDifficulty hard_levelDifficulty;

    [SerializeField] string mainGameScene;

    public float currentSpeed = 0;

    int startingSpeed;

    float speedIncrementRate;
    float incrementRate;
    float lerpTime;

    LevelDifficulty currLevelDifficulty;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (currLevelDifficulty == null) 
        {
            SetToEasy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpeed < startingSpeed - 0.01f)
        {
            lerpTime += Time.deltaTime / currLevelDifficulty.timeToReachStartSpeed;
            currentSpeed = Mathf.Lerp(0, startingSpeed, lerpTime);
            return;
        }

        incrementRate = speedIncrementRate * Time.deltaTime;
        currentSpeed += incrementRate;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += ResetGame;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= ResetGame;
    }

    void ResetGame(Scene scene, LoadSceneMode mode)
    {
        SetDifficulty();
        currentSpeed = 0;
        lerpTime = 0;

        if (SceneManager.GetActiveScene().name == mainGameScene) 
        {
            OnGameReset?.Invoke();
        }
    }

    public void SetToEasy() 
    {
        currLevelDifficulty = easy_levelDifficulty;
        SetDifficulty();
    }

    public void SetToMedium()
    {
        currLevelDifficulty = medium_levelDifficulty;
        SetDifficulty();
    }

    public void SetToHard()
    {
        currLevelDifficulty = hard_levelDifficulty;
        SetDifficulty();
    }

    void SetDifficulty() 
    {
        startingSpeed = currLevelDifficulty.startingSpeed;
        speedIncrementRate = currLevelDifficulty.speedIncrementRate;
    }
}
