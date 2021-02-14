using UnityEngine;
using System.Collections;

public class LevelSetup : MonoBehaviour {

	public static float bspeed;
	public static bool gameOver;
	public static int scores;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (Camera.main.orthographicSize * Camera.main.aspect / 3.6f,
		                                   transform.localScale.y,
		                                   transform.localScale.z);
		gameOver = false;

		if (PlayerPrefs.GetString ("Resume") == "false" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Resume"))) {
						scores = 0;
						bspeed = 1.0f;
						if (gameObject.tag == "Block1") {
								transform.position = new Vector3 (Random.Range (-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), 3.2f);
						} else if (gameObject.tag == "Block2") {
								transform.position = new Vector3 (Random.Range (-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), 0);
						} else if (gameObject.tag == "Block3") {
								transform.position = new Vector3 (Random.Range (-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), -3.2f);
						} else if (gameObject.tag == "Block4") {
								transform.position = new Vector3 (Random.Range (-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect), -6.4f);
						} else if (gameObject.tag == "Player") {
								transform.position = new Vector3 (0, 6.0f, 0);
						}
				} else {
						scores = PlayerPrefs.GetInt ("Scores");
						bspeed = PlayerPrefs.GetFloat ("Bspeed");
						if (gameObject.tag == "Block1") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block1_x"), PlayerPrefs.GetFloat ("Block1_y"), PlayerPrefs.GetFloat ("Block1_z"));
						} else if (gameObject.tag == "Block2") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block2_x"), PlayerPrefs.GetFloat ("Block2_y"), PlayerPrefs.GetFloat ("Block2_z"));
						} else if (gameObject.tag == "Block3") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block3_x"), PlayerPrefs.GetFloat ("Block3_y"), PlayerPrefs.GetFloat ("Block3_z"));
						} else if (gameObject.tag == "Block4") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Block4_x"), PlayerPrefs.GetFloat ("Block4_y"), PlayerPrefs.GetFloat ("Block4_z"));
						} else if (gameObject.tag == "Player") {
								transform.position = new Vector3 (PlayerPrefs.GetFloat ("Player_x"), PlayerPrefs.GetFloat ("Player_y"), PlayerPrefs.GetFloat ("Player_z"));
						}
				}
	}
	
	// Update is called once per frame
	void Update () {
				if (gameOver == false) {
						if (gameObject.CompareTag ("Block1")) {
								if (bspeed < 9.0f) {
										bspeed += 0.0001f;
								}
						}
						if (Input.GetKeyDown (KeyCode.Escape) && gameObject.tag == "Player") {
								PlayerPrefs.SetString ("Resume", "true");
								PlayerPrefs.SetInt ("Scores", scores);
								PlayerPrefs.SetFloat ("Bspeed", bspeed);
								Application.LoadLevel ("Level_0");
								for (int i= 1; i<5; i++) {
										GameObject go = GameObject.FindGameObjectWithTag ("Block" + i);
										PlayerPrefs.SetFloat ("Block" + i + "_x", go.transform.position.x);
										PlayerPrefs.SetFloat ("Block" + i + "_y", go.transform.position.y);
										PlayerPrefs.SetFloat ("Block" + i + "_z", go.transform.position.z);
								}
								PlayerPrefs.SetFloat ("Player_x", transform.position.x);
								PlayerPrefs.SetFloat ("Player_y", transform.position.y);
								PlayerPrefs.SetFloat ("Player_z", transform.position.z);
						}
				} else {
						if (Input.GetKeyDown (KeyCode.Escape)) {
								PlayerPrefs.SetString ("Resume", "false");
								Application.LoadLevel ("Level_0");
						}
				}
	}

	void OnGUI(){
		if (gameObject.tag == "Player") {
						GUIStyle g = new GUIStyle ();
						g.fontSize = (int)(Camera.main.orthographicSize * Camera.main.aspect * 20.0f / 3.6f);
						GUI.Label (new Rect (5.0f, 3.0f, 400.0f, 400.0f), "Score: " + scores, g);
				}
	}
}
