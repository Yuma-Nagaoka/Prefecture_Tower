using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {


	public bool usingButton = false; //マウスオーバーに起因する変数
	public bool freezeEvent = false;//関数AddGravityに起因する変数
	public GameObject[] prefecture;//すべてのオブジェクトの参照変数が配列に登録されている
	GameObject activePre;//アクティブなオブジェクトの参照変数
	public int count = 0;//積んだオブジェクトの数
	public Text resultScore;
	public GameObject result;
	int number;//ランダムな数値
	float create_y = 1.32f;//オブジェクトが生成されるy座標
	public GameObject cameraObj;
	private Camera mainCamera;
	public GameObject button;
	public int gameStatus = 0;
	public GameObject backGround;

	private void Start()
	{
		mainCamera = cameraObj.GetComponent<Camera> ();//カメラオブジェクトの取得
		number = Random.Range (0, prefecture.Length);//0-46までのランダムな数を取得
		//オブジェクトを生成
		activePre = Instantiate(prefecture[number]) as GameObject;
		count++;
	}
	// Update is called once per frame
	void Update () 
	{
		//オブジェクトが静止したら次のオブジェクトを生成
		if (activePre.GetComponent<Rigidbody2D> ().IsSleeping () && freezeEvent && gameStatus == 0) {
			
			//生成位置のy座標と積みあがったオブジェクトのy座標との差が5より小さいとき生成位置を変更する
			if (create_y - activePre.transform.position.y < 4f) {
				create_y += 2f;
			}

			//次のオブジェクトを生成
			number = Random.Range (0, prefecture.Length);
			activePre = Instantiate(prefecture[number], new Vector3(0f, create_y, 0f), Quaternion.identity) as GameObject;
			count++;
			freezeEvent = false;

			//生成するオブジェクトの上端のy座標がカメラの上端の座標より大きいときにカメラを上に移動させる
			if ((create_y + activePre.GetComponent<SpriteRenderer>().bounds.size.y / 2) > -mainCamera.ScreenToWorldPoint (Vector3.zero).y) {
				cameraObj.transform.position += new Vector3(0f, 1f, 0f);
				backGround.transform.position += new Vector3(0f, -1f, 0f);//当たり判定は場所を維持
			}
		}
	}

	//マウスの位置にオブジェクトを移動させる
	public void Transport () {
		if(usingButton) return;
		if(freezeEvent) return;

		Vector3 mousePos = Input.mousePosition; 
		mousePos.z = 0f; 
		Vector3 objPos = Camera.main.ScreenToWorldPoint (mousePos); 
		activePre.transform.position = new Vector3 (objPos.x, create_y, 0);
	}
	//オブジェクトに重力を与える
	public void AddGravity () {
		if(usingButton) return;
	
		activePre.GetComponent<Rigidbody2D> ().gravityScale = 0.8f;
		freezeEvent = true;
	}
	//オブジェクトを回転させる
	public void Rotation () {
		if(freezeEvent) return;

		// z軸を軸として45°回転
		activePre.transform.Rotate (new Vector3 (0f, 0f, -45f)); 
	}
	//ゲームリザルト
	public void GameResult(){
		gameStatus = -1;
		result.SetActive (true);
		resultScore.text = "Score:" + (count - 1);
	}
	//回転ボタンにマウスオーバーしたとき、オブジェクトの移動を不可にする
	public void Enter(){
		usingButton = true;
	}
	//回転ボタンからマウスアウトしたとき、オブジェクトの移動を可にする
	public void Exit(){
		usingButton = false;
	}
	//コンティニューする
	public void OnContinue () {
		SceneManager.LoadScene (
			SceneManager.GetActiveScene().name);
	}
}
