using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{

		public static int level, highscore, stars;
		public static bool music, sound;
		public AudioClip click;
		public TextMesh score, h_score, star, stage_h_score, stage_star;
		public float st_time;

		// Use this for initialization
		void Start ()
		{
				if (tag == "SplashScreen") {
						st_time = Time.fixedTime;
				}

				if (tag == "Back" || name == "level_screen" || name == "game_over" || tag == "SplashScreen" || tag == "About") {
						transform.localScale = new Vector3 (Camera.main.orthographicSize * Camera.main.aspect / 3.6f,
		                                    transform.localScale.y,
		                                    transform.localScale.z);
				}
				if (tag == "Back") {
						if (LevelSetup.gameOver == true) {
								GameObject.FindGameObjectWithTag ("Menu").transform.position = new Vector3 (0, 20, 0);
								GameObject.FindGameObjectWithTag ("GameOver").transform.position = new Vector3 (0, 0, 0);
								score.text = LevelSetup.scores.ToString ();
								star.text = LevelSetup.stars.ToString ();
								stars += LevelSetup.stars;
								if (LevelSetup.scores > highscore) {
										highscore = LevelSetup.scores;
								}
								h_score.text = highscore.ToString ();
								PlayerPrefs.SetInt ("Highscore", highscore);
								PlayerPrefs.SetInt ("Stars", stars);
						}
						if (PlayerPrefs.GetString ("Music") == "true" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Music"))) {
								music = true;
								Camera.main.audio.Play ();
						} else {
								music = false;
								Camera.main.audio.Stop ();
						}
						if (PlayerPrefs.GetString ("Sound") == "true" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Sound"))) {
								sound = true;
						} else {
								sound = false;
						}
						highscore = PlayerPrefs.GetInt ("Highscore");
						stars = PlayerPrefs.GetInt ("Stars");
						stage_h_score.text = highscore.ToString ();
						stage_star.text = stars.ToString ();
						level = PlayerPrefs.GetInt("Level");
				}
				if (tag == "Resume") {
						if (PlayerPrefs.GetString ("Resume") == "false" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Resume"))) {
								renderer.enabled = false;
						}
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (tag == "SplashScreen" && Time.fixedTime - st_time >= 2.0f) {
						Application.LoadLevel ("Level_0");
				}
				
				if (tag == "Lock1" && stars >= 200) {
						renderer.enabled = false;
				}
				if (tag == "Lock2" && stars >= 700) {
						renderer.enabled = false;
				}
				if (tag == "Lock3" && stars >= 2000) {
						renderer.enabled = false;
				}

				if (Input.GetKeyDown (KeyCode.Escape) && tag == "Back") {
						if (GameObject.FindGameObjectWithTag ("About").transform.position.y == 0) {
								GameObject.FindGameObjectWithTag ("About").transform.position = new Vector3 (0, 60, 0);
								GameObject.FindGameObjectWithTag ("Menu").transform.position = new Vector3 (0, 0, 0);
						} else if (GameObject.FindGameObjectWithTag ("GameOver").transform.position.y == 0) {
								GameObject.FindGameObjectWithTag ("GameOver").transform.position = new Vector3 (0, 40, 0);
								GameObject.FindGameObjectWithTag ("Menu").transform.position = new Vector3 (0, 0, 0);
						} else if (transform.position.y == 20) {
								GameObject.FindGameObjectWithTag ("MainMenu").transform.position = new Vector3 (0, 0, 0);
								GameObject.FindGameObjectWithTag ("Stages").transform.position = new Vector3 (0, 20, 0);
						} else {
								Application.Quit ();
						}
				}
		}

		void OnMouseUpAsButton ()
		{
				if (gameObject.tag == "Play") {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						GameObject.FindGameObjectWithTag ("MainMenu").transform.position = new Vector3 (0, 20, 0);
						GameObject.FindGameObjectWithTag ("Stages").transform.position = new Vector3 (0, 0, 0);
				} else if (gameObject.tag == "Resume" && PlayerPrefs.GetString ("Resume") == "true") {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						Application.LoadLevel ("Level_" + level);
				}
		}
}
