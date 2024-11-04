using UnityEngine;

public class HealthController : MonoBehaviour, IHealthController
{
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth = 100;

    public int CurrentHealth 
    { 
        get => _currentHealth;
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = Mathf.Clamp(value, 0, _maxHealth);
            }
        }
    }
    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public bool IsDead
    {
        get => _currentHealth <= 0;
        set 
        {
            if (value)
            {
                _onDead.Invoke(gameObject);
            }
        }
    }

    public OnDead _onDead = new OnDead();

    OnDead IHealthController.onDead { get => _onDead; }

    /// <summary>
    /// This will add health (positive and negative values accepted) to the <see cref="_currentHealth"/>.
    /// </summary>
    /// <param name="addHealth"></param>
    public void ChangeHealth(int currentHealth)
    {
        _currentHealth += currentHealth;
    }

    /// <summary>
    /// This will change the <see cref="_maxHealth"/> of the controller (positive and negative values accepted).
    /// </summary>
    /// <param name="maxHealth"></param>
    public void ChangeMaxHealth(int maxHealth)
    {
        _maxHealth += maxHealth;
    }

    /// <summary>
    /// The method to apply damage to an object with HealthController implemented.
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
    }
}
