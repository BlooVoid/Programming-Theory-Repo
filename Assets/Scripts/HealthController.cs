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

    public void TakeDamage(int damage)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
    }
}
