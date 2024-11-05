using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    [SerializeField] private float fillSmoothing = 0.5f;

    private PlayerController pController;

    private float timer;

    private void Awake()
    {
        pController = GetComponentInParent<PlayerController>();

        pController.onTakeDamage.AddListener((GameObject gameObject) =>
        {
            StartCoroutine(UpdateHealthBar()); // Had to move to coroutine to update the lerping
        });
    }

    private void Update()
    {
        // removed from update to the event, that way it isnt always updating, only when it should.
        //healthFillImage.fillAmount = Mathf.Lerp(healthFillImage.fillAmount, pController.GetHealthNormailzed(), fillSmoothing * Time.deltaTime);

        //Debug

        //Debug.Log("Normalized Health: " + pController.GetHealthNormailzed());
        //Debug.Log("Current Health: " + pController.CurrentHealth);
        //Debug.Log("Max Health: " + pController.MaxHealth);
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
