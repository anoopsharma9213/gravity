using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
				if (LevelSetup.gameOver == false) {
						Vector3 curr = transform.position;
						Vector3 next;
						if (curr.y > Camera.main.orthographicSize + renderer.bounds.size.y * 0.5f) {
								next = new Vector3 (UnityEngine.Random.Range (-(Camera.main.orthographicSize * Camera.main.aspect), Camera.main.orthographicSize * Camera.main.aspect), -(Camera.main.orthographicSize + renderer.bounds.size.y), 0);
								LevelSetup.scores++;
								transform.position = next;
						} else {
								next = Vector3.up * LevelSetup.bspeed + curr;
								transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
						}
				}
	}
}
