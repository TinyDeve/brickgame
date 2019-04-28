using net.onur.brick.views.gamecontroller;
using net.onur.brick.views.soundcontroller;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace net.onur.brick.controller.brick
{
	public class BrickController : View {
		public enum GameMode
		{
			PLAY,
			NOGAME,
			STOP
		}

		public GameMode gameMode;
		
		[Header("Input Variables")] public float mTapTimeMax = 0;
		[SerializeField]private float mTapTimeWindow = 0.1f;
		[SerializeField]private float mTapMoveWindow = 2f;

		//Controller
		[SerializeField]private GameController gameController;
		
		[Space]
		//[Range(0,500)]
		[SerializeField]private int xForceAmount;

		//[Range(0,500)]
		[SerializeField]private int yForceAmount;

		[Space] [SerializeField]private bool mUseDiagnostic = false;

		private float _tapTime = 0;
		
		private Vector2 _mTapPosition = Vector3.zero;
		private Vector2 _mTouchMovement = Vector3.zero;

		private Vector2 _mForceRightVector;
		private Vector2 _mForceLeftVector;

		private Rigidbody2D _rigidBody2D;
		private Collider2D _collider2D;

		private bool _firstMove = false;
		
		// Use this for initialization
		protected override void Start()
		{
			base.Start();
			_rigidBody2D = GetComponent<Rigidbody2D>();
			_collider2D = GetComponent<Collider2D>();
			_mForceRightVector = new Vector2(xForceAmount, yForceAmount);
			_mForceLeftVector = new Vector2(-xForceAmount, yForceAmount);
		}

		// Update is called once per frame
		private void Update()
		{
			if (gameMode.Equals(GameMode.PLAY))
			{
				GetInput();
			}
		}

		public void ResetValues()
		{
			
			Debug.Log("BrickResetStart");
			_firstMove = true;
			FreezeBall(true);
			_collider2D.isTrigger = false;
			if(gameMode.Equals(GameMode.NOGAME))ChangeGameModePlay();
			
			
			_rigidBody2D.freezeRotation = true;
			_rigidBody2D.position = new Vector3(0,-5,0);
			_rigidBody2D.velocity = new Vector2(0, 0);
			_rigidBody2D.angularVelocity = 0;
			
			transform.rotation = new Quaternion(0,0,0.3826834f,0.9238796f);
			Debug.Log("BrickResetFinished");

		}


		public void ChangeGameModePlay()
		{
			if (gameMode == GameMode.PLAY) return;
			Invoke("CanPlay",0.1f);
		}

		private void CanPlay()
		{
			gameMode = GameMode.PLAY;
		}
		
		public void FreezeBall(bool freeze)
		{
			_rigidBody2D.bodyType = freeze ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Obstacle") && gameMode.Equals(GameMode.PLAY))
			{
				KillGame();
			}
		}

		public void KillGame()
		{
			Debug.Log("KillGame : game killed");
			//Change game mode
			gameMode = GameMode.NOGAME;
			
			//Start kill animation
			_collider2D.isTrigger = true;
			_rigidBody2D.freezeRotation = false;
			_rigidBody2D.AddForce(new Vector2(0,-300));
			_rigidBody2D.angularVelocity = Random.Range(100, 300);
			//Start kill sound
			GameMaster.ınstance.soundController.PlaySound(SoundController.SoundType.FAİL);

			//Change UI
			gameController.BrickKilled();
		}

		#region MoveBrick

		

		private void GetInput()
		{
			if (gameMode==GameMode.PLAY && Input.GetMouseButtonDown(0))
			{
				Move(Input.mousePosition.x > ((float) Screen.width / 2));
				
			}
			/*//Old Input
#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				Move(false);
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				Move(true);
			}
#else
			if (Input.touchCount > 0)
			{
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Began)
				{
					_tapTime = 0;
					_mTouchMovement = Vector3.zero;
					_mTapPosition = touch.position;
				}
				else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					_tapTime += Time.time;
					_mTouchMovement += touch.deltaPosition;
				}
				else if (touch.phase == TouchPhase.Ended)
				{
					if (_tapTime < mTapTimeWindow && _mTouchMovement.magnitude < mTapMoveWindow && gameMode==GameMode.PLAY)
					{
						
						Move(_mTapPosition.x > ((float) Screen.width / 2));
						//Diagnostic(m_tapPosition.magnitude.ToString(".00") + "TapOccurred");
						//Diagnostic("Tap detected "+ m_tapPosition.ToString() + " " + SwipeDiagnostic(m_tapPosition));
					}
				}
			}
#endif
*/
			
			
		}

		private void Move(bool right)
		{
			GameMaster.ınstance.soundController.PlaySound(SoundController.SoundType.TAP);
			
			
			if (_firstMove)
			{
				//Debug.Log("first");
				FreezeBall(false);
				_firstMove = false;
			}

			var velocity = _rigidBody2D.velocity;
			var forceVelocity = Vector2.Lerp(velocity, right ? _mForceRightVector : _mForceLeftVector, 0.8f);
			_rigidBody2D.velocity = new Vector2(0, 0);
			_rigidBody2D.AddForce(forceVelocity);
		}
		
		#endregion
	}
}
