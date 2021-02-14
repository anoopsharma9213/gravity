using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour
{

		public Sprite spike, original;
		public SpriteRenderer sr;
		public bool isspike;
		public float size;

		// Use this for initialization
		void Start ()
		{
				sr = renderer as SpriteRenderer;
				size = 0.35f;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (isspike == true) {
						sr.sprite = spike;
				} else {
						sr.sprite = original;
				}
				if (LevelSetup.gameOver == false && LevelSetup.application_pause == false) {
						Vector3 curr = transform.position;
						Vector3 next;
						if (curr.y > Camera.main.orthographicSize + 2 * size) {
								GameObject.FindGameObjectWithTag ("Life").GetComponent<LifeMovement> ().update_count ();
								GameObject.FindGameObjectWithTag ("Star").GetComponent<LifeMovement> ().update_count ();
								next = new Vector3 (Random.Range (-(Camera.main.orthographicSize * Camera.main.aspect), Camera.main.orthographicSize * Camera.main.aspect), -(Camera.main.orthographicSize + 2 * size), 0);
								LevelSetup.scores++;
								int spike_num = 0;
								for (int i= 1; i<5; i++) {
										GameObject go = GameObject.FindGameObjectWithTag ("Block" + i);
										if (go.GetComponent<BlockMovement> ().getStatus () == true && go.tag != gameObject.tag) {
												spike_num++;
										}
								}
								isspike = false;
								int prob;
								if (Application.loadedLevelName == "Level4") {
										prob = 80;
								} else if (Application.loadedLevelName == "Level3") {
										prob = 60;
								} else if (Application.loadedLevelName == "Level2") {
										prob = 50;
								} else {
										prob = 30;
								}
								if ((spike_num < 2 && LevelSetup.bspeed <= 5.0f) || (spike_num < 1 && LevelSetup.bspeed > 3.0f)) {
										if (Random.Range (0, 100) < prob) {
												isspike = true;
										}
								}
								transform.position = next;
						} else {
								next = Vector3.up * LevelSetup.bspeed + curr;
								transform.position = Vector3.Lerp (curr, next, Time.deltaTime);
						}
				}
		}

		public bool getStatus ()
		{
				return isspike;
		}

		public void setStatus (string arg)
		{
				if (arg == "true") {
						isspike = true;
				} else {
						isspike = false;
				}
		}
}
