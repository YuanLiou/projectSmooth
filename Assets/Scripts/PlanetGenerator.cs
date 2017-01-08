using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetGenerator : MonoBehaviour {
	public GameObject[] planets;
	Queue<GameObject> avaliablePlanets = new Queue<GameObject>();
	// Use this for initialization
	void Start () {
		// 放入 Queue 之中
		avaliablePlanets.Enqueue (planets [0]);
		avaliablePlanets.Enqueue (planets [1]);
		avaliablePlanets.Enqueue (planets [2]);
		// 每 20 秒
		InvokeRepeating("MovePlanet", 0, 20f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	// 從 Queue 中取出 planets ，並讓他開始向下飄
	void MovePlanet() {
		EnqueuePlanets ();
		// Queue 裡沒有東西
		if (avaliablePlanets.Count == 0)
			return;

		// get planets
		GameObject aplanet = avaliablePlanets.Dequeue();
		aplanet.GetComponent<Planet> ().isMoving = true;
	}

	// planets 跑到螢幕外面，不動的處理
	void EnqueuePlanets() {
		//int i = 0;
		foreach(GameObject a_planet in planets) {
			if ( (a_planet.transform.position.y < 0) && !(a_planet.GetComponent<Planet>().isMoving) ) {
				a_planet.GetComponent<Planet> ().ResetPosition ();
				avaliablePlanets.Enqueue (a_planet);
				//print ("The" + (i++) + "Planets pos: " + a_planet.transform.position.x+", "+a_planet.transform.position.y);
			}
		}
	}
}
