using UnityEngine;
using System.Collections;

public class Charactercontrol : MonoBehaviour
{

		public float move_speed, fall_speed;
		private Vector3 move_dir, fall_dir;
		public static bool Game_over;
		private bool iscaptured;

		// Use this for initialization
		void Start ()
		{
				Game_over = false;
				iscaptured = false;
				fall_dir = Vector3.down;
		}

		// Update is called once per frame
		void Update ()
		{

				if (Input.GetKeyDown (KeyCode.Escape)) {
						if (Game_over == true) {
								Application.LoadLevel ("Scene1");
						} else {
								Application.Quit ();
						}
				}

				if (Game_over == false) {

						Vector3 currpos = transform.position;
						Vector3 target;

						if (currpos.y < -(Camera.main.orthographicSize + renderer.bounds.size.y) || currpos.y > Camera.main.orthographicSize) {
								Game_over = true;
								target = new Vector3 (0, 7.2f);
								transform.position = target;
						} else {
								if (iscaptured == false) {
										target = currpos + fall_dir * fall_speed;
										transform.position = Vector3.Lerp (currpos, target, Time.deltaTime);
										transform.position = target;
								}
								else{
					target = currpos + Vector3.up * Blockmovement.speed;
					transform.position = Vector3.Lerp (currpos, target, Time.deltaTime);
					transform.position = target;
								}

								if (Input.GetButton ("Fire1")) {
										if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x >= 0) {
												move_dir = Vector3.right;
												transform.rotation = new Quaternion (0, 0, 0, 0);
										} else {
												move_dir = Vector3.left;
												transform.rotation = new Quaternion (0, 180, 0, 0);
										}
										currpos = transform.position;
										target = currpos + move_dir * move_speed;
										transform.position = Vector3.Lerp (currpos, target, Time.deltaTime);

										float dis = (transform.position - Camera.main.transform.position).z;
										transform.position = new Vector3 (Mathf.Clamp (transform.position.x,
					                                             Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dis)).x + renderer.bounds.size.x * 0.5f,
					                                             Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dis)).x - renderer.bounds.size.x * 0.5f), transform.position.y);
								}

						}
				}
		}

		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Block") {
						iscaptured = true;
				}
		}

		void OnCollisionExit2D (Collision2D col)
		{
				if (col.gameObject.tag == "Block") {
						iscaptured = false;
				}
		}
}