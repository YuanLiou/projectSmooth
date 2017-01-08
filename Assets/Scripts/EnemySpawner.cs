using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public GameObject enemy;    // 敵人 prefabs
	float maxSpawnRateInSecond = 5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 生產怪物 Method
	void SpawnEnemy() {
		// 左邊底部
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		// 右邊頂部
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		// 生產
		GameObject enemy01 = (GameObject)Instantiate (enemy);
		enemy01.transform.position = new Vector2 (Random.Range(min.x, max.x), max.y);

		NextSpawnSheduler ();
	}

	// 怪物製造機排程器
	void NextSpawnSheduler() {
		float spawnInNSec;
		if ( maxSpawnRateInSecond > 1f ) {
			// 從隨機秒數到一秒之間選一個數字
			spawnInNSec = Random.Range (1f, maxSpawnRateInSecond);
		} else {
			spawnInNSec = 1f;
		}
		Invoke ("SpawnEnemy", spawnInNSec);
	}

	// 增加難度
	void IncreseSpawnRate() {
		if ( maxSpawnRateInSecond > 1f ) {
			maxSpawnRateInSecond--;
		}

		if ( maxSpawnRateInSecond == 1f ) {
			CancelInvoke ("IncreseSpawnRate");
		}
	}

	// 生成怪物
	public void StartEnemySpawn() {
		maxSpawnRateInSecond = 5f;    // 重設生怪時間
		Invoke ("SpawnEnemy", maxSpawnRateInSecond);
		// 每30秒減少怪物重造時間(PS:第一個是方法名，第二個是「第一次調用」要隔幾秒，第三個是「每隔幾秒調用一次」)
		InvokeRepeating("IncreseSpawnRate", 0f, 30f);
	}

	// 停止生成怪物
	public void StopEnemySpawn() {
		CancelInvoke ("SpawnEnemy");
		CancelInvoke ("IncreseSpawnRate");
	}

}
