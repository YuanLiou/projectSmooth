using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public float speed;
	public bool isMoving;

	Vector2 min;    // 左下邊界
	Vector2 max;    // 右上邊界

	void Awake() {
		isMoving = false;
		min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

		// max.y 加上 Sprite 一半的高度(doc: The extents of the box. This is always half of the size.)
		max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
		// min.y 剪去 Sprite 一半的高度
		min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving)
			return;
		// 取得目前的位置，並加上 y 軸動量再更新位置
		Vector2 position = transform.position;
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);
		transform.position = position;
		// 跑到螢幕外了
		if (transform.position.y < min.y) {
			isMoving = false;
		}
	}

	public void ResetPosition() {
		// x 隨機
		transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
	}
}
