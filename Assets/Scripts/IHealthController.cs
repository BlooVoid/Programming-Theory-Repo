using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class OnDead : UnityEvent<GameObject> { }

public interface IHealthController
{
    OnDead onDead { get; }

    int CurrentHealth { get; set; }

    int MaxHealth { get; set; }

    bool IsDead { get; set; }

    void TakeDamage(int damage);
}
