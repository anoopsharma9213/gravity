using UnityEngine;
using System.Collections;

public class LevelSetup : MonoBehaviour
{

		public static float bspeed;
		public static bool gameOver , application_pause;
		public static int scores, lives, stars;

		// Use this for initialization
		void Start ()
		{
				if (gameObject.tag == "Back") {
						transform.localScale = new Vector3 (Camera.main.orthographicSize * Camera.main.aspect / 3.6f,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
						if (MainMenu.music == true) {
								Camera.main.audio.Play ();
						} else {
								Camera.main.audio.Stop ();
						}
				}
				if (tag == "LifeUp" || tag == "LifeDown" || tag == "StarUp") {
						renderer.sortingOrder = -1;
				}
				gameOver = false;
				application_pause = false;

				if (PlayerPrefs.GetString ("Resume") == "false" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Resume"))) {
						scores = 0;
						stars = 0;
						lives = 2;
						bspeed = 1.0f;
						if (gameObject.tag == "Block1") {
								transform.position = new Vector3 (-2.5f, 3.55f);
								gameObject.GetComponent<BlockMovement> ().setStatus ("false");
						} else if (gameObject.tag == "Block2") {
								transform.position = new Vector3 (1.5f, 0);
								gameObject.GetComponent<BlockMovement> ().setStatus ("false");
						} else if (gameObject.tag == "Block3") {
								transform.position = new Vector3 (2.5f, -3.55f);
								gameObject.GetComponent<BlockMovement> ().setStatus ("false");
						} else if (gameObject.tag == "Block4") {
								transform.position = new Vector3 (-1.5f, -7.1f);
								gameObject.GetComponent<BlockMovement> ().setStatus ("false");
						} else if (gameObject.tag == "Player") {
								transform.position = new Vector3 (1.5f, 0.61f, 0);
								rigidbody2D.isKinematic = false;
						}
				} else {
						scores = PlayerPrefs.GetInt ("Scores");
						stars = PlayerPrefs.GetInt ("Star");
						lives = PlayerPrefs.GetInt ("Lives");
						bspeed = PlayerPrefs.GetFloat ("Bspeed");
						if (gameObject.tag == "Block1") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block1_x"), PlayerPrefs.GetFloat ("Block1_y"), PlayerPrefs.GetFloat ("Block1_z"));
								gameObject.GetComponent<BlockMovement> ().setStatus (PlayerPrefs.GetString ("Block1_spike"));
						} else if (gameObject.tag == "Block2") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block2_x"), PlayerPrefs.GetFloat ("Block2_y"), PlayerPrefs.GetFloat ("Block2_z"));
								gameObject.GetComponent<BlockMovement> ().setStatus (PlayerPrefs.GetString ("Block2_spike"));
						} else if (gameObject.tag == "Block3") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block3_x"), PlayerPrefs.GetFloat ("Block3_y"), PlayerPrefs.GetFloat ("Block3_z"));
								gameObject.GetComponent<BlockMovement> ().setStatus (PlayerPrefs.GetString ("Block3_spike"));
						} else if (gameObject.tag == "Block4") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block4_x"), PlayerPrefs.GetFloat ("Block4_y"), PlayerPrefs.GetFloat ("Block4_z"));
								gameObject.GetComponent<BlockMovement> ().setStatus (PlayerPrefs.GetString ("Block4_spike"));
						} else if (gameObject.tag == "Life") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Life_x"), PlayerPrefs.GetFloat ("Life_y"), PlayerPrefs.GetFloat ("Life_z"));
								if (PlayerPrefs.GetString ("Life_up") == "true") {
										GetComponent<LifeMovement> ().setGo_up (true);
								} else {
										GetComponent<LifeMovement> ().setGo_up (false);
								}
						} else if (gameObject.tag == "Star") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Star_x"), PlayerPrefs.GetFloat ("Star_y"), PlayerPrefs.GetFloat ("Star_z"));
								if (PlayerPrefs.GetString ("Star_up") == "true") {
										GetComponent<LifeMovement> ().setGo_up (true);
								} else {
										GetComponent<LifeMovement> ().setGo_up (false);
								}
						} else if (gameObject.tag == "Player") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Player_x"), PlayerPrefs.GetFloat ("Player_y"), PlayerPrefs.GetFloat ("Player_z"));
								if (PlayerPrefs.GetString ("Kinetic") == "true") {
										rigidbody2D.isKinematic = true;
								} else {
										rigidbody2D.isKinematic = false;
								}
						}
				}
				GameObject[] Lifebar = GameObject.FindGameObjectsWithTag ("LifeBar");
				for (int i=0; i<Lifebar.Length; i++)
						for (int j=i+1; j<Lifebar.Length; j++) 
								if (Lifebar [i].transform.position.x > Lifebar [j].transform.position.x) {
										Vector3 t = Lifebar [j].transform.position;
										Lifebar [j].transform.position = Lifebar [i].transform.position;
										Lifebar [i].transform.position = t;
								}
				for (int i=0; i<Lifebar.Length; i++) {
						if (i < lives) {
								Lifebar [i].renderer.sortingOrder = 4;
						} else {
								Lifebar [i].renderer.sortingOrder = -1;
						}
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (gameOver == false && application_pause == false) {
						if (gameObject.CompareTag ("Block1")) {
								if (bspeed > 5.0f && bspeed < 8.0f) {
										bspeed += 0.0001f;
								} else if (bspeed > 3.0f) {
										bspeed += 0.0005f;
								} else {
										bspeed += 0.001f;
								}
						}
						if (Input.GetKeyDown (KeyCode.Escape) && gameObject.tag == "Player") {
								PlayerPrefs.SetInt ("Level", MainMenu.level);
								PlayerPrefs.SetString ("Resume", "true");
								PlayerPrefs.SetInt ("Scores", scores);
								PlayerPrefs.SetInt ("Star", stars);
								PlayerPrefs.SetFloat ("Bspeed", bspeed);
								PlayerPrefs.SetInt ("Lives", lives);
								for (int i= 1; i<5; i++) {
										GameObject go = GameObject.FindGameObjectWithTag ("Block" + i);
										PlayerPrefs.SetFloat ("Block" + i + "_x", go.transform.position.x);
										PlayerPrefs.SetFloat ("Block" + i + "_y", go.transform.position.y);
										PlayerPrefs.SetFloat ("Block" + i + "_z", go.transform.position.z);
										if (go.GetComponent<BlockMovement> ().getStatus () == true) {
												PlayerPrefs.SetString ("Block" + i + "_spike", "true");
										} else {
												PlayerPrefs.SetString ("Block" + i + "_spike", "false");
										}
								}
								PlayerPrefs.SetFloat ("Player_x", transform.position.x);
								PlayerPrefs.SetFloat ("Player_y", transform.position.y);
								PlayerPrefs.SetFloat ("Player_z", transform.position.z);
								PlayerPrefs.SetFloat ("Star_x", GameObject.FindGameObjectWithTag ("Star").transform.position.x);
								PlayerPrefs.SetFloat ("Star_y", GameObject.FindGameObjectWithTag ("Star").transform.position.y);
								PlayerPrefs.SetFloat ("Star_z", GameObject.FindGameObjectWithTag ("Star").transform.position.z);
								PlayerPrefs.SetFloat ("Life_x", GameObject.FindGameObjectWithTag ("Life").transform.position.x);
								PlayerPrefs.SetFloat ("Life_y", GameObject.FindGameObjectWithTag ("Life").transform.position.y);
								PlayerPrefs.SetFloat ("Life_z", GameObject.FindGameObjectWithTag ("Life").transform.position.z);
								if (GameObject.FindGameObjectWithTag ("Star").GetComponent<LifeMovement> ().getGo_up () == true) {
										PlayerPrefs.SetString ("Star_up", "true");
								} else {
										PlayerPrefs.SetString ("Star_up", "false");
								}
								if (GameObject.FindGameObjectWithTag ("Life").GetComponent<LifeMovement> ().getGo_up () == true) {
										PlayerPrefs.SetString ("Life_up", "true");
								} else {
										PlayerPrefs.SetString ("Life_up", "false");
								}
								if (rigidbody2D.isKinematic == true) {
										PlayerPrefs.SetString ("Kinetic", "true");
								} else {
										PlayerPrefs.SetString ("Kinetic", "false");
								}
								SettingControl.showTray = false;
								Application.LoadLevel ("Level_0");
						}
				} else if (gameOver == true) {
						if (gameObject.tag == "Player") {
								PlayerPrefs.SetString ("Resume", "false");
								Application.LoadLevel ("Level_0");
						}
				} else if (application_pause == true) {
						application_pause = false;
				}
		}
		
		void OnApplicationPause(){
				application_pause = true;
		}
}
