using DG.Tweening;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] private float duration = .15f;
    [SerializeField] [Range(0f, 10f)] private float strength = 3f;
    [SerializeField] [Range(0, 10)] private int vibrato = 10;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void Shake()
    {
        mainCamera.DOShakePosition(duration, strength, vibrato);
        mainCamera.DOShakeRotation(duration, strength, vibrato);
    }
}
