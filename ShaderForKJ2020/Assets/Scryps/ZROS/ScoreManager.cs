using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Scores;
    public Text scoreText;

    private void Update()
    {
        scoreText.text = Scores + "/6";
    }

    public void Awake()
    {
        Instance = this;
    }
}
