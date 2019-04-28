
using System.Collections.Generic;
using UnityEngine;

namespace net.onur.brick.config
{
	[CreateAssetMenu(fileName = "ColorConfig", menuName = "Configs/ColorConfig", order = 1)]
	public class ColorConfig : ScriptableObject 
	{
		public List<Color> AllObstacleColor;
	}
}

