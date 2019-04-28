using System.Collections;
using System.Collections.Generic;
using net.onur.brick.controller.brick;
using SG;
using UnityEngine;

public class KillBrick : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.gameObject.CompareTag("Player")) return;
		if(other.GetComponent<BrickController>().gameMode != BrickController.GameMode.PLAY)return;
		other.GetComponent<BrickController>().KillGame();
	}
}
