using UnityEngine;
using System.Collections;

public class SettingControl : MonoBehaviour
{
		public float rotspeed, trans_speed;
		public bool showicon;
		public static bool showTray;
		private bool transition;
		public SpriteRenderer sr;
		public Sprite on, off;
		public AudioClip click;

		// Use this for initialization
		void Start ()
		{
				sr = renderer as SpriteRenderer;
				transition = false;
				showicon = false;
				if (name == "music") {
						if (MainMenu.music == false) {
								sr.sprite = off;
						} else {
								sr.sprite = on;
						}
				} else if (name == "sound") {
						if (MainMenu.sound == false) {
								sr.sprite = off;
						} else {
								sr.sprite = on;
						}
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				//Debug.Log (transform.rotation.ToString ());
				if (tag == "Setting") {
						Vector3 temp;
						if (transition == true && showTray == true) {
								if (showicon == false) {
										temp = GameObject.FindGameObjectWithTag ("Tray").transform.localScale;
										if (temp.x < 1) {
												GameObject.FindGameObjectWithTag ("Tray").transform.localScale = new Vector3 (temp.x + trans_speed, 1, temp.z);
										} else {
												showicon = true;
												GameObject.FindGameObjectWithTag ("Tray").transform.localScale = new Vector3 (1, 1, 1);
										}
								} else {
										temp = GameObject.FindGameObjectWithTag ("SettingButton").transform.localScale;
										if (temp.x < 1) {
												GameObject[] button = GameObject.FindGameObjectsWithTag ("SettingButton");
												foreach (GameObject i in button) {
														i.transform.localScale = new Vector3 (i.transform.localScale.x + trans_speed, i.transform.localScale.y + trans_speed, i.transform.localScale.z);
												}
										} else {
												GameObject[] button = GameObject.FindGameObjectsWithTag ("SettingButton");
												foreach (GameObject i in button) {
														i.transform.localScale = new Vector3 (1, 1, 1);
												}
												transition = false;
										}
								}
						} else if (transition == true && showTray == false) {
								if (showicon == true) {
										temp = GameObject.FindGameObjectWithTag ("SettingButton").transform.localScale;
										if (temp.x > 0) {
												GameObject[] button = GameObject.FindGameObjectsWithTag ("SettingButton");
												foreach (GameObject i in button) {
														i.transform.localScale = new Vector3 (i.transform.localScale.x - trans_speed, i.transform.localScale.y - trans_speed, i.transform.localScale.z);
												}
										} else {
												GameObject[] button = GameObject.FindGameObjectsWithTag ("SettingButton");
												foreach (GameObject i in button) {
														i.transform.localScale = new Vector3 (0, 0, 1);
												}
												showicon = false;

				
										}
								} else {
										temp = GameObject.FindGameObjectWithTag ("Tray").transform.localScale;
										if (temp.x > 0) {
												GameObject.FindGameObjectWithTag ("Tray").transform.localScale = new Vector3 (temp.x - trans_speed, 1, temp.z);
										} else {
												GameObject.FindGameObjectWithTag ("Tray").transform.localScale = new Vector3 (0, 0, 1);
					
												transition = false;
										}
								}
						}
						if (transition == true) {
								if (showTray == true) {
										Quaternion rot = transform.rotation;
										rot.w = rot.w - rotspeed;
										if (rot.w >= 0) {
												rot.z = rot.z + rotspeed;
										} else {
												rot.z = rot.z - rotspeed;
												if (rot.w <= -1 && rot.x <= 0) {
														rot.w = 1;
												}
										}
										transform.rotation = rot;
								} else {
										Quaternion rot = transform.rotation;
										rot.w = rot.w + rotspeed;
										if (rot.w <= 0) {
												rot.z = rot.z + rotspeed;
										} else {
												rot.z = rot.z - rotspeed;
												if (rot.w >= 1 && rot.x <= 0) {
														rot.w = 1;
												}
										}
										transform.rotation = rot;
								}
						}
				}
		}

		void OnMouseUpAsButton ()
		{
				if (tag == "Setting") {
						if (transition == false) {
								if (showTray == false) {
										showTray = true;
										transition = true;
								} else {
										showTray = false;
										transition = true;
										transform.rotation = new Quaternion (0, 0, 0, -1);
								}
						}
				} else if (name == "music" && showTray == true) {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						if (MainMenu.music == false) {
								MainMenu.music = true;
								PlayerPrefs.SetString ("Music", "true");
								sr.sprite = on;
								Camera.main.audio.Play ();
						} else {
								MainMenu.music = false;
								PlayerPrefs.SetString ("Music", "false");
								sr.sprite = off;
								Camera.main.audio.Stop ();
						}
				} else if (name == "sound" && showTray == true) {
						if (MainMenu.sound == false) {
								MainMenu.sound = true;
								PlayerPrefs.SetString ("Sound", "true");
								sr.sprite = on;
						} else {
								MainMenu.sound = false;
								PlayerPrefs.SetString ("Sound", "false");
								sr.sprite = off;
						}
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
				} else if (name == "rate" && showTray == true) {
						if (Application.platform == RuntimePlatform.WP8Player) {
								Application.OpenURL ("zune:reviewapp?appid=2838d93d-4811-49c8-933d-7debe523e8af");
						}
						else if (Application.platform == RuntimePlatform.Android) {
								Application.OpenURL ("market://details?id=com.RampageStudios.Gravity1_0");
						}
				} else if (name == "about" && showTray == true) {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						GameObject.FindGameObjectWithTag ("Menu").transform.position = new Vector3 (0, 60, 0);
						GameObject.FindGameObjectWithTag ("About").transform.position = new Vector3 (0, 0, 0);
				} else if (name == "fb") {
						Application.OpenURL ("http://www.facebook.com/rampagestudios.in");
				} else if (name == "playagain") {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_" + MainMenu.level);
				} else if (name == "menu") {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						GameObject.FindGameObjectWithTag ("Menu").transform.position = new Vector3 (0, 0, 0);
						GameObject.FindGameObjectWithTag ("GameOver").transform.position = new Vector3 (0, 40, 0);
				} else if (name == "Stage1") {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						MainMenu.level = 1;
						PlayerPrefs.SetInt ("Level", MainMenu.level);
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_" + MainMenu.level);
				} else if (name == "Stage2" && MainMenu.stars >= 200) {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						MainMenu.level = 2;
						PlayerPrefs.SetInt ("Level", MainMenu.level);
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_" + MainMenu.level);
				} else if (name == "Stage3" && MainMenu.stars >= 700) {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						MainMenu.level = 3;
						PlayerPrefs.SetInt ("Level", MainMenu.level);
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_" + MainMenu.level);
				} else if (name == "Stage4" && MainMenu.stars >= 2000) {
						GameObject.FindGameObjectWithTag ("SoundCamera").GetComponent<SoundPlay> ().sound_play (click);
						MainMenu.level = 4;
						PlayerPrefs.SetInt ("Level", MainMenu.level);
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_" + MainMenu.level);
				}
		}
}
