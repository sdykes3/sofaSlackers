using UnityEngine;
using System.Collections;

public class gameOverScript : MonoBehaviour {
	
	public SpriteRenderer playarrow;
	public SpriteRenderer qarrow;
	
	// Use this for initialization
	void Start () {
		
		qarrow.enabled = false;
		playarrow.enabled = true;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.DownArrow)) { 
			playarrow.enabled = !playarrow.enabled;
			qarrow.enabled = !qarrow.enabled;
		}
		
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			playarrow.enabled = !playarrow.enabled;
			qarrow.enabled = !qarrow.enabled;
		}
		
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (playarrow.enabled) {
				Application.LoadLevel("GameMain");
			} else {
				Application.LoadLevel("Title");
			}
		}
		
	}
}
