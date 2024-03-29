﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    private static List<string> sceneNames = new List<string>();
    private static int sceneCount;
    private static int currentSceneNumber=0;
    private static int gameScore = 0; 
    private static int recordScore = 1000; 
    private static int livesScore = 3; 
	private int timeLimit=31; 
    private static int timeBall;
    private static bool gameWasOver = false;
    private List<string> notLevelScenes = new List<string>{
        "MainMenuScene",
        "WinScene",
        "LoseScene",
        "HelpScene",
        "GameOverScene",
        "CongratScene"
    };
    
    void Start()
    {
        InitSceneNames();
        ReadRecord(); 

        if (gameWasOver)
        {
            SetDefaultGameValues();
            gameWasOver = false;
        }
    }

   
    public int GetLevel()
    {
        return currentSceneNumber+1;
    }

    public void InitSceneNames()
    {
        sceneCount = SceneManager.sceneCountInBuildSettings;     
        for( int i = 0; i < sceneCount; i++ )
        {
            sceneNames.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex( i )));
        }

        for(int j=0; j<notLevelScenes.Count; j++)
        {
            sceneNames.Remove(notLevelScenes[j]);
        }
        sceneCount-=notLevelScenes.Count;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ExitButton()
    {
        SaveGameStatus();
        Application.Quit();
    }

    public void PlayNewGame()
    {
        SetDefaultGameValues(); 
        LoadCurrentLevelScene();
    }

    public void LoadCurrentLevelScene()
    {
        if (currentSceneNumber<sceneCount)
            SceneManager.LoadScene(sceneNames[currentSceneNumber]);
        else
            {
                int lastScore = gameScore; 
                SceneManager.LoadScene("CongratScene");
            }
    }

    public void LoadWinScene()
    {
        currentSceneNumber++;
        SaveGameStatus(); 
        SceneManager.LoadScene("WinScene"); 
    }

    public void LoadLoseScene()
    {
        SaveGameStatus();
        SceneManager.LoadScene("LoseScene");
    }


    public void LoadGameOverScene()
    {
        int lastScore = gameScore; 
        //SetDefaultGameValues(); 
        gameWasOver=true;
        SaveGameStatus(); 
        SceneManager.LoadScene("GameOverscene");
    }

    public void LoadHelpScene()
    {
        SceneManager.LoadScene("HelpScene");
    }

    public int GetCurrentLevelNumber()
    {
        return currentSceneNumber;
    }

    private float[, ] coords;

    public float[,] GetShapeCoords()
    {
        switch (currentSceneNumber)
        {
            case 0:
            {
                coords = new float[,]{
                    { 0f, 0f }, { 0f, 2f }, { -1f, -1f }, { -1f, 1f }, { 2f, -2f }, 
		            { 0f, -2f }, { 2f, 0f }, { -2f, 2f }, { -2f, -2f },{ -2f, 0f },
                    { 1f, -1f }, { 1f, 1f }, { 2f, 2f }
                };
                break;
            }   
            case 1:
            {
                coords = new float[,] { 
                    { -2f, 2f }, { -2f, 0.5f }, { -2f, -1f }, { -0.7f, 1f }, { -0.7f, -0.5f }, 
                    { -0.7f, -2f }, { 0.7f, 2f }, { 0.7f, 0.5f }, { 0.7f, -1f }, {2f, 1f },
                    { 2f, -0.5f }, { 2f, -2f } 
                };
                break;
            } 
            case 2:
            {
                coords = new float[,] {
                    {-2f, 2f}, {-2f, 0.5f}, {-2f, -1f}, {-0.7f, 1f}, {-0.7f, -0.5f}, 
                    {-0.7f, -2f}, {0.7f, 1f}, {0.7f, -0.5f}, {0.7f, -2f},
                    {2f, 2f}, {2f, 0.5f}, {2f, -1f} 
                };
                break;
            }
            case 3:
            {
                coords = new float[,] {
                    { 0f, 0f }, { 0f, 1.5f }, { -1.5f, -0f }, { -2f, -2f }, { 2f, 2f }, 
                    { 1.5f, 0f }, { 0f, -1.5f }, { -2f, 2f }, { 2f, -2f }
                };
                break;
            }
            case 4:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -2f, 0.5f }, { -2f, -1f }, { -0.7f, 1f }, { -0.7f, -0.5f }, 
                    { -0.5f, -2f }, { 0.5f, 2f }, { 0.7f, 0.5f }, { 0.7f, -1f },
                    { 2f, 1f }, { 2f, -0.5f }, { 2f, -2f }
                };
                break;
            }
            case 5:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -2f, 0f }, { -2f, -2f }, { -0.7f, 1f }, { -0.7f, -1f }, 
                    { 0.7f, 1f }, { 0.7f, -1f },
                    { 2f, 2f }, { 2f, 0f }, { 2f, -2f }
                };
                break;
            }
            case 6:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -2f, 0f }, { -2f, -2f }, { -0.7f, 1f }, { -0.7f, -1f }, 
                    { 0.7f, 1f }, { 0.7f, -1f },
                    { 2f, 2f }, { 2f, 0f }, { 2f, -2f }
                };
                break;
            }
            case 7:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -2f, 0f }, { -2f, -2f }, { -0.7f, 1f }, { -0.7f, -1f }, 
                    { 0.7f, 1f }, { 0.7f, -1f },
                    { 2f, 2f }, { 2f, 0f }, { 2f, -2f } 
                };
                break;
            }
            case 8:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -1f, 1f }, { 1f, 1f }, 
                    { 2f, 0f }, { 2f, 2f } ,{ -1f, -1f }, { -2f, -2f },
                    {-2f, 0f}, {1f, -1f}, {2f, -2f}
                };
                break;
            }
            case 9:
            {
                coords = new float[,] {
                    { -2f, 2f }, { -1f, 1f }, { 1f, 1f }, 
                    { 2f, 0f }, { 2f, 2f } ,{ -1f, -1f }, { -2f, -2f },
                    {-2f, 0f}, {1f, -1f}, {2f, -2f}
                };
                break;
            }
        }
        return coords;
    } 

    private int[] shapeIndexes;
    public int[] GetPosIndexes()
    {
        switch(currentSceneNumber)
        {
            case 0:
            {
                shapeIndexes = new int[]{0,0,0,0,0,1,1,1,1,2,2,2,2};
                break;
            }
            case 1:
            {
                shapeIndexes = new int[]{0,0,1,1,1,1,1,2,2,2,2,2};
                break;
            }
            case 2:
            {
                shapeIndexes = new int[]{0,0,0,0,1,1,1,1,1,2,2,2};
                break;
            }
            case 3:
            {
                shapeIndexes = new int[]{0,0,0,1,1,1,1,2,2};
                break;
            }
            case 4:
            {
                shapeIndexes = new int[]{0,0,0,0,1,1,2,2,2,3,3,3};
                break;
            }
            case 5:
            {
                shapeIndexes = new int[]{0,0,1,1,1,2,2,2,2,3};
                break;
            }
            case 6:
            {
                shapeIndexes = new int[]{0,0,1,1,1,2,2,2,2,3};
                break;
            }
            case 7:
            {
                shapeIndexes = new int[]{0,0,1,1,1,2,2,2,2,3};
                break;
            }
            case 8:
            {
                shapeIndexes = new int[]{0,0,0,1,1,2,3,3,3,3};
                break;
            }
            case 9:
            {
                shapeIndexes = new int[]{0,0,0,1,1,2,3,3,3,3};
                break;
            }
        }
        return shapeIndexes;
    }

    public void DeleteOneLife()
    {
        livesScore--;
    }

    public void SetDefaultGameValues()
    {
        gameScore = 0;
        livesScore = 3;
        currentSceneNumber=0;
    }

     public void UpdateTimer()
    {
        timeLimit--;
        timeBall=timeLimit;
        if (timeLimit==0)
        {
            DeleteOneLife();
            if(livesScore>0)
            {
                LoadLoseScene();
            }
            else
            {
                LoadGameOverScene();    
            }
        }
    }

    public int GetTimeLimit()
    {
        return timeLimit;
    }

    public void UpdateGameScore()
    {
        gameScore+=100;
        gameScore+=timeBall;
        if (timeBall>27)
            livesScore++;
    }

    public int GetGameScore()
    {
        return gameScore;
    }

    public int GetLives()
    {
        return livesScore;
    }

    private string purpose;
    public string GetLevelPurpose(int[] posIndexes)
    {
        int shapeCount=0;

        for(int i=0; i<posIndexes.Length; i++)
        {
            if(posIndexes[i]==0)
                shapeCount++;
        }

        return "Найти 1 из "+shapeCount+" фигур одного размера";
    }

    public void SaveGameStatus()
    {
        if (gameWasOver)
        {
            PlayerPrefs.SetInt("gameScore", 0);
            PlayerPrefs.SetInt("livesScore", 3);
            PlayerPrefs.SetInt("currentSceneNumber", 0);
        }  
        else 
        {
            PlayerPrefs.SetInt("gameScore", gameScore);
            PlayerPrefs.SetInt("livesScore", livesScore);
            PlayerPrefs.SetInt("currentSceneNumber", currentSceneNumber);
        }
    }

    public void PlaySavedGame()
    {
       if(PlayerPrefs.HasKey("gameScore") && PlayerPrefs.HasKey("livesScore")  && PlayerPrefs.HasKey("currentSceneNumber"))
       {
            gameScore = PlayerPrefs.GetInt("gameScore");
            livesScore = PlayerPrefs.GetInt("livesScore");
            currentSceneNumber = PlayerPrefs.GetInt("currentSceneNumber");
       } 
       
       if (currentSceneNumber>=sceneCount)
            SetDefaultGameValues();
       LoadCurrentLevelScene();
    }

    public int GetRecord()
    {
        return recordScore;
    }

    public void UpdateRecord()
    {
        if (gameScore>recordScore)
        {
            recordScore = gameScore;
            SaveRecord();
        }
    }

    public void SaveRecord()
    {
        PlayerPrefs.SetInt("recordScore", recordScore);
    }

    public void ReadRecord()
    {
        if(PlayerPrefs.HasKey("recordScore"))
            recordScore = PlayerPrefs.GetInt("recordScore");
    }
}