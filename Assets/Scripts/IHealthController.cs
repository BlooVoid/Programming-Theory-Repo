using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnDead : UnityEvent<GameObject> { }

public interface IHealthController
{
    OnDead onDead { get; }

    int CurrentHealth { get; set; }

    int MaxHealth { get; set; }

    bool IsDead { get; set; }

    void ChangeHealth(int currentHealth);

    void ChangeMaxHealth(int maxHealth);

    void TakeDamage(int damage);
}

public static class HealthControllerHelper
{
    public static IHealthController GetHealthController(this GameObject gameObject)
    {
        IHealthController healthController = gameObject.GetComponent<IHealthController>();

        if (healthController != null)
        {
            return healthController;
        }

        return null;
    }

    public static void ApplyDamage(this GameObject gameObject, int damage)
    {
        GetHealthController(gameObject).TakeDamage(damage);
    }
}
