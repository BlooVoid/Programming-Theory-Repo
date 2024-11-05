using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnDead : UnityEvent<GameObject> { }

[System.Serializable]
public class OnTakeDamage : UnityEvent<GameObject> { }

public interface IHealthController
{
    OnDead onDead { get; set; }

    OnTakeDamage onTakeDamage { get; set; }

    float CurrentHealth { get; set; }

    float MaxHealth { get; set; }

    bool IsDead { get; set; }

    /// <summary>
    /// This will add health (positive and negative values accepted) to the currentHealth
    /// </summary>
    /// <param name="addHealth"></param>
    void ChangeHealth(int currentHealth);

    /// <summary>
    /// This will change the maxHealth of the controller (positive and negative values accepted).
    /// </summary>
    /// <param name="maxHealth"></param>
    void ChangeMaxHealth(int maxHealth);

    /// <summary>
    /// The method to apply damage to an object with HealthController implemented.
    /// </summary>
    /// <param name="damage"></param>
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
