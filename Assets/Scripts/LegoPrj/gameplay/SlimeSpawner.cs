using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    [SerializeField] SlimeMove slimePrf;
    [SerializeField] float spaceSize;
    [SerializeField] Transform leftLimit, rightLimit;
    [SerializeField] Transform destination;

    float xLeftLimit, xRightLimit;

    private float speedSlime = 10.0f;
    private float delaySpawn = 10.0f;
    private float deltaTime = 0;
    private int amountSlime = 10;
    private int countSlime = 0;
    private void Awake()
    {
        xLeftLimit = leftLimit.position.x;
        xRightLimit = rightLimit.position.x;
    }

    private void Update()
    {
        if (amountSlime <= 0) return;
        if (!GameManager.Instance.waitTimeGame || !GameManager.Instance.startGame || countSlime > amountSlime) return;
        deltaTime += Time.deltaTime;
        if(deltaTime > delaySpawn)
        {
            deltaTime = 0;
            countSlime++;
            GenerateSlime();
        }
    }

    void GenerateSlime()
    {
        SlimeMove slime = Instantiate(slimePrf, transform);
        slime.gameObject.SetActive(true);
        switch (GameManager.Instance.mapSpawner.axis)
        {
            case AXIS.Zpositive:
                slime.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                break;
            case AXIS.Znegative:
                slime.transform.position = new Vector3(Random.Range(xLeftLimit, xRightLimit), 0, -spaceSize);
                break;
        }
        slime.moveDirection = - GameManager.Instance.mapSpawner.moveDirection;
        slime.destination = destination;
        slime.speed = speedSlime;
    }

    public void LoadLevel(float _speed, int amount, float spawnTime)
    {
        speedSlime = _speed;
        delaySpawn = spawnTime;
        amountSlime = amount;
        Debug.Log($"speed: {speedSlime}, spawn: {delaySpawn}, amount: {amountSlime}");
    }

}
