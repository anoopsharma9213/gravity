using UnityEngine;
using System.Collections;

public class LifeMovement : MonoBehaviour
{

		public bool show, go_up;
		public int show_count;
		public int count;

		// Use this for initialization
		void Start ()
		{
				show = false;
				go_up = false;
				count = 0;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (LevelSetup.gameOver == false && LevelSetup.application_pause == false) {
						if (tag == "Life") {
								if (transform.position.x == GameObject.FindGameObjectWithTag ("Star").transform.position.x) {
										if (transform.position.x >= 0) {
												transform.position = new Vector3 (transform.position.x - (GameObject.FindGameObjectWithTag ("Star").renderer.bounds.size.x * 0.5f + renderer.bounds.size.x * 0.6f), transform.position.y);
										} else {
												transform.position = new Vector3 (transform.position.x + (GameObject.FindGameObjectWithTag ("Star").renderer.bounds.size.x * 0.5f + renderer.bounds.size.x * 0.6f), transform.position.y);
										}
								}
						}
						if (show == true) {
								Vector3 pos = Vector3.one;
								for (int i= 1; i<5; i++) {
										GameObject go = GameObject.FindGameObjectWithTag ("Block" + i);
										if (go.transform.position.y < -Camera.main.orthographicSize && go.GetComponent<BlockMovement> ().getStatus () == false && GameObject.FindGameObjectWithTag ("Player").transform.position.y > -5.5f) {
												pos = new Vector3 (go.transform.position.x, go.transform.position.y + (go.renderer.bounds.size.y * 0.5f + renderer.bounds.size.y * 0.5f), 0);
												transform.position = pos;
												show = false;
												go_up = true;
												break;
										}
								}
						} else if (go_up == true) {
								Vector3 curr = transform.position;
								if (curr.y > Camera.main.orthographicSize + renderer.bounds.size.y * 0.5f) {
										transform.position = new Vector3 (0, -20, 0);
										go_up = false;
								} else {
										Vector3 next = curr + Vector3.up * LevelSetup.bspeed;
										transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
								}
						} else {
								if (show_count == count) {
										count = 0;
										show = true;
								}
						}
				}
		}

		public void setGo_up (bool arg)
		{
				go_up = arg;
		}

		public bool getGo_up ()
		{
				return go_up;
		}

		public void update_count ()
		{
				count++;
		}
}
