using strange.extensions.mediation.impl;
using UnityEngine;

namespace net.onur.brick.views.cameracontroller
{
	public class CameraController : View
	{

		[SerializeField]private Transform brickTransform;
		private Transform _cameraTransform;


		//private Tween boxTween;
		// Use this for initialization
		protected override void Start()
		{
			base.Start();
			_cameraTransform = transform;
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			if (brickTransform == null || !(brickTransform.position.y > _cameraTransform.position.y)) return;
			
			_cameraTransform.position = Vector3.Lerp(
					_cameraTransform.position,
					new Vector3(0,brickTransform.position.y,-10f),
					0.5f);
		}
	}
}