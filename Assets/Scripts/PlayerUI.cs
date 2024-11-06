using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Image healthFillImage;
    [SerializeField] private float fillSmoothing = 0.5f;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverGO;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button tryAgainButton;
    private PlayerController pController;

    private void Awake()
    {
        pController = GetComponentInParent<PlayerController>();
    }

    private void Start()
    {
        StartCoroutine(UpdateHealthBar());

        pController.onTakeDamage.AddListener(() =>
        {
            StartCoroutine(UpdateHealthBar()); 
        });

        GameManager.Instance.onUpdateScore += GameManager_onUpdateScore;
        GameManager.Instance.onUpdateTimer += GameManager_onUpdateTimer;

        GameManager.Instance.OnTimerStart += ShowTimerText;
        GameManager.Instance.OnTimerStop += HideTimerText;

        GameManager.Instance.OnGameOver.AddListener(() =>
        {
            gameOverGO.SetActive(true);
        });

        exitGameButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ExitGame();
        });

        tryAgainButton.onClick.AddListener(() =>
        {
            DataManager.Instance.ReloadScene();
        });
    }

    private void GameManager_onUpdateTimer(float time)
    {
        timerText.text = Mathf.CeilToInt(time).ToString();
    }

    private void GameManager_onUpdateScore(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    private void ShowTimerText()
    {
        timerText.gameObject.SetActive(true);
    }

    private void HideTimerText()
    {
        timerText.gameObject.SetActive(false);
    }

    private IEnumerator UpdateHealthBar()
    {
        float duration = fillSmoothing; // Set the desired smoothing duration
        float elapsedTime = 0f;

        float initialFill = healthFillImage.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            healthFillImage.fillAmount = Mathf.Lerp(initialFill, pController.GetHealthNormailzed(), t);

            yield return null; // Wait for the next frame
        }
    }
}
