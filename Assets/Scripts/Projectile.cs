using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int damage = 5;

    public int Damage { get => damage; protected set => damage = value; }

    private void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * transform.up;
    }

    public void ChangeDamage(int damageMultiplier)
    {
        Damage *= damageMultiplier;
    }
}
