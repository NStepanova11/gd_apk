using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvas_1 : MonoBehaviour
{
    public Shape shape; 
    public Sprite[] shapeType_1;
	public Sprite[] shapeType_2;
	public Sprite[] shapeType_3;
    private Sprite[] images;
	private int shapeType;

	public GameController gameController;
	public GameObject levelObject;
	private int[] posIndexes;
	private float[,] pos;

	private string levelPurpose;
	public Text purposeText;
	public Text scoreText;
	public Text recText;
	public Text levelTitle;
	public Text livesText;
	public Text timerText;
	public Image timerPanel;
	public bool isChangeAngle;
	public bool isChangeColor;
	private int[] rotateAngles = {45, 90, 135, 180};

	void Start()
	{
		timerPanel =  GameObject.Find("TimerPanel").GetComponent<Image>();
		posIndexes = gameController.GetPosIndexes();
		pos = gameController.GetShapeCoords();
		levelPurpose = gameController.GetLevelPurpose(posIndexes);
		purposeText.text = levelPurpose;
		
		InvokeRepeating("RunTimer", 1, 1);

		GetShapeType(); 
		UpdateArrayOfImages(); 
		ShuffleCoords(); 

		for(int i=0; i<posIndexes.Length; i++)
		{
            Shape cloneShape = Instantiate(shape) as Shape;
            cloneShape.SetShape(images[posIndexes[i]]);
            cloneShape.transform.SetParent(levelObject.transform, false);
            cloneShape.transform.position = new Vector3(pos[i,0], pos[i,1], 0);
			if (isChangeAngle)
			{
				cloneShape.transform.Rotate(0, 0, getAngle());
			}
			if (isChangeColor)
			{
				Renderer rend = cloneShape.GetComponent<SpriteRenderer>();
            	rend.material.color = GetColor();
			}
			
            if(posIndexes[i]==0)
			{
				cloneShape.tag = "MainShape";
			}
			else
			{
				cloneShape.tag = "SubShape";
			}
		}
	}
	
	public void GetShapeType()
	{
		shapeType = Random.Range(0, 3);	
		if(shapeType==0)
		{
			images = shapeType_1.Clone() as Sprite[];
		}
		else if (shapeType==1)
		{
			images = shapeType_2.Clone() as Sprite[];
		}
		else if (shapeType==2)
		{
			images = shapeType_3.Clone() as Sprite[];
		}
	}
		
	public void ShuffleCoords()
	{
		for (int i = 0; i < posIndexes.Length; i++ ) 
		{
			int tmp = posIndexes[i];
			int r = Random.Range(i, posIndexes.Length);
			posIndexes[i] = posIndexes[r];
			posIndexes[r] = tmp;
		}
	}
	
	public void UpdateArrayOfImages()
	{
		Sprite subShape = images[0];
		images[0] = images[shapeType];
		images[shapeType] = subShape;	
	}

	void RunTimer() 
	{
		gameController.UpdateTimer();
		timerText.text = gameController.GetTimeLimit().ToString();
		if (gameController.GetTimeLimit()<=15 && gameController.GetTimeLimit()>3)
         	timerPanel.color = UnityEngine.Color.yellow;
		else if (gameController.GetTimeLimit()<=3) 
		    timerPanel.color = UnityEngine.Color.red;
	}

	void Update()
	{
		scoreText.text = gameController.GetGameScore().ToString();
		recText.text = gameController.GetRecord().ToString();
		levelTitle.text = "#"+gameController.GetLevel();
		livesText.text = gameController.GetLives().ToString();
	}


	public Color GetColor()
	{
		int colorType = Random.Range(0, 2);	
		if (colorType==1)
		{
			return( new Color(1f, 0.4f, 0f, 1f));
		}
		else
		{
			return( new Color(1f, 0f, 0.7f, 1));
		}
	}

	private int getAngle()
	{
		int r = Random.Range(0, rotateAngles.Length);
		return rotateAngles[r];
	}

}
