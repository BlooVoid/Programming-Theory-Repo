using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance {  get; private set; }

    public int score;
    public string playerName;

    public static int bestScore;
    public static string bestPlayer;

    public string BestPlayer { get => bestPlayer; }
    public int BestScore { get => bestScore; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();

        Application.quitting += Application_quitting;
    }

    private void Application_quitting()
    {
        CheckBestPlayer();
    }

    public void CheckBestPlayer()
    {
        if(bestScore < score)
        {
            Debug.Log("New high score!");
            Save();
        }
    }

    public void Save()
    {
        SaveData saveData = new SaveData();

        saveData.bestScore = score;
        saveData.bestPlayer = playerName;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string file = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(file);

            bestPlayer = saveData.bestPlayer;
            bestScore = saveData.bestScore;
        }
        else
        {
            bestPlayer = null;
            bestScore = 0;
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void SetScore(int currentScore)
    {
        score = currentScore;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    class SaveData
    {
        public int bestScore;
        public string bestPlayer;
    }
}
