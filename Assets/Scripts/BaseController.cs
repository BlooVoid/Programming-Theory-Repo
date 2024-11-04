using UnityEngine;

public class BaseController : HealthController
{
    [Header("Movement Speed")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float speedMultiplier = 2f;
    [Header("Damage")]
    [SerializeField] protected int damage = 5;


    protected Vector2 inputDirection;
    protected Vector3 moveDirection;

    private void OnTriggerEnter(Collider other)
    {
        //var projectile = other.GetComponent<Projectile>();

        //if(other.GetComponent<Projectile>())
        //{
        //    TakeDamage(damage);
        //}
    }
}
