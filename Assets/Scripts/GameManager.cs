using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	// 遊戲物件
	public GameObject playButton;
	public GameObject airplane;
	public GameObject enemySpawner;
	public GameObject gameOverSprite;    // GameOver Img
	public GameObject userScore;    // Score UI
	public GameObject gameTimer;    // 倒數器
	public GameObject gameTitle;    // 標題

	public enum GameStates {
		Opening, GamePlay, GameOver
	}

	GameStates gameStates;
	// Use this for initialization
	void Start () {
	}
	
	// 更新遊戲狀態
	void UpdateGameStates () {
		switch(gameStates) {
		case GameStates.GameOver:
			// 時間停止
			gameTimer.GetComponent<TimeCounter>().stopTimeCounter();
			// 停止生成怪物
			enemySpawner.GetComponent<EnemySpawner>().StopEnemySpawn();
			// 顯示 GameOver畫面
			gameOverSprite.SetActive(true);
			// 8秒後 改變遊戲狀態到 GameOpening
			Invoke("SetGameStateToOpening", 8f);
			break;
		case GameStates.GamePlay:
			// 重設分數
			userScore.GetComponent<GameScore> ().Score = 0;
			// 隱藏按鈕
			playButton.SetActive (false);
			gameTitle.SetActive (false);
			// 啟動玩家
			airplane.GetComponent<UserControll>().Init();
			// 怪物生成
			enemySpawner.GetComponent<EnemySpawner>().StartEnemySpawn();
			// 開始倒數
			gameTimer.GetComponent<TimeCounter>().startTimeCounter();

			break;
		case GameStates.Opening:
			// 停止顯示 GameOver 畫面
			gameOverSprite.SetActive (false);
			// 讓 Play 按鈕可以用
			playButton.SetActive (true);
			gameTitle.SetActive (true);
			break;
		}
	}

	// 設定狀態用 Method
	public void SetGameState(GameStates state) {
		gameStates = state;
		UpdateGameStates ();
	}

	// 按按鈕遊戲開始
	public void StartGamePlay() {
		gameStates = GameStates.GamePlay;
		UpdateGameStates ();
	}

	// 設定遊戲狀態至 GameOpening
	public void SetGameStateToOpening() {
		SetGameState (GameStates.Opening);
	}
}
