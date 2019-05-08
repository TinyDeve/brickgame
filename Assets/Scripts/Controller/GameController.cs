using System.Collections.Generic;
using net.onur.brick.controller.brick;
using net.onur.brick.models.game;
using strange.extensions.mediation.impl;
using SG;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace net.onur.brick.views.gamecontroller
{
	public class GameController : View
	{
		[SerializeField]private Camera followCamera;
		[SerializeField]private Transform followBrick;
		[SerializeField]private float screenWidth;
		[SerializeField]private float cubeScale = 1;
		[SerializeField]private int lastObjectPlacement;
		
		//UIS
		[SerializeField]private GameObject gameUi;
		[SerializeField]private GameObject killUi;
		[SerializeField]private GameObject homeUi;
		[SerializeField]private GameObject stopUi;
		
		//GameObjects
		[SerializeField]private GameObject brick;
		
		//Score UI
		[SerializeField]private TextMeshProUGUI scoreText;
		[SerializeField]private TextMeshProUGUI bestText;
		
		//Controller
		[SerializeField]private BrickController brickController;

		[Range(0, 1)] 
		[SerializeField]private float spacePercentage;

		[Range(0, 0.5f)] 
		[SerializeField]private float spacePosition;
		
		[Range(0, 0.5f)] 
		[SerializeField]private float cubeObstaclePosition;
		
		[SerializeField]private int heightDifference = 10;
		[SerializeField]private int changeColorPer = 2;

		[SerializeField] private GameObject _leftWall;
		[SerializeField] private GameObject _rightWall;


		private int _resetTime =0;
		private int _againTime = 0;
		
		//Models
		[Inject]
		public GameModel GameModel { get; set;}
		
		private float _obstacleLength;
		private float _aspectRatio;
		private float _spaceBetween = 2;
		
		//Scores
		private int _score;
		private int _bestScore;

		private List<Color> _colors;

		private const string PoolName = "Obstacle";
		
		// Use this for initialization
		protected override void Start()
		{
			_resetTime = 0;
			_aspectRatio = (float) Screen.height / (float) Screen.width;
			screenWidth = (followCamera.orthographicSize / _aspectRatio) * 2;
			_obstacleLength = screenWidth;
			_spaceBetween = screenWidth * spacePercentage;

			_colors = GameRoot.Instance.ColorConfig.AllObstacleColor;
			
			ResourceManager.Instance.InitPool(PoolName,40);
			
			PositionWalls();
		}

		private void PositionWalls()
		{
			var width = screenWidth  + _leftWall.transform.localScale.x;
			width /= 2;
			
			_leftWall.transform.localPosition = new Vector3(-width,0,0);
			_rightWall.transform.localPosition = new Vector3(width,0,0);
		}
		
		private void ReStart()
		{
			_resetTime += 1;
			Debug.Log("Restart" + _resetTime);
			
			_score = 0;
			scoreText.text = "0";
			
			
			_bestScore = GameModel.Score;
			bestText.text = _bestScore.ToString();
			
			
			//camera
			followCamera.transform.position = new Vector3(0,0,-10);
			//follow brick object
			followBrick.position = new Vector3(0,-5,0);
			

			//Pool all remain objects
			SendObjectsToPool();
			
			//Create New Pattern
			for (var i = 0; i < 10; i++)
			{
				CreateLongObstacles(i * heightDifference);
			}
			
			
			//Reset ball Controller Values
			brickController.ResetValues();
		}

		private void SendObjectsToPool()
		{
			while(0 < gameObject.transform.childCount)
			{
				var obstacles = gameObject.transform.GetChild(0).gameObject;
				ResourceManager.Instance.ReturnObjectToPool(obstacles);
			}
		}


		public void Play()
		{
			homeUi.SetActive(false);
			gameUi.SetActive(true);
			
			//Invoke("ReStart",0.1f);
			ReStart();
		}

		public void BrickKilled()
		{
			killUi.SetActive(true);
			gameUi.SetActive(false);
			
			brickController.gameMode = BrickController.GameMode.NOGAME;
			
			GameModel.Save();
			
		}

		public void Pause()
		{
			gameUi.SetActive(false);
			stopUi.SetActive(true);

			FreezeGame(true);

		}


		public void Resume()
		{
			gameUi.SetActive(false);
			stopUi.SetActive(true);	
			
			FreezeGame(false);
		}
		
		
		private void FreezeGame(bool freeze)
		{
			
			brickController.FreezeBall(freeze);
			
			if (freeze)
			{
				
			}
			else
			{
				
			}
		}
		
		public void Again()
		{
			if (brickController.gameMode == BrickController.GameMode.PLAY) return; 
			
			_againTime += 1;
			killUi.SetActive(false);
			gameUi.SetActive(true);

			brickController.gameMode = BrickController.GameMode.PLAY;
			
			Debug.Log("Again" + _againTime);
			
			ReStart();
			
		}

		public void Home()
		{
			killUi.SetActive(false);
			gameUi.SetActive(true);

			brickController.gameMode = BrickController.GameMode.STOP;
		}
		
		private void Update()
		{
			if (!(followCamera.transform.position.y > _score * heightDifference)) return;
			
			_score += 1;
			scoreText.text = _score.ToString();
			CreateLongObstacles(lastObjectPlacement+heightDifference);
			
			if (_score <= _bestScore) return;
			
			_bestScore = _score;
			bestText.text = _bestScore.ToString();
			GameModel.Score = _bestScore;
			GameModel.Save();
		}

		#region Place Game Objects
		private void CreateLongObstacles(int yPoint)
		{
			lastObjectPlacement = yPoint;
			var obstacleLeft = ResourceManager.Instance.GetObjectFromPool(PoolName);
			obstacleLeft.transform.parent = transform;
			var obstacleRight =ResourceManager.Instance.GetObjectFromPool(PoolName);
			obstacleRight.transform.parent = transform;

			ColorObstacles(obstacleLeft, yPoint);
			ColorObstacles(obstacleRight, yPoint);
			
			PlaceLongObstacles(yPoint, obstacleLeft, obstacleRight);
		}

		private void PlaceLongObstacles(int yPoint, GameObject obstacleLeft, GameObject obstacleRight)
		{

			var xPivotRange = screenWidth - screenWidth * 2 * spacePosition - _spaceBetween ;
			var xPivot = Random.Range(-xPivotRange / 2, xPivotRange/2);

			var positionChange = (_obstacleLength / 2) + _spaceBetween/2;

			obstacleLeft.transform.localScale = new Vector3(_obstacleLength, 1, 0);
			obstacleRight.transform.localScale = new Vector3(_obstacleLength, 1, 0);

			obstacleLeft.transform.position = new Vector3(xPivot - positionChange, yPoint, 0);
			obstacleRight.transform.position = new Vector3(xPivot + positionChange, yPoint, 0);
			
			PlaceCubeObstacle(xPivot,yPoint,1);
			if(yPoint == 0) return;
			PlaceCubeObstacle(xPivot,yPoint,-1);

		}

		private void PlaceCubeObstacle(float xPivot,float yPoint, int upDown)
		{
			var xStartPoint = xPivot-_spaceBetween/2;
			var extraLen = 2;
			xStartPoint += Mathf.Abs(xStartPoint * cubeObstaclePosition + cubeScale/2);
			var xCubePivotRange = _spaceBetween - _spaceBetween * 2 * cubeObstaclePosition - cubeScale;
			var xCubePivot = Random.Range(0, xCubePivotRange);
			
			//Debug.Log(xStartPoint.ToString());

			var position = new Vector3(xCubePivot+xStartPoint, yPoint+extraLen*upDown, 0);

			var cubeObstacle = ResourceManager.Instance.GetObjectFromPool(PoolName);
			
			ColorObstacles(cubeObstacle, (int)yPoint);
			
			cubeObstacle.transform.localScale = new Vector3(cubeScale,cubeScale,1);
			cubeObstacle.transform.parent = transform;
			cubeObstacle.transform.position = position;
			
			//GameObject gameObjectUp = Instantiate(brick, new Vector3(xCubePivot+xStartPoint, yPoint+extraLen*upDown, 0), Quaternion.identity, transform);
		}

		private void ColorObstacles(GameObject obstacle,int yPoint)
		{
			var stage = yPoint/(heightDifference*changeColorPer);
			stage = stage % _colors.Count;

			obstacle.transform.GetChild(0).transform.gameObject.GetComponent<SpriteRenderer>().color =
				_colors[stage];

		}
#endregion
	}
}
