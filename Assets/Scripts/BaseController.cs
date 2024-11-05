using UnityEngine;
using UnityEngine.Events;

public class BaseController : HealthController
{
    [Header("Movement")]
    [SerializeField] protected float _movementSpeed = 2f;
    [SerializeField] protected float _speedModifier = 1f;
    [SerializeField] protected float movementSmoothing = 10f;
    [Header("Projectile")]
    [SerializeField] protected bool canFire = true;
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected float projectileSpeedModifier = 0f;
    [SerializeField] protected int damageMultiplier = 1; 
    [SerializeField] protected float fireRate = 2f;

    
    public float MovementSpeed { get => _movementSpeed; protected set => _movementSpeed = value;  }
    public float SpeedModifier { get => _speedModifier; protected set => _speedModifier = value; }

    public UnityEvent OnFireProjectile;

    private float timer;

    protected virtual void Awake()
    {
        if (fillHealthOnStart)
        {
            CurrentHealth = MaxHealth;
        }
    }

    protected virtual void Update()
    {
        FireProjectile();
    }

    public virtual void ChangeSpeed(float modifier)
    {
        MovementSpeed += modifier;
    }

    protected virtual void FireProjectile()
    {
        if (canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {

                foreach (Transform t in firePoints)
                {
                    GameObject projectileGO = Instantiate(projectilePrefab, t.position, t.rotation);
                    Projectile projectile = projectileGO.GetComponent<Projectile>();
                    projectile.ChangeMovementSpeed(projectileSpeedModifier);
                    projectile.ChangeDamage(damageMultiplier);
                }

                OnFireProjectile?.Invoke();
                timer = 0;
            }
        }
    }
}
