using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetTrans;
    [SerializeField] float smooth;
    private float offsetZ, offsetY;

    private void Start()
    {
        offsetY = transform.position.y - targetTrans.position.y;
        offsetZ = transform.position.z - targetTrans.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x, targetTrans.position.y + offsetY, targetTrans.position.z + offsetZ);
        transform.position = Vector3.Lerp(transform.position, targetPos, smooth * Time.deltaTime);
    }
}
