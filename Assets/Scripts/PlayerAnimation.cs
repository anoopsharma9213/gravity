﻿using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
		public Sprite[] sprite;
		public Sprite stationary_sprite, falling_sprite;
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
				if (Application.loadedLevelName == "Level_4") {
						if (PlayerMovement.iscaptured == false || PlayerMovement.isanimated == true) {
								int index = (int)(Time.timeSinceLevelLoad * fps);
								index = index % sprite.Length;
								sr.sprite = sprite [index];
						} else {
								sr.sprite = falling_sprite;
						}
				} else {
						if (PlayerMovement.isanimated == true && PlayerMovement.iscaptured == true) {
								int index = (int)(Time.timeSinceLevelLoad * fps);
								index = index % sprite.Length;
								sr.sprite = sprite [index];
						} else if (PlayerMovement.iscaptured == true) {
								sr.sprite = stationary_sprite;
						} else {
								sr.sprite = falling_sprite;
						}
				}
		}
}
