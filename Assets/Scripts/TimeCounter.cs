using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {
	Text timeUI;    // Time Counter UI

	float startTime;    // 按下 play 時的時間
	float elapsedTime;
	bool startCounter;

	float minutes;
	float seconds;
	// Use this for initialization
	void Start () {
		startCounter = false;
		timeUI = GetComponent<Text> ();
	}

	// 開始時間倒數
	public void startTimeCounter() {
		startTime = Time.time;
		startCounter = true;
	}

	// 停止倒數
	public void stopTimeCounter() {
		startCounter = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (startCounter) {
			// 開始計算時間
			elapsedTime = Time.time - startTime;
			minutes = elapsedTime / 60;
			seconds = elapsedTime % 60;

			timeUI.text = string.Format ("{0:00}:{1:00}", minutes, seconds);    // 更新介面
		}
	}
}
