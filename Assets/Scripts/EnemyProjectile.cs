using UnityEngine;

public class EnemyProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.TakeDamage(Damage);

            Destroy(gameObject);
        }
    }
}
