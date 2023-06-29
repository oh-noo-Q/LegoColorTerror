using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public List<GameObject> numbers;
    public Transform number1Pos, number2Pos;

    public void ActiveNumber()
    {
        DisableAll();
    }

    public void DisableAll()
    {
        foreach(GameObject number in numbers)
        {
            number.SetActive(false);
        }
    }
}
