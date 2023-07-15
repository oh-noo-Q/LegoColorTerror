using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObjectManager : MonoBehaviour
{
    public Movement currentMovementTarget;
    public GameObject targetPrf;

    public void ActiveMovementTarget()
    {
        currentMovementTarget.SetTarget(targetPrf);
    }

    public void SetMoveObjectTarget(Movement moveObject)
    {
        currentMovementTarget = moveObject;
    }
}
