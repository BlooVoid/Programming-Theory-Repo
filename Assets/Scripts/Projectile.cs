using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int damage = 5;

    public int Damage { get => damage; private set => damage = value; }

    private void Update()
    {
        transform.position += movementSpeed * Time.deltaTime * transform.forward;
    }

    public void ChangeDamage(int damageMultiplier)
    {
        Damage *= damageMultiplier;
    }

    private void OnTriggerEnter(Collider other)
    {
        var baseController = other.GetComponent<BaseController>();

        if (baseController != null)
        {
            if(baseController is PlayerController playerController)
            {
                baseController.TakeDamage(damage);
            }
        }
    }
}
