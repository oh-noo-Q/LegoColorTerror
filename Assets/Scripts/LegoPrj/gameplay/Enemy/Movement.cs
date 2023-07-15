using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed;

    protected virtual void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    public virtual void SetTarget(GameObject targetGO)
    {

    }
}
