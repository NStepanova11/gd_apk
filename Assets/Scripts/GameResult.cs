﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameResult : MonoBehaviour
{
    public Text scoreText;
    public Text recText;
    public Text livesText;
    public GameController gameController;

    void Start()
    {
        recText.text = gameController.GetRecord().ToString();
        scoreText.text = gameController.GetGameScore().ToString();
        livesText.text = gameController.GetLives().ToString();
    }
}
