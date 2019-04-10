using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {

	public GameObject gameControl, rotationButton;

	//画面下にオブジェクトが触れたらゲームオーバー
	void OnTriggerEnter2D(){
		gameControl.GetComponent<GameControl> ().GameResult();
		rotationButton.SetActive (false);
	}
}
