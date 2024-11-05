using UnityEngine;

public class PlayerProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemyController = other.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            enemyController.TakeDamage(Damage);

            Destroy(gameObject);
        }
    }
}
