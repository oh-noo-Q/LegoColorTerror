using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberController : MonoBehaviour
{
    public List<GameObject> numbers;
    public List<Transform> numberPos;

    public void ActiveNumber(int number, LegoColor color)
    {
        DisableAll();
        if (number <= 0) return;
        List<int> digits = new List<int>();
        while(number > 0)
        {
            digits.Add(number % 10);
            number = number / 10;
        }
        for(int i = 0; i < digits.Count; i++)
        {
            int index = (digits.Count - i - 1) * 10 + digits[i] - 1;
            numbers[index].transform.SetParent(numberPos[i]);
            numbers[index].transform.localPosition = Vector3.zero;
            numbers[index].GetComponent<Renderer>().material = GameManager.Instance.colorDic[color];
            numbers[index].SetActive(true);
                    
        }
    }

    public void DisableAll()
    {
        foreach(GameObject number in numbers)
        {
            number.SetActive(false);
        }
    }
}
