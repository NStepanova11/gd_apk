using System.Collections;
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
    private static int gameScore = 0; //загужать из файла
    private static int recordScore = 0; //загружать из файла
    private static int livesScore = 3; //загружать из файла
	private int timeLimit=31; 
    private static int timeBall;
    private string recordFileName = "record.txt";
    private static int startRecord=0;

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
        startRecord = recordScore;
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
        if (timeBall>25)
            livesScore++;
    }

    public int GetGameScore()
    {
        return gameScore;
    }

    public void UpdateRecord()
    {
        if (gameScore>recordScore);
        {
            recordScore = gameScore;
        }

        if (recordScore>startRecord)
        {
            startRecord=recordScore;
            SaveRecord();
        }
    }

    public void SaveRecord()
    {
            if (File.Exists(recordFileName) != true) {  //проверяем есть ли такой файл, если его нет, то создаем
                using (StreamWriter sw = new StreamWriter(new FileStream(recordFileName, FileMode.Create, FileAccess.Write))) {
                    sw.WriteLine(recordScore);             //пишем строчку, или пишем что хотим
                }
            } else {                              //если файл есть, то откываем его и пишем в него 
                using (StreamWriter sw = new StreamWriter(new FileStream(recordFileName, FileMode.Truncate, FileAccess.Write))) {
                    (sw.BaseStream).Seek(0, SeekOrigin.End);         //идем в конец файла и пишем строку или пишем то, что хотим
                    sw.WriteLine(recordScore);
                }
        }
    }

    public void ReadRecord()
    {
        string recString;
        if (File.Exists(recordFileName) == true)
        {
            using (StreamReader r = new StreamReader(recordFileName))
            {
                recString =  r.ReadLine();  
            }
            recordScore = int.Parse(recString);
        }
    }

    private string gameStatusFile = "gameStatus.txt";
    public void SaveGameStatus()
    {
        if (File.Exists(gameStatusFile) != true) {  
                using (StreamWriter sw = new StreamWriter(new FileStream(gameStatusFile, FileMode.Create, FileAccess.Write))) {
                    sw.WriteLine(gameScore);
                    sw.WriteLine(livesScore);
                    sw.WriteLine(currentSceneNumber);
                }
            } 
        else 
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(gameStatusFile, FileMode.Truncate, FileAccess.Write))) {
                (sw.BaseStream).Seek(0, SeekOrigin.End);
                sw.WriteLine(gameScore);
                sw.WriteLine(livesScore);
                sw.WriteLine(currentSceneNumber);
            }
        }
    }

    public void GetLastGameStatus()
    {
        List<string> lines = new List<string>();
        if (File.Exists(gameStatusFile) == true){
            using (StreamReader r = new StreamReader(gameStatusFile))
            {
                while(!r.EndOfStream)
                {
                    lines.Add(r.ReadLine());
                }
                gameScore = int.Parse(lines[0]);
                livesScore = int.Parse(lines[1]);
                currentSceneNumber = int.Parse(lines[2]);
            }
        }
    }

    public int GetLevel()
    {
        return currentSceneNumber+1;
    }

    public int GetLives()
    {
        return livesScore;
    }

    public int GetRecord()
    {
        Debug.Log(currentSceneNumber+"---"+recordScore);
        return recordScore;
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
        Application.Quit();
		Debug.Log("Exit pressed!");
    }

    public void PlayNewGame()
    {
        SetDefaultGameValues();
        LoadCurrentLevelScene();
    }


    public void PlaySavedGame()
    {
        GetLastGameStatus();
        LoadCurrentLevelScene();
    }
    
    public void LoadCurrentLevelScene()
    {
        if (currentSceneNumber<sceneCount)
            SceneManager.LoadScene(sceneNames[currentSceneNumber]);
        else
            {
                int lastScore = gameScore;
                SetDefaultGameValues();
                SceneManager.LoadScene("CongratScene");
            }
    }

    public void SetDefaultGameValues()
    {
        gameScore = 0;
        livesScore = 3;
        currentSceneNumber=0;
    }

    public void LoadWinScene()
    {
        currentSceneNumber++;
        SaveGameStatus();
        SceneManager.LoadScene("WinScene"); //mainMenuScene
    }

    public void LoadLoseScene()
    {
        SaveGameStatus();
        SceneManager.LoadScene("LoseScene");
    }


    public void LoadGameOverScene()
    {
        int lastScore = gameScore;
        SetDefaultGameValues();
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


    public void DeleteOneLife()
    {
        livesScore--;
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
}
