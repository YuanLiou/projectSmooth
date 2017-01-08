using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserControll : MonoBehaviour {
	public GameObject gameManager; // 遊戲管理員
	public float speed;
	public GameObject bullets;    // 子彈 prefab
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	public GameObject exposion;    // 爆炸 prefab
	private const float firstX = 0.01f;
	private const float firstY = -2.53f;
	private Animator anim;
	float firerate = 0.25f;
	private float nextFire = 0.15f;

	public Text lives;    // 生命ui
	const int MaxLive = 3;
	int NowLive;    // 目前的生命

	public void Init() {
		NowLive = MaxLive;
		lives.text = NowLive.ToString();    // 數值放在 UI上
		gameObject.transform.position = new Vector2 (firstX, firstY);
		gameObject.SetActive(true);    // 啟動 主角機
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// 按下 C 射擊
		if ( Input.GetKey(KeyCode.C) && Time.time > nextFire ) {
			animationChooser ("shooting");
			gameObject.GetComponent<AudioSource> ().Play ();    // 音效
			GameObject bullets01 = (GameObject)Instantiate (bullets);
			bullets01.transform.position = bulletPosition1.transform.position;

			GameObject bullets02 = (GameObject)Instantiate (bullets);
			bullets02.transform.position = bulletPosition2.transform.position;
			nextFire = Time.time + firerate;
		} else if ( Input.GetKeyDown(KeyCode.Alpha1) ) {
			speed = 2f;
		} else if ( Input.GetKeyDown(KeyCode.Alpha2) ) {
			speed = 4f;
		} else if ( Input.GetKeyDown(KeyCode.Alpha3) ) {
			speed = 6f;
		} else if ( Input.GetKeyDown(KeyCode.Alpha4) ) {
			speed = 8f;
		}
			
		float x = Input.GetAxisRaw ("Horizontal");    //數值左(-1), 沒有輸入(0), 右(1)
		float y = Input.GetAxisRaw ("Vertical");      //數值下(-1), 沒有輸入(0), 上(1)
		
		Vector2 direction = new Vector2 (x, y).normalized;    // 把它轉成 Unity 用的向量
        Move (direction);
	}

	void Move (Vector2 dir) {
        // 尋找螢幕的邊界
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));    // 左下腳螢幕邊界的座標
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));    // 右上腳螢幕邊界的座標

		// 主要 Sprite 的 x, y 長度
		//float player_x = GetComponent<SpriteRenderer>().bounds.size.x;
		//float player_y = GetComponent<SpriteRenderer>().bounds.size.y;
		//float half = 2.0f;
		//print ("Player X: " +player_x);
		//print ("Player Y: " +player_y);
        
        // 一半的 Sprite 可以再出現在螢幕裡面
		max.x = max.x - 0.15f;    // 減掉 Player Sprite 一半的寬度
		min.x = min.x + 0.2f;    // 加上 Player Sprite 一半的寬度
		max.y = max.y - 0.15f;    // 減掉 Player Sprite 一半的高度
		min.y = min.y + 0.2f;    // 加上 Player Sprite 一半的高度

        // 玩家目前的位置
        Vector2 pos = transform.position;

        // 計算新的位置
        pos += dir * speed * Time.deltaTime;

        // 確定位置不會跑到螢幕外面
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // 更新角色位置，中文打就補
        transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D _collider) {
		// 偵測被敵方或者是敵方子彈破壞
		if ( _collider.tag == "EnemyShipTag" || _collider.tag == "EnemyBulletTag" ) {
			NowLive--;
			lives.text = NowLive.ToString ();
			GameObject expo = (GameObject)Instantiate (exposion);
			expo.transform.position = transform.position;
			if ( NowLive == 0 ) {
				// 設定遊戲狀態
				gameManager.GetComponent<GameManager> ().SetGameState (GameManager.GameStates.GameOver);
				gameObject.SetActive (false);    // 隱藏主角機
			} else {
				//Destroy (gameObject);
				gameObject.SetActive (false);
				Invoke ("airplaneRebirth", 2f);    // 2秒後重生
			}
		}

	}

	public void airplaneRebirth() {
		// return first place
		gameObject.transform.position = new Vector2 (firstX, firstY);
		gameObject.SetActive (true);
	}

	public void animationChooser(string action) {
		switch(action) {
		case "shooting":
			anim.SetBool ("Shooting", true);
			break;
		case "unshooting":
			anim.SetBool ("Shooting", false);
			break;
		default:
			break;
		}
	}
}