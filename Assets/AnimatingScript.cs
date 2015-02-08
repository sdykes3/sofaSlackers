using UnityEngine;
using System.Collections;

public class AnimatingScript : MonoBehaviour {

	bool updatePos;

	// Use this for initialization
	void Start () {
		updatePos = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(updatePos)
		{
//			transform.parent.position = gameObject.transform.position;
//			transform.position = Vector3.zero;
			updatePos = false;
			gameObject.transform.parent.GetComponent<PlayerBehavior>().animationFinishedFlag = true;
			print ("Animation over!");
		}
	}
	
	public void animFinished()
	{
		//print ("Animation FINISHED");
		//gameObject.transform.parent.position = gameObject.transform.position;
		//gameObject.transform.position = Vector3.zero;
		gameObject.GetComponent<Animator>().StopPlayback();
		updatePos = true;

		//gameObject.transform.position = gameObject.transform.GetChild(0).position;
		//gameObject.transform.GetChild(0).position = Vector3.zero;
	}
}
