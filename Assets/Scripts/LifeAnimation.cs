using UnityEngine;
using System.Collections;

public class LifeAnimation : MonoBehaviour
{

		public Sprite[] sprite;
		public SpriteRenderer sr;
		public float fps;

		// Use this for initialization
		void Start ()
		{
				sr = renderer as SpriteRenderer;
		}
	
		// Update is called once per frame
		void Update ()
		{
				int index = (int)(Time.timeSinceLevelLoad * fps);
				index = index % sprite.Length;
				sr.sprite = sprite [index];
		}
}
