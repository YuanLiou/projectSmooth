using UnityEngine;
using System.Collections;

public class BulletsControll : MonoBehaviour {
	float speed;
	// Use this for initialization
	void Start () {
		speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
		// 取得目前子彈的位置
		Vector2 position = transform.position;

		// 子彈新的位置
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		// 更新位置
		transform.position = position;
		//print (position);

		// 螢幕右上角的座標
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// 如果子彈跑到螢幕外面去，就把它拿掉
		if (transform.position.y > max.y) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D _collider) {
		// 偵測觸碰到敵機
		if ( _collider.tag == "EnemyShipTag" ) {
			Destroy (gameObject);
		}
	}
}
