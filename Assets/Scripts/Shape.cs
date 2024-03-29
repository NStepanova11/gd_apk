﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shape : MonoBehaviour
{
    private Renderer renderrer;
    public GameController gameController;
    private GameObject[] goodShapes, badShapes;
    public Text timerText;
    public void OnMouseDown()
    {
        renderrer = GetComponent<SpriteRenderer>();
        renderrer.material.color = Color.yellow;
        Deactivate();
        if (renderrer.tag=="MainShape")
        {
            
            Invoke("ColorGoodShapes", 1);
            Invoke("GoToWinScene", 1.7f);
        }
        else
        {
            Invoke("ColorAllShapes", 1);
            Invoke("GoToLoseScene", 1.7f);

        }
    }

    public void Deactivate()
    {
        for (int i = 0; i < goodShapes.Length; i++)
        {
            goodShapes[i].GetComponent<CircleCollider2D>().enabled = false;
        }

        for (int j = 0; j < badShapes.Length; j++)
        {
            badShapes[j].GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void ColorGoodShapes () {
       for (int i = 0; i < goodShapes.Length; i++)
            {
                Renderer rend = goodShapes[i].GetComponent<SpriteRenderer>();
                rend.material.color = Color.green;
        }
    }

    public void GoToWinScene()
    {
        gameController.UpdateGameScore(); 
        gameController.UpdateRecord();    
        gameController.LoadWinScene();
    }

    public void ColorAllShapes()
    {
        for (int i = 0; i < goodShapes.Length; i++)
        {
            Renderer rend = goodShapes[i].GetComponent<SpriteRenderer>();
            rend.material.color = Color.red;
        }
        for (int i = 0; i < badShapes.Length; i++)
        {
            Renderer rend = badShapes[i].GetComponent<SpriteRenderer>();
            rend.material.color = Color.red;
        }
    }

    public void GoToLoseScene()
    {
        gameController.DeleteOneLife();
        if (gameController.GetLives()>0)
            gameController.LoadLoseScene();
        else
            gameController.LoadGameOverScene();
    }

    public void SetShape(Sprite image)
    {
        GetComponent<SpriteRenderer>().sprite = image;
    }

    void Start()
    {
        goodShapes = GameObject.FindGameObjectsWithTag("MainShape");
        badShapes = GameObject.FindGameObjectsWithTag("SubShape");
    }
    
}
