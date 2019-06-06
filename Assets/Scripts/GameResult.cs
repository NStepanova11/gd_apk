﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    public Text scoreText;
    public Text recText;
    public GameController gameController;

    void Start()
    {
       // Debug.Log("record: "+gameController.GetRecord());
        recText.text = gameController.GetRecord().ToString();
        scoreText.text = gameController.GetGameScore().ToString();
    }
}
