using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour
{
    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay = 0.003f;
    public float shake_intensity = .1f;
    public float shake_ratio = 0.1f;
    public bool isShaking = false;

    private float temp_shake_intensity = 0;

    void Update()
    {
        if (temp_shake_intensity > 0 && isShaking == true)
        {
            transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
            transform.rotation = new Quaternion(
                originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * shake_ratio,
                originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * shake_ratio,
                originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * shake_ratio,
                originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * shake_ratio);
            temp_shake_intensity -= shake_decay;
        }
        else
        {
            isShaking = false;
        }
        if (temp_shake_intensity <= 0) Shake();
    }

    public void Shake()
    {
        if (!isShaking)
        {
            isShaking = true;
            originPosition = transform.position;
            originRotation = transform.rotation;
            temp_shake_intensity = shake_intensity;
        }
    }
}
