using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AXIS
{
    Zpositive,
    Znegative,
}
public class MapSpawner : MonoBehaviour
{
    public GameObject[] maps;

    [Space(10)]
    public int spawnAmount = 5;
    public AXIS axis;

    [Space(10)]
    public int mapSize;
    public int spaceSize;
    public Vector3 moveDirection;

    public float speed;

    GameObject lastSpawnOb;

    private void Awake()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            GameObject newMap = Instantiate(maps[0], transform);
            newMap.GetComponent<MapMoving>().mapSpawner = this;
            newMap.SetActive(true);
            switch(axis)
            {
                case AXIS.Zpositive:
                    newMap.transform.position = new Vector3(0, newMap.transform.position.y, -i * mapSize);
                    moveDirection = new Vector3(0, 0, 1);
                    break;
                case AXIS.Znegative:
                    newMap.transform.position = new Vector3(0, newMap.transform.position.y, i * mapSize);
                    moveDirection = new Vector3(0, 0, 1);
                    break;
            }
            lastSpawnOb = newMap;
        }
    }

    public void RestartRunMap(MapMoving map)
    {
        Vector3 lastPos = lastSpawnOb.transform.position;

        switch(axis)
        {
            case AXIS.Zpositive:
                lastPos.z -= mapSize;
                break;

            case AXIS.Znegative:
                lastPos.z += mapSize;
                break;
        }

        map.transform.position = lastPos;
        lastSpawnOb = map.gameObject;
    }
}
