using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

		public float move_speed, fall_speed;
		private Vector3 move_dir;
		public static bool iscaptured, isanimated, isdead;
		public bool wait;
		public float st, disp_st;
		public AudioClip life_gain, star_gain, life_lost;
		public TextMesh score_disp, star_disp;

		// Use this for initialization
		void Start ()
		{
				iscaptured = false;
				isanimated = false;
				wait = false;
				isdead = false;
				disp_st = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (LevelSetup.gameOver == false && LevelSetup.application_pause == false) {
						if (disp_st != 0 && Time.fixedTime - disp_st >= 2.0f) {
								GameObject[] hud = GameObject.FindGameObjectsWithTag ("LifeUp");
								foreach (GameObject i in hud) {
										i.renderer.sortingOrder = -1;
								}
								hud = GameObject.FindGameObjectsWithTag ("LifeDown");
								foreach (GameObject i in hud) {
										i.renderer.sortingOrder = -1;
								}
								hud = GameObject.FindGameObjectsWithTag ("StarUp");
								foreach (GameObject i in hud) {
										i.renderer.sortingOrder = -1;
								}
								disp_st = 0;
						}
						score_disp.text = LevelSetup.scores.ToString ();
						star_disp.text = LevelSetup.stars.ToString ();
						star_disp.renderer.sortingOrder = 5;
						score_disp.renderer.sortingOrder = 5;
						Vector3 curr = transform.position;
						Vector3 next;
						if (isdead == true) {
								if (Time.fixedTime - st >= 0.25f) {
										rigidbody2D.isKinematic = true;
								}
						}
						if (curr.y < -(Camera.main.orthographicSize + renderer.bounds.size.y) || curr.y > Camera.main.orthographicSize + renderer.bounds.size.y * 0.5f) {
								if (LevelSetup.lives == 0 && wait == false) {
										LevelSetup.gameOver = true;
								} else {
										if (wait == false) {
												if (isdead == false) {
														GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (life_lost);
												}
												GameObject[] hud = GameObject.FindGameObjectsWithTag ("LifeDown");
												foreach (GameObject i in hud) {
														i.renderer.sortingOrder = 5;
												}
												disp_st = Time.fixedTime;
												LevelSetup.lives--;
												GameObject[] Lifebar = GameObject.FindGameObjectsWithTag ("LifeBar");
												for (int i=0; i<Lifebar.Length; i++)
														for (int j=i+1; j<Lifebar.Length; j++) 
																if (Lifebar [i].transform.position.x > Lifebar [j].transform.position.x) {
																		Vector3 t = Lifebar [j].transform.position;
																		Lifebar [j].transform.position = Lifebar [i].transform.position;
																		Lifebar [i].transform.position = t;
																}
												for (int i=0; i<Lifebar.Length; i++) {
														if (i < LevelSetup.lives) {
																Lifebar [i].renderer.sortingOrder = 4;
														} else {
																Lifebar [i].renderer.sortingOrder = -1;
														}
												}
												wait = true;
										}
										Vector3 pos = Vector3.one;
										for (int i= 1; i<5; i++) {
												GameObject go = GameObject.FindGameObjectWithTag ("Block" + i);
												if (go.transform.position.y < -Camera.main.orthographicSize && go.GetComponent<BlockMovement> ().getStatus () == false) {
														pos = new Vector3 (go.transform.position.x, go.transform.position.y + (go.renderer.bounds.size.y * 0.5f + gameObject.renderer.bounds.size.y * 0.5f), 0);
														transform.position = pos;
														rigidbody2D.isKinematic = false;
														wait = false;
														isdead = false;
														break;
												}
										}
								}
						} else {
								if (iscaptured == false) {
										next = curr + Vector3.down * fall_speed;
										transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								} else {
										next = curr + Vector3.up * LevelSetup.bspeed;
										transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								}
						}
						GameObject.FindGameObjectWithTag ("Button_right").renderer.material.color = Color.white;
						GameObject.FindGameObjectWithTag ("Button_left").renderer.material.color = Color.white;
						if (Input.GetButton ("Fire1") && isdead == false) {
								isanimated = true;
								if (Camera.main.ScreenToWorldPoint (Input.mousePosition).x >= 0) {
										move_dir = Vector3.right;
										transform.rotation = new Quaternion (0, 0, 0, 0);
										GameObject.FindGameObjectWithTag ("Button_right").renderer.material.color = Color.grey;
								} else {
										move_dir = Vector3.left;
										transform.rotation = new Quaternion (0, 180, 0, 0);
										GameObject.FindGameObjectWithTag ("Button_left").renderer.material.color = Color.grey;
								}
								curr = transform.position;
								next = curr + move_dir * move_speed;
								transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								
								float dis = (transform.position - Camera.main.transform.position).z;

								if (Application.loadedLevelName == "Level_2") {
										transform.position = new Vector3 (Mathf.Clamp (transform.position.x,
					                                                Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dis)).x + renderer.bounds.size.x * 0.25f,
					                                                Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dis)).x - renderer.bounds.size.x * 0.25f), transform.position.y);

								} else {
										transform.position = new Vector3 (Mathf.Clamp (transform.position.x,
					                                               Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dis)).x + renderer.bounds.size.x * 0.5f,
					                                               Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dis)).x - renderer.bounds.size.x * 0.5f), transform.position.y);
								}
						} else {
								isanimated = false;
						}
				}
		}

		void OnCollisionEnter2D (Collision2D col)
		{

				if (col.collider.tag == "Life") {
						if (col.collider.transform.position.y <= -6.0f) {
								transform.position = col.collider.transform.position;
						}
						col.collider.GetComponent<LifeMovement> ().setGo_up (false);
						col.collider.transform.position = new Vector3 (0, -20, 0);
						if (LevelSetup.lives < 10) {
								LevelSetup.lives++;
				
								GameObject[] hud = GameObject.FindGameObjectsWithTag ("LifeUp");
								foreach (GameObject i in hud) {
										i.renderer.sortingOrder = 5;
								}
								disp_st = Time.fixedTime;
								GameObject[] Lifebar = GameObject.FindGameObjectsWithTag ("LifeBar");
								for (int i=0; i<Lifebar.Length; i++)
										for (int j=i+1; j<Lifebar.Length; j++) 
												if (Lifebar [i].transform.position.x > Lifebar [j].transform.position.x) {
														Vector3 t = Lifebar [j].transform.position;
														Lifebar [j].transform.position = Lifebar [i].transform.position;
														Lifebar [i].transform.position = t;
												}
								for (int i=0; i<Lifebar.Length; i++) {
										if (i < LevelSetup.lives) {
												Lifebar [i].renderer.sortingOrder = 4;
										} else {
												Lifebar [i].renderer.sortingOrder = -1;
										}
								}
						}
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (life_gain);
				}
				if (col.collider.tag == "Star") {
						if (col.collider.transform.position.y <= -6.0f) {
								transform.position = col.collider.transform.position;
						}
						col.collider.GetComponent<LifeMovement> ().setGo_up (false);
						col.collider.transform.position = new Vector3 (0, -20, 0);
						LevelSetup.stars++;
						GameObject[] hud = GameObject.FindGameObjectsWithTag ("StarUp");
						foreach (GameObject i in hud) {
								i.renderer.sortingOrder = 5;
						}
						disp_st = Time.fixedTime;
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (star_gain);
				}
				if (col.collider.tag != "Life" && col.collider.tag != "Star" && iscaptured == false) {
						Vector3 obj = gameObject.transform.position;
						Vector3 obj_size = gameObject.renderer.bounds.size;
						Vector3 colobj = col.collider.transform.position;
						Vector3 colobj_size = col.collider.renderer.bounds.size;

						if ((obj.x >= colobj.x - colobj_size.x * 0.5f - obj_size.x * 0.5f) &&
								(obj.x <= colobj.x + colobj_size.x * 0.5f + obj_size.x * 0.5f)) {
								if (col.collider.GetComponent<BlockMovement> ().getStatus () == true) {
										iscaptured = true;
										isdead = true;
										st = Time.fixedTime;
										GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (life_lost);
								} else {
										if (obj.y > colobj.y + colobj_size.y * 0.5f) {
												iscaptured = true;
										}
										BoxCollider2D bx = gameObject.GetComponent<BoxCollider2D> ();
										float i = (bx.transform.position.y - bx.size.y * 0.5f) - (col.collider.transform.position.y + col.collider.renderer.bounds.size.y * 0.5f);
										transform.position = new Vector3 (transform.position.x, col.collider.transform.position.y + (col.collider.renderer.bounds.size.y * 0.5f + (renderer.bounds.size.y * 0.5f - i)), 0);
								}
						}
				}
		}

		void OnCollisionExit2D (Collision2D col)
		{
				if (col.collider.tag != "Life" && col.collider.tag != "Star") {
						iscaptured = false;
				}
		}
}
