using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;    // 取得目前位置
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);
		transform.position = position;    // 更新位置
		// 移動至畫面外時，將他重新移動到畫面上方
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0));    // 底部左邊
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));    // 頂部右邊
		if ( transform.position.y < min.y ) {
			transform.position = new Vector2 (Random.Range (min.x, max.x), max.y);
		}
	}
}
