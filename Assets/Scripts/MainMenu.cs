using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public static int level;

	// Use this for initialization
	void Start () {
		transform.localScale = new Vector3 (Camera.main.orthographicSize * Camera.main.aspect / 3.6f,
		                                    transform.localScale.y,
		                                    transform.localScale.z);
		if (gameObject.tag == "Setting") {
						level = PlayerPrefs.GetInt ("Level");
				}
		if (gameObject.tag == "Resume") {
						if (PlayerPrefs.GetString ("Resume") == "false" || string.IsNullOrEmpty (PlayerPrefs.GetString ("Resume"))) {
								renderer.enabled = false;
						}
				}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
						Application.Quit ();
				}
	}

	void OnGUI()
	{
		GUIStyle g = new GUIStyle ();
		g.fontSize = (int)(Camera.main.orthographicSize * Camera.main.aspect * 20.0f / 3.6f);
		GUI.Label (new Rect (130.0f, 555.0f, 100.0f, 200.0f), "Level : "+ level, g);
	}

	void OnMouseUpAsButton()
	{
		if (gameObject.tag == "Setting") {
						level++;
						if (level == 6) {
								level = 1;
						}
				} else if (gameObject.tag == "Play") {
						PlayerPrefs.SetString ("Resume", "false");
						Application.LoadLevel ("Level_1");
				} else if (gameObject.tag == "Resume") {
						Application.LoadLevel ("Level_1");
				}
	}
}
