using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMoving : MonoBehaviour
{
    public MapSpawner mapSpawner;

    private void Update()
    {
        transform.Translate(mapSpawner.moveDirection * mapSpawner.speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        switch(mapSpawner.axis)
        {
            case AXIS.Zpositive:
                if (transform.position.z > mapSpawner.mapSize)
                {
                    mapSpawner.RestartRunMap(this);
                }
                break;
            case AXIS.Znegative:
                if (transform.position.z < -mapSpawner.mapSize)
                {
                    mapSpawner.RestartRunMap(this);
                }
                break;
        }
    }
}
