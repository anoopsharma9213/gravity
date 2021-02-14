using UnityEngine;
using System.Collections;

public class Setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 temp = new Vector3 (Camera.main.orthographicSize * Camera.main.aspect / 3.6f,
		                            transform.localScale.y,
		                            transform.localScale.z);
		transform.localScale = temp;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
