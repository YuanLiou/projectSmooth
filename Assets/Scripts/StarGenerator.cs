using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {
	public GameObject starBg;    // Star prefab
	public int MaxStars;

	Color[] starColors = {
		new Color(0.5f, 0.5f, 1f),    //藍
		new Color(0, 1f, 1f),    //綠
		new Color(1f, 1f, 0),    //黃
		new Color(1f, 0, 0)    //紅
	};
	// Use this for initialization
	void Start () {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));    // 左下
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));    // 右上
		for ( int i=0; i<MaxStars; ++i) {
			GameObject star = (GameObject)Instantiate (starBg);
			star.GetComponent<SpriteRenderer> ().color = starColors [Random.Range (0, 3)];    // 顏色
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));    // 位置
			star.GetComponent<Stars>().speed = -(1f * Random.value + 0.5f);    // 速度
			star.transform.parent = transform;    // 將生產出來的星星作為生產器的孩子
		}	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
