using UnityEngine;
using System.Collections;

public class Charactermovement : MonoBehaviour
{

		public Sprite[] sequence;
		public SpriteRenderer sr;
		public float fps;
		private bool iscaptured;

		// Use this for initialization
		void Start ()
		{
				sr = renderer as SpriteRenderer;
				iscaptured = false;
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (iscaptured == false) {
						int index = (int)(Time.timeSinceLevelLoad * fps);
						index = index % sequence.Length;
						sr.sprite = sequence [index];
				}
		}
}