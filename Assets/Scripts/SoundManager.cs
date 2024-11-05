using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private float _masterVolume = 1.0f;

    [SerializeField] private AudioClip[] fireSounds;
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip[] deathSounds;

    public float MasterVolume 
    { 
        get => _masterVolume; 
        set 
        { 
            _masterVolume = value; 
            _masterVolume = Mathf.Clamp(_masterVolume, 0.0f, 1.0f);
        }
    }

    private void Start()
    {
        var playerController = PlayerController.Instance;

        playerController.OnFireProjectile.AddListener((Projectile projectile) =>
        {
            PlayClipAtPoint(fireSounds, playerController.transform.position, MasterVolume);
        });

        playerController.onTakeDamage.AddListener((GameObject gameObject) =>
        {
            PlayClipAtPoint(damageSounds, playerController.transform.position, MasterVolume);
        });

        playerController.onDead.AddListener((GameObject gameObject) =>
        {
            PlayClipAtPoint(deathSounds, playerController.transform.position, MasterVolume);
        });
    }

    public void PlayClipAtPoint(AudioClip[] audioClips, Vector3 position, float volume = 1)
    {
        PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }

    public void PlayClipAtPoint(AudioClip audioClip, Vector3 position, float volume = 1)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
