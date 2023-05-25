using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour
{
    public Text scoreText;

    public void Init(string score)
    {
        scoreText.text = score;
    }
}
