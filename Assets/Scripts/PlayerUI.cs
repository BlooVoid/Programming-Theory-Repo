using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;
    [SerializeField] private float fillSmoothing = 10f;

    private PlayerController pController;

    private void Awake()
    {
        pController = GetComponentInParent<PlayerController>();

        pController.onTakeDamage.AddListener((GameObject gameObject) =>
        {
            healthFillImage.fillAmount = Mathf.Lerp(healthFillImage.fillAmount, pController.GetHealthNormailzed(), fillSmoothing * Time.deltaTime);
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
}
