using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnUpdateScore(int score);
    public event OnUpdateScore onUpdateScore;

    public delegate void OnUpdateTimer(float time);
    public event OnUpdateTimer onUpdateTimer;

    public static GameManager Instance;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform[] enemySpawnPoints;

    private List<GameObject> enemiesList = new List<GameObject>();

    private bool shouldSpawnWave = false;

    private float timer = 3f;
    private float timerMax = 3f;

    private int _score;

    public int Score { get => _score; private set { _score = value; } }

    public float Timer { get => timer; }

    public Action OnTimerStart;
    public Action OnTimerStop;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(enemiesList.Count <= 0 && !shouldSpawnWave)
        {
            shouldSpawnWave = true;
        }
        else if (enemiesList.Count == enemySpawnPoints.Length && shouldSpawnWave)
        {
            shouldSpawnWave = false;
        }

        if(shouldSpawnWave)
        {
            OnTimerStart();

            timer -= Time.deltaTime;

            onUpdateTimer?.Invoke(timer);

            if(timer <= 0)
            {
                SpawnEnemies();
                timer = timerMax;

                OnTimerStop();
            }
        }
    }

    private void SpawnEnemies()
    {
        foreach(Transform t in enemySpawnPoints)
        {
            int randomValue = UnityEngine.Random.Range(0, enemySpawnPoints.Length - 1);
            float enemyRotation = -180f;

            GameObject enemyGO = Instantiate(enemyPrefabs[0], t.position, Quaternion.Euler(0f, 0f, enemyRotation));

            var enemyController = enemyGO.GetComponent<EnemyController>();

            if(enemyController != null)
            {
                enemyController.onDead.AddListener((GameObject gameOject) =>
                {
                    Score += enemyController.ScoreMultiplier;
                    onUpdateScore?.Invoke(Score);
                    enemiesList.Remove(gameOject);
                });
            }

            enemiesList.Add(enemyGO);
        }
    }
}
