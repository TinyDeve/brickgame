using strange.extensions.mediation.impl;
using UnityEngine;


namespace net.onur.brick.views.followcamera
{
	public class FollowBrick : View {
		
		
		[SerializeField]private Transform brickTransform;
		private Transform _objectTransform;

		//private Tween boxTween;
		// Use this for initialization

		protected override void Awake()
		{
			base.Awake();
			_objectTransform = transform;
			_objectTransform.position = brickTransform.position;
		}

		// Update is called once per frame
		private void FixedUpdate()
		{
			if (!(brickTransform.position.y > _objectTransform.position.y)) return;
			
			_objectTransform.position = new Vector3(0, brickTransform.transform.position.y, 0);
		}
	}
}

