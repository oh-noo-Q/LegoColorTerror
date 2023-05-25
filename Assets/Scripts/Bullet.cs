using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LineRenderer beam;
    public ParticleSystem startFX;
    public ParticleSystem endFX;

    public void Initialize(Vector3 startPos, Vector3 endPos)
    {
        beam.SetPosition(0, startPos);
        beam.SetPosition(1, endPos);
        startFX.transform.position = startPos;
        endFX.transform.position = endPos;
        beam.gameObject.SetActive(true);
        startFX.gameObject.SetActive(true);
        endFX.gameObject.SetActive(true);
    }
}
