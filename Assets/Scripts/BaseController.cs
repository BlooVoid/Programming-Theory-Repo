using UnityEngine;

public class BaseController : HealthController
{
    [Header("Movement")]
    [SerializeField] protected float _movementSpeed = 2f;
    [SerializeField] protected float speedMultiplier = 2f;
    [SerializeField] protected float movementSmoothing = 10f;
    [Header("Damage")]
    [SerializeField] protected int damage = 5;

    public float MovementSpeed { get => _movementSpeed; protected set { _movementSpeed = value; } }

    protected Vector2 inputDirection;
    protected Vector3 moveDirection;

    protected virtual void Start()
    {
        if (fillHealthOnStart)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public virtual void ChangeSpeed(float movementSpeed)
    {
        MovementSpeed *= movementSpeed;
    }

    public void ChangeDamage(int damage)
    {
        damage += damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        //var projectile = other.GetComponent<Projectile>();

        //if(other.GetComponent<Projectile>())
        //{
        //    TakeDamage(damage);
        //}
    }
}
