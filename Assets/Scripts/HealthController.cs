using UnityEngine;

public class HealthController : MonoBehaviour, IHealthController
{
    [Header("Health")]
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] protected bool fillHealthOnStart = true;

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
                _onDead?.Invoke(gameObject);
            }
        }
    }

    public OnDead _onDead = new OnDead();
    public OnTakeDamage _onTakeDamage = new OnTakeDamage();

    public OnDead onDead { get => _onDead; set => _onDead = value; }
    public OnTakeDamage onTakeDamage { get => _onTakeDamage; set => _onTakeDamage = value; }

    public void ChangeHealth(int currentHealth)
    {
        _currentHealth += currentHealth;
    }

   
    public void ChangeMaxHealth(int maxHealth)
    {
        _maxHealth += maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            _onTakeDamage?.Invoke(gameObject);
        }
    }
}
