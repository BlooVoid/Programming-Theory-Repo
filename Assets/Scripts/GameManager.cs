using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform[] enemySpawnPoints;

    private List<GameObject> enemiesList = new List<GameObject>();

    private bool shouldSpawnWave = false;

    private int _score;

    public int Score { get => _score; private set { _score = value; } }

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
            SpawnEnemies();
        }
    }
    private void SpawnEnemies()
    {
        foreach(Transform t in enemySpawnPoints)
        {
            int randomValue = Random.Range(0, enemySpawnPoints.Length - 1);

            GameObject enemyGO = Instantiate(enemyPrefabs[0], t.position, Quaternion.Euler(0f, 0f, -180f));

            var enemyController = enemyGO.GetComponent<EnemyController>();

            if(enemyController != null)
            {
                enemyController.onDead.AddListener((GameObject gameOject) =>
                {
                    _score *= enemyController.ScoreMultiplier;
                    enemiesList.Remove(gameOject);
                });
            }

            enemiesList.Add(enemyGO);
        }
    }
}
