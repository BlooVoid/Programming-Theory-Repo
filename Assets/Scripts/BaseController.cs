using UnityEngine;
using UnityEngine.Events;

public class BaseController : HealthController
{
    [Header("Movement")]
    [SerializeField] protected float _movementSpeed = 2f;
    [SerializeField] protected float _speedMultiplier = 0.5f;
    [SerializeField] protected float movementSmoothing = 10f;
    [Header("Projectile")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int damageMultiplier = 0; // start at zero, pick up items to increase this value
    [SerializeField] protected float fireRate = 2f;

    
    public float MovementSpeed { get => _movementSpeed; protected set => _movementSpeed = value;  }
    public float SpeedMultiplier { get => _speedMultiplier; protected set => _speedMultiplier = value; }

    public UnityEvent<Projectile> OnFireProjectile;

    private float timer;

    private void Awake()
    {
        if (fillHealthOnStart)
        {
            CurrentHealth = MaxHealth;
        }
    }

    private void Update()
    {
        FireProjectile();
    }

    public virtual void ChangeSpeed()
    {
        MovementSpeed *= _speedMultiplier;
    }

    protected virtual void FireProjectile()
    {
        timer += Time.deltaTime;
        if(timer > fireRate)
        {
            var projectileGO = Instantiate(projectilePrefab);
            var projectile = projectileGO.GetComponent<Projectile>();

            OnFireProjectile?.Invoke(projectile);
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var projectile = other.GetComponent<Projectile>();

        if(other.GetComponent<Projectile>())
        {
            TakeDamage(projectile.Damage);
        }
    }
}
