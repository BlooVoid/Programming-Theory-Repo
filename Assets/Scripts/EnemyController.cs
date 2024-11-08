using UnityEngine;

public class EnemyController : BaseController
{
    [SerializeField] private int _scoreMultiplier = 2;

    public int ScoreMultiplier { get => _scoreMultiplier; private set => _scoreMultiplier = value; }

    private void OnDestroy()
    {
        onDead.RemoveAllListeners();
        onTakeDamage.RemoveAllListeners();
        OnFireProjectile.RemoveAllListeners();
    }

    private void Start()
    {
        onDead.AddListener((GameObject gameObject) =>
        {
            Destroy(gameObject);
        });
    }
}
