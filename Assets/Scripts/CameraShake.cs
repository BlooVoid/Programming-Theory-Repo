using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmplitude = 1.5f;
    [SerializeField] private float shakeFrequency = 0.1f;
    [SerializeField] private float shakeTime = 0.2f;

    private CinemachineBasicMultiChannelPerlin multiChannelPerlin;


    private void Awake()
    {
        multiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>(); 
    }

    private void Start()
    {
        PlayerController.Instance.onTakeDamage.AddListener(() =>
        {
            StartCoroutine(ShakeCameraRoutine());
        });
    }

    private void ShakeCamera()
    {
        multiChannelPerlin.AmplitudeGain = shakeAmplitude;
        multiChannelPerlin.FrequencyGain = shakeFrequency;
    }

    private void StopShakeCamera()
    {
        multiChannelPerlin.AmplitudeGain = 0;
        multiChannelPerlin.FrequencyGain = 0;
    }

    private IEnumerator ShakeCameraRoutine()
    {
        ShakeCamera();
        yield return new WaitForSeconds(shakeTime);
        StopShakeCamera();
    }
}
