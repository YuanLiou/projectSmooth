using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {
	public GameObject enemyBullets;    // 敵人子彈 prefab
	public GameObject enemyObject;    // 本體
	// Use this for initialization
	void Start () {
		// 1 秒後發射子彈
		Invoke ("EnemyFireBullets", 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 射擊子彈
	void EnemyFireBullets() {
		// 主角機 reference
		GameObject playerAirplane = GameObject.Find ("AirCraft");
		if (playerAirplane != null) {
			enemyObject.GetComponent<EnemyController> ().animationChooser ("shooting");

			// 玩家還沒陣亡
			GameObject bullets = (GameObject)Instantiate (enemyBullets);
			// 玩家還沒陣亡
			GameObject bullets2 = (GameObject)Instantiate (enemyBullets);
			// 玩家還沒陣亡
			GameObject bullets3 = (GameObject)Instantiate (enemyBullets);

			// 設定子彈位置
			bullets.transform.position = transform.position;
			bullets2.transform.position = transform.position;
			bullets3.transform.position = transform.position;

			// 計算主角機位置
			Vector2 direction = playerAirplane.transform.position - bullets.transform.position;

			// 設定子彈方向
			bullets.GetComponent<EnemyBullets> ().setBulletsDirection (direction);
			bullets2.GetComponent<EnemyBullets> ().setBulletsDirection (new Vector2(direction.x + 2, direction.y));
			bullets3.GetComponent<EnemyBullets> ().setBulletsDirection (new Vector2(direction.x - 2, direction.y));
		}
	}

}
