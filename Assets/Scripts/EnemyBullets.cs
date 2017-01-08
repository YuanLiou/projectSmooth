using UnityEngine;
using System.Collections;

public class EnemyBullets : MonoBehaviour {
	float speed;    // 子彈速度
	Vector2 bulletDirection;    // 子彈的方向
	bool isReady;    // 子彈方向是否已設定

	// initialize variables or game state before the game starts
	void Awake() {
		speed = 5f;
		isReady = false;
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		if ( isReady ) {
			// 取得子彈目前位置	
			Vector2 position = transform.position;
			// 設定子彈方向
			position += bulletDirection * speed * Time.deltaTime;
			// 更新位置
			transform.position = position;

			// 跑到外面消滅掉偵測, 左邊底部
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
			// 右邊頂部
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
			if ( transform.position.x > max.x || transform.position.y > max.y || 
				transform.position.x < min.x  || transform.position.y < min.y ) {
				Destroy (gameObject);
			}
		}
	}

	// 取得玩家的位置
	public void setBulletsDirection( Vector2 dir ) {
		bulletDirection = dir.normalized;
		isReady = true;
	}

	void OnTriggerEnter2D(Collider2D _collider) {
		// 偵測是否碰觸到主角機
		if ( _collider.tag == "PlayerShipTag" ) {
			Destroy (gameObject);
		}
	}

}
