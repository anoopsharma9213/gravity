using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float move_speed, fall_speed;
	private Vector3 move_dir;
	private bool iscaptured;

	// Use this for initialization
	void Start () {
		iscaptured = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (LevelSetup.gameOver == false) {
						Vector3 curr = transform.position;
						Vector3 next;
						if (curr.y < -(Camera.main.orthographicSize + renderer.bounds.size.y * 0.5f) || curr.y > Camera.main.orthographicSize) {
								LevelSetup.gameOver = true;
						} else {
								if (iscaptured == false) {
										next = curr + Vector3.down * fall_speed;
										transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								} else {
										next = curr + Vector3.up * LevelSetup.bspeed;
										transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								}
						}
						if (Input.GetButton ("Fire1")) {
								if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x >= 0) {
										move_dir = Vector3.right;
										transform.rotation = new Quaternion (0, 0, 0, 0);
								} else {
										move_dir = Vector3.left;
										transform.rotation = new Quaternion (0, 180, 0, 0);
								}
								curr = transform.position;
								next = curr + move_dir * move_speed;
								transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
				
								float dis = (transform.position - Camera.main.transform.position).z;
								transform.position = new Vector3 (Mathf.Clamp (transform.position.x,
				                                               Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dis)).x + renderer.bounds.size.x * 0.5f,
				                                               Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dis)).x - renderer.bounds.size.x * 0.5f), transform.position.y);
						}
				}
	}

	void OnCollisionEnter2D (Collision2D col){
		iscaptured = true;
	}

	void OnCollisionExit2D (Collision2D col){
		iscaptured = false;
	}
}
