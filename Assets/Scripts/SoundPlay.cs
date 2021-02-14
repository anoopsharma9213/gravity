using UnityEngine;
using System.Collections;

public class SoundPlay : MonoBehaviour
{

		// Use this for initialization
		public void sound_play (AudioClip play)
		{
				if (MainMenu.sound == true) {
						audio.PlayOneShot (play);
				}
		}
}
