using UnityEngine;
using System.Collections;

public class TitleSpript : MonoBehaviour {

	public SpriteRenderer arrow;
	public SpriteRenderer arrow_options_jzsux;

	// Use this for initialization
	void Start () {
	
		arrow_options_jzsux.enabled = false;
		arrow.enabled = true;
		
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.DownArrow)) { 
			arrow.enabled = !arrow.enabled;
			arrow_options_jzsux.enabled = !arrow_options_jzsux.enabled;
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			arrow.enabled = !arrow.enabled;
			arrow_options_jzsux.enabled = !arrow_options_jzsux.enabled;
		}
		
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (arrow.enabled) {
				Application.LoadLevel ("GameMain");
			} else {
				Application.LoadLevel("Creditz");			
			}
		}
		
	}
}
