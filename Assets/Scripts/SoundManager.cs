using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Volume")]
    [SerializeField] private float _fireSoundsVolume = 1.0f;
    [SerializeField] private float _damageSoundsVolume = 1.0f;
    [SerializeField] private float _deathSoundsVolume = 1.0f;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] fireSounds;
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip[] deathSounds;

    private void Start()
    {
        var playerController = PlayerController.Instance;

        playerController.OnFireProjectile.AddListener(() =>
        {
            PlayClipAtPoint(fireSounds, playerController.transform.position, _fireSoundsVolume);
        });

        playerController.onTakeDamage.AddListener(() =>
        {
            PlayClipAtPoint(damageSounds, playerController.transform.position, _damageSoundsVolume);
        });

        playerController.onDead.AddListener((GameObject gameObject) =>
        {
            PlayClipAtPoint(deathSounds, playerController.transform.position, _deathSoundsVolume);
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
