using System.Security.Cryptography;
using UnityEngine;

public class HealthController : MonoBehaviour, IHealthController
{
    [Header("Health")]
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected float _maxHealth = 100;
    [SerializeField] protected bool fillHealthOnStart = true;

    public bool _isDead;

    public virtual float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
                _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
            }
            var newDeathState = _currentHealth <= 0;
            if (newDeathState != IsDead)
            {
                IsDead = newDeathState;
            }
        }
    }

    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    public virtual bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            if (_isDead != value)
            {
                _isDead = value;
                if (value) onDead.Invoke(gameObject);
            }
        }
    }

    public OnDead _onDead = new OnDead();
    public OnTakeDamage _onTakeDamage = new OnTakeDamage();

    public OnDead onDead { get => _onDead; set => _onDead = value; }
    public OnTakeDamage onTakeDamage { get => _onTakeDamage; set => _onTakeDamage = value; }

    public void ChangeHealth(int health)
    {
        CurrentHealth += health;
    }

   
    public void ChangeMaxHealth(int maxHealth)
    {
        MaxHealth += maxHealth;
    }

    /// <summary>
    /// Take a passed through amount of damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            if (CurrentHealth >= 0)
            {
                CurrentHealth -= damage;
                onTakeDamage.Invoke(gameObject);
            }
        } 
    }

    /// <summary>
    /// Take a static set amount of damage
    /// </summary>
    public void TakeDamage()
    {
        if (CurrentHealth >= 0)
        {
            CurrentHealth -= 25;
            onTakeDamage.Invoke(gameObject);
        }
    }
}
