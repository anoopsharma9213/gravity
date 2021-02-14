using UnityEngine;
using System.Collections;

public class Blockmovement : MonoBehaviour
{

		public static float speed;
		public Vector3 dir;
		public static int points;

		// Use this for initialization
		void Start ()
		{
				dir = Vector3.up;
		speed = 0.03f;
				points = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{

				if (Charactercontrol.Game_over == false) {

						if (speed < 0.20f) {
								speed += 0.00001f;
						}

						Vector3 curr = transform.position;
						Vector3 next;
						if (curr.y > Camera.main.orthographicSize + renderer.bounds.size.y) {
								next = new Vector3 (Random.Range (-(Camera.main.orthographicSize * Camera.main.aspect), Camera.main.orthographicSize * Camera.main.aspect), -(Camera.main.orthographicSize + renderer.bounds.size.y), 0);
								points++;
						} else {
								next = dir * speed + curr;
						}

						transform.position = next;
				}
		}
	
		void OnGUI ()
		{
				GUIStyle g = new GUIStyle ();
				g.fontSize = 20;
				GUI.Label (new Rect (5.0f, 3.0f, 400.0f, 400.0f), "Score: " + points, g);
		}
}