using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LegoColor
{
    BLUE,
    GREEN,
    RED,
    PINK,
    PURPLE
}

public class LegoEnemy : MonoBehaviour
{
    public GameObject[] pieces;
    public LegoColor mainColor;

    public Vector3 moveDirection;
    public float speed;

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}

