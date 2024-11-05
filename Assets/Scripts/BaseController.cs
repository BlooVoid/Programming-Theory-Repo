using UnityEngine;
using UnityEngine.Events;

public class BaseController : HealthController
{
    [Header("Movement")]
    [SerializeField] protected float _movementSpeed = 2f;
    [SerializeField] protected float _speedMultiplier = 0.5f;
    [SerializeField] protected float movementSmoothing = 10f;
    [Header("Projectile")]
    [SerializeField] protected bool canFire = true;
    [SerializeField] protected Transform[] firePoints;
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected int damageMultiplier = 1; // start at one, pick up items to increase this value (this used to be zero... you can guess why that didn't work.)
    [SerializeField] protected float fireRate = 2f;

    
    public float MovementSpeed { get => _movementSpeed; protected set => _movementSpeed = value;  }
    public float SpeedMultiplier { get => _speedMultiplier; protected set => _speedMultiplier = value; }

    public UnityEvent<Projectile> OnFireProjectile;

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

    public virtual void ChangeSpeed()
    {
        MovementSpeed *= _speedMultiplier;
    }

    protected virtual void FireProjectile()
    {
        if (canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                Projectile projectile = null;

                foreach (Transform t in firePoints)
                {
                    GameObject projectileGO = Instantiate(projectilePrefab, t.position, t.rotation);
                    projectile = projectileGO.GetComponent<Projectile>();
                    projectile.ChangeDamage(damageMultiplier);
                }


                OnFireProjectile?.Invoke(projectile);
                timer = 0;
            }
        }
    }
}
