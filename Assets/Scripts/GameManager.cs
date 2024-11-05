using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void OnUpdateScore(int score);
    public event OnUpdateScore onUpdateScore;

    public delegate void OnUpdateTimer(float time);
    public event OnUpdateTimer onUpdateTimer;

    public static GameManager Instance;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform[] enemySpawnPoints;
    public bool isGameOver = false;

    private List<GameObject> enemiesList = new List<GameObject>();

    private bool shouldSpawnWave = false;

    private float timer = 3f;
    private float timerMax = 3f;

    private int _score;

    public int Score { get => _score; private set { _score = value; } }

    public float Timer { get => timer; }

    public Action OnTimerStart;
    public Action OnTimerStop;
    public UnityEvent OnGameOver;

    private void Awake()
    {
        Instance = this;
        shouldSpawnWave = enemiesList.Count <= 0;
    }

    private void Start()
    {
        PlayerController.Instance.onDead.AddListener((GameObject gameObject) =>
        {
            isGameOver = true;
            DataManager.Instance.CheckBestPlayer();
            OnGameOver?.Invoke();
        });
    }

    private void Update()
    {
        if (shouldSpawnWave)
        {
            if (timer == timerMax)
            {
                OnTimerStart?.Invoke();
            }

            timer -= Time.deltaTime;
            onUpdateTimer?.Invoke(timer);

            if (timer <= 0)
            {
                SpawnEnemies();
                timer = timerMax;
                OnTimerStop?.Invoke();
                shouldSpawnWave = false;
            }
        }
    }

    private void SpawnEnemies()
    {
        foreach(Transform t in enemySpawnPoints)
        {
            int randomValue = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            float enemyRotation = -180f;

            GameObject enemyGO = Instantiate(enemyPrefabs[randomValue], t.position, Quaternion.Euler(0f, 0f, enemyRotation));

            var enemyController = enemyGO.GetComponent<EnemyController>();

            if(enemyController != null)
            {
                enemyController.onDead.AddListener((GameObject gameOject) =>
                {
                    Score += enemyController.ScoreMultiplier;
                    DataManager.Instance.SetScore(Score);
                    onUpdateScore?.Invoke(Score);
                    enemiesList.Remove(gameOject);
                    shouldSpawnWave = enemiesList.Count <= 0;
                });
            }

            enemiesList.Add(enemyGO);
        }
    }
}
